using System.Reflection;
using static System.Console;

public class AccountHolderService
{ 
    public List<Transaction> GetTransactionHistory(string id, string bankId)
    {
        BankingService bankingService = new BankingService();

        List<Transaction> userTransaction = bankingService.GetTransactionHistory(id, bankId);

        return userTransaction;
    }

    public Transaction TransactionInfo(AccountHolder accountHolder , string receiverAccountId,double transactionAmount)
    {
        try
        {
            Transaction transaction = new Transaction()
            {
                SrcAccountId = accountHolder.Id,
                Amount = transactionAmount,
                CreatedBy = accountHolder.Name,
                DstAccountId = receiverAccountId,
                CreatedOn = DateTime.Now,
                Id = $"TXN/{accountHolder.BankId}/{accountHolder.Id}/{DateTime.Now.ToOADate()}",
            };

            return transaction;
        }
        catch
        {
            return new Transaction();
        }
    }

    public AccountHolder GetAccountHolder(string accountId)
    {
        try
        {
            if (!string.IsNullOrEmpty(accountId))
            {
                return GlobalData.AccountHolders.Find(accountHolder => accountHolder.Id == accountId);
            }
        }
        catch (Exception ex)
        {
            //log
        }
        
        return null;
    }

    public APIResponse UpdateAccountHolderAfterDeposit(AccountHolder accountHolder)
    {
        APIResponse apiResponse = new APIResponse();

        try
        {
            var index = GlobalData.AccountHolders.FindIndex(accHolder => accHolder.Id == accountHolder.Id);
            if (index > -1)
            {
                GlobalData.AccountHolders[index] = accountHolder;
                apiResponse.Message = "Successfully deposited.";
                apiResponse.IsSuccess = true;
            }
        }
        catch (Exception ex)
        {
            apiResponse.Message = "Unable to deposit money.";
            apiResponse.IsSuccess = false;
        }

        return apiResponse;
    }

    public APIResponse UpdateAccountHolderAfterWithdraw(AccountHolder accountHolder)
    {
        APIResponse apiResponse = new APIResponse();

        try
        {
            var index = GlobalData.AccountHolders.FindIndex(accHolder => accHolder.Id == accountHolder.Id);
            if (index > -1)
            {
                GlobalData.AccountHolders[index] = accountHolder;
                apiResponse.Message = "Successful Withdraw.";
                apiResponse.IsSuccess = true;
            }
        }
        catch (Exception ex)
        {
            apiResponse.Message = "Unable to withdraw money.";
            apiResponse.IsSuccess = false;
        }

        return apiResponse;
    }

    public APIResponse MakeTransferFundObject(AccountHolder currentAccountHolder, string receiverAccountId, double transferAmount, int modeOfTransfer)
    {
        Bank srcBank = BankingService.GetBankDetail(currentAccountHolder.BankId);
        if (srcBank == null)
            return Utility.SetApiMessage(false, "Please enter correct id");

        AccountHolder dstAccountHolder = this.GetAccountHolder(receiverAccountId);

        if (dstAccountHolder == null || string.IsNullOrEmpty(dstAccountHolder.Id))
            return Utility.SetApiMessage(false, "Please enter correct id");

        if (modeOfTransfer != 1 && modeOfTransfer != 2)
            return Utility.SetApiMessage(false, "Please choose correct mode");

        TransferFund transferFund = new TransferFund()
        {
            SrcBank = srcBank,
            ModeOfTransfer = modeOfTransfer,
            SrcAccountHolder = currentAccountHolder,
            DstAccountHolder = dstAccountHolder,
            OriginalAmount = transferAmount
        };

        return SetNewAmountBasedOnCharge(transferFund);

    }
    public APIResponse SetNewAmountBasedOnCharge(TransferFund transferFund)
    {
        double charge, newAmount = transferFund.OriginalAmount;

        if (transferFund.SrcAccountHolder.BankId == transferFund.DstAccountHolder.BankId)
        {
            if (transferFund.ModeOfTransfer == 2)
            {
                charge = transferFund.SrcBank.IMPSSame / 100;
                newAmount = transferFund.OriginalAmount + (charge == 0.0 ? 0.05 : charge) * transferFund.OriginalAmount;
            }
        }
        else
        {
            if (transferFund.ModeOfTransfer == 1)
            {
                charge = transferFund.SrcBank.RTGSDiff / 100;
                newAmount = transferFund.OriginalAmount + (charge == 0 ? 0.02 : charge) * transferFund.OriginalAmount;
            }
            else if (transferFund.ModeOfTransfer == 2)
            {
                charge = transferFund.SrcBank.IMPSDiff / 100;
                newAmount = transferFund.OriginalAmount + (charge == 0 ? 0.06 : charge) * transferFund.OriginalAmount;
            }
        }
        transferFund.NewAmount = newAmount;

        return MakeTransfer(transferFund);
    }

    public APIResponse MakeTransfer(TransferFund transferFund)
    {
        try
        {
            AccountHolder srcAccountHolder = this.GetAccountHolder(transferFund.SrcAccountHolder.Id);
            AccountHolder dstAccountHolder = this.GetAccountHolder(transferFund.DstAccountHolder.Id);
            if (srcAccountHolder == null || string.IsNullOrEmpty(srcAccountHolder.BankId) || dstAccountHolder == null || string.IsNullOrEmpty(dstAccountHolder.Id))
                return Utility.SetApiMessage(false, "Not able to find the account please check the id");
            if (srcAccountHolder.Balance < transferFund.NewAmount)
                return Utility.SetApiMessage(false, $"Not enough balance on your account {srcAccountHolder.Balance}");

            Transaction transaction = WithDrawMoneyFromSender(transferFund);

            if(transaction != null && !string.IsNullOrEmpty(transaction.Id))
                return DepositMoneyToReceiver(transferFund,transaction); 

            return Utility.SetApiMessage(false, "Transfer is Unsuccesful check id");
        }
        catch (Exception ex)
        { }
        return Utility.SetApiMessage(false, "Transfer is unsuccesful check id");
    }

    public APIResponse DepositMoneyToReceiver(TransferFund transferFund , Transaction transaction)
    {
        APIResponse apiResponse = new APIResponse();
        try
        {
            Transaction newTransaction = TransactionInfo(transferFund.SrcAccountHolder, transferFund.DstAccountHolder.Id,transferFund.OriginalAmount); // have to create new transaction because previous one is getting changed for both of them ads it is call by reference.
            newTransaction.Id = transaction.Id;
            newTransaction.Type = true;
            transferFund.DstAccountHolder.Balance += transferFund.OriginalAmount;
            transferFund.SrcAccountHolder.Balance -= transferFund.NewAmount;
            GlobalData.Transactions.Add(transaction);
            GlobalData.Transactions.Add(newTransaction);
            apiResponse = Utility.SetApiMessage(true, "Successfully transferred the money");
        }
        catch (Exception ex)
        {
            apiResponse = Utility.SetApiMessage(false, "Some error occured please try again");
        }

        return apiResponse;
    }

    public Transaction WithDrawMoneyFromSender(TransferFund transfer)
    {
        try
        {
            Transaction transaction = TransactionInfo(transfer.SrcAccountHolder, transfer.DstAccountHolder.Id, transfer.NewAmount); // have to create new transaction because previous one is getting changed for both of them ads it is call by reference.
            transaction.Type = false;
            transaction.Amount = transfer.NewAmount;
            return transaction;
        }
        catch
        {
            return null;
        }
    }
}