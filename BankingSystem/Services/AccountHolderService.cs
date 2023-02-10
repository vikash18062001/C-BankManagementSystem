using System.Reflection;
using static System.Console;

public class AccountHolderService 
{
    public APIResponse TransferFund(TransferFund transferFund)
    {
       
        try
        {           
            Transaction transaction = this.GetTransactionDetail(transferFund.SrcAccountHolder, transferFund.DstAccountHolder.Id, transferFund.OriginalAmount);

            return MakeTransfer(transaction, transferFund.NewAmount);
        }
        catch(Exception ex)
        {
            return Utility.SetApiMessage(false, "Unexcepted error came please try again");
        }
    }

    public APIResponse MakeTransfer(Transaction transaction, double amountAfterCharges)
    {
        if (transaction != null)
        {
            return TransferMoneyFromSender(transaction, amountAfterCharges);
        }

        return Utility.SetApiMessage(false, "Some error occured");
    }

    public APIResponse TransferMoneyFromSender(Transaction transaction  , double amountAfterCharges)
    {
        try
        {
            AccountHolder accountHolder = this.GetAccountHolder(transaction.SrcAccountId);
            if (accountHolder == null || string.IsNullOrEmpty(accountHolder.BankId))
                return Utility.SetApiMessage(false, "Not able to find the account please check the id");
            if (accountHolder.Balance < amountAfterCharges)
                return Utility.SetApiMessage(false, $"Not enough balance on your account {accountHolder.Balance}");

            if (DepositMoneyToReceiver(transaction, transaction.Amount))
            {
                return MakeTransactionObject(accountHolder, transaction, amountAfterCharges); // have to create new transaction because previous one is getting changed for both of them ads it is call by reference.
            }

            return Utility.SetApiMessage(false, "Transfer is Unsuccesful check id");
        }
        catch (Exception ex)
        { }
        return Utility.SetApiMessage(false, "Transfer is unsuccesful check id");
    }

    public APIResponse MakeTransactionObject(AccountHolder accountHolder,Transaction transaction,double amountAfterCharges)
    {
        APIResponse apiResponse = new APIResponse();
        try
        {
            Transaction newTransaction = GetTransactionDetail(accountHolder, transaction.DstAccountId, amountAfterCharges); // have to create new transaction because previous one is getting changed for both of them ads it is call by reference.
            newTransaction.Id = transaction.Id;
            newTransaction.Type = false;
            newTransaction.Amount = amountAfterCharges;
            GlobalData.Transactions.Add(newTransaction);
            accountHolder.Balance -= amountAfterCharges;
            apiResponse = Utility.SetApiMessage(true, "Successfully transferred the money");
        }
        catch
        {
            apiResponse = Utility.SetApiMessage(false, "Some error occured please try again");
        }
        return apiResponse;
    }

    public bool DepositMoneyToReceiver(Transaction transaction, double transferAmount)
    {
        try
        {
            AccountHolder accountHolder = this.GetAccountHolder(transaction.DstAccountId);
            if (accountHolder == null || string.IsNullOrEmpty(accountHolder.Id))
                return false;

            accountHolder.Balance += transferAmount;
            transaction.Type = true;
            transaction.Amount = transferAmount;
            GlobalData.Transactions.Add(transaction);
            return true;
        }
        catch (Exception ex)
        {

        }
        return false;

    }

    public List<Transaction> ShowTransactionHistory(string id, string bankId)
    {
        BankingService bankingService = new BankingService();

        List<Transaction> userTransaction = bankingService.ShowTransactionHistory(id, bankId);

        return userTransaction;
    }

    public Transaction GetTransactionDetail(AccountHolder accountHolder , string receiverAccountId,double transactionAmount)
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
        Bank srcBank = BankingService.GetBankDetails(currentAccountHolder.BankId);
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

        return SetNewAmountBasedOnCharges(transferFund);

    }
    public APIResponse SetNewAmountBasedOnCharges(TransferFund transferFund)
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

        return TransferFund(transferFund);
    }
}