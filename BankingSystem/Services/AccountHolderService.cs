using System.Data.Common;
using System.Reflection;
using System.Transactions;
using static System.Console;

public class AccountHolderService 
{
    public bool DepositMoney(Transaction transaction)
    {

        string bankId = Utility.GetAccountDetail(transaction.SrcAccountId).BankId;

        List<AccountHolder> accountHolders = (from account in GlobalDataService.AccountHolders where account.Id == transaction.SrcAccountId && account.BankId == bankId select account).ToList<AccountHolder>();

        if (accountHolders.Count() == 0)
            return false;

        accountHolders.First().Balance += transaction.Amount;
        GlobalDataService.Transactions.Add(transaction);
        return true;

    }

    public Utility.StatusMessage WithDrawMoney(Transaction transaction) // account id
    {
        string bankId = Utility.GetAccountDetail(transaction.SrcAccountId).BankId;

        List<AccountHolder> accountHolders = (from account in GlobalDataService.AccountHolders where account.Id == transaction.SrcAccountId && account.BankId == bankId select account).ToList<AccountHolder>();

        if (accountHolders.Count() == 0)
            return Utility.StatusMessage.Failed;
        else
        {
            if (accountHolders.First().Balance < transaction.Amount)
                return Utility.StatusMessage.Balance;

            accountHolders.First().Balance -= transaction.Amount;
            GlobalDataService.Transactions.Add(transaction);
            return Utility.StatusMessage.Success;
        }

    }

    public Utility.StatusMessage TransferFund(AccountHolder accountHolder, string receiverAccountId, double transferAmount , int modeOfTransfer)
    {
        double charge, newAmount = transferAmount;

        Bank model = Utility.GetBankDetails(accountHolder.BankId);

        string receiverBankId = Utility.GetAccountDetail(receiverAccountId).BankId;

        if(modeOfTransfer != 1 || modeOfTransfer != 2)
            return Utility.StatusMessage.WrongSelection;

        if (accountHolder.BankId == receiverBankId)
        {
            if (modeOfTransfer == 2)
            {
                charge = model.IMPSSame / 100;
                newAmount = transferAmount + (charge == 0.0 ? 0.05 : charge) * transferAmount;
            }
        }
        else
        {
            if (modeOfTransfer == 1)
            {
                charge = model.RTGSDiff / 100;
                newAmount = transferAmount + (charge == 0 ? 0.02 : charge) * transferAmount;
            }
            else if (modeOfTransfer == 2)
            {
                charge = model.IMPSDiff / 100;
                newAmount = transferAmount + (charge == 0 ? 0.06 : charge) * transferAmount;
            }
        }
        Transaction transaction = GetTransactionDetail(accountHolder,receiverAccountId , newAmount);
        return MakeTransfer(transaction,transferAmount,newAmount);

    }

    public Utility.StatusMessage MakeTransfer(Transaction transaction, double amount, double newAmount)
    {
        if (transaction != null)
        {
            return TransferMoneyFromSender(transaction, amount, newAmount);
        }
        return Utility.StatusMessage.Failed;

    }

    public Utility.StatusMessage TransferMoneyFromSender(Transaction transaction ,double amount , double newAmount)
    {
        string srcBankId = Utility.GetAccountDetail(transaction.SrcAccountId).BankId;
        if(string.IsNullOrEmpty(srcBankId))
        {
            return Utility.StatusMessage.Failed;
        }
        List<AccountHolder> accountHolders = (from account in GlobalDataService.AccountHolders where account.Id == transaction.SrcAccountId && account.BankId == srcBankId select account).ToList<AccountHolder>();
        if (accountHolders.Count == 0)
            return Utility.StatusMessage.Failed;

        if (accountHolders.First().Balance < newAmount)
            return Utility.StatusMessage.Balance;

        if (DepositMoneyToReceiver(transaction, amount, newAmount))
        {
            Transaction newTransaction = GetTransactionDetail(accountHolders.First(), transaction.DstAccountId, newAmount); // have to create new transaction because previous one is getting changed for both of them ads it is call by reference.
            newTransaction.Id = transaction.Id;
            newTransaction.Type = false;
            newTransaction.Amount = newAmount;
            GlobalDataService.Transactions.Add(newTransaction);
            accountHolders.First().Balance -= newAmount;
            return Utility.StatusMessage.Success;
        }

        return Utility.StatusMessage.Failed;

        //foreach (AccountHolder accountHolder in GlobalDataService.AccountHolders)
        //{
        //    if(accountHolder.Id == transaction.SrcAccountId && accountHolder.BankId == srcBankId)
        //    {
        //        if (accountHolder.Balance < newAmount)
        //        {
        //            return Utility.StatusMessage.Balance;

        //        }
                            
        //        return Utility.StatusMessage.Failed;
        //    }
        //}
        //return Utility.StatusMessage.Failed;
    }

    public bool DepositMoneyToReceiver(Transaction transaction, double amount, double newAmount)
    {
        string bankId = Utility.GetAccountDetail(transaction.DstAccountId).BankId;

        List<AccountHolder> accountHolders = (from account in GlobalDataService.AccountHolders where account.Id == transaction.DstAccountId && account.BankId == bankId select account).ToList<AccountHolder>();
        if (accountHolders.Count == 0)
            return false;

        accountHolders.First().Balance += amount;
        transaction.Type = true;
        transaction.Amount = amount;
        GlobalDataService.Transactions.Add(transaction);
        return true;
        //foreach (AccountHolder accountHolder in GlobalDataService.AccountHolders)
        //{
        //    if (accountHolder.Id == transaction.DstAccountId && accountHolder.BankId == bankId)
        //    {
        //        accountHolder.Balance += amount;
        //        transaction.Type = true;
        //        transaction.Amount = amount;
        //        GlobalDataService.Transactions.Add(transaction);
        //        return true;
        //    }
        //}
        //return false;
    }

    public void ShowTransactionHistory(string id, string bankId)
    {
        BankingService bankingService = new BankingService();

        bankingService.ShowTransactionHistory(id, bankId);
    }

    public Transaction GetTransactionDetail(AccountHolder accountHolder , string receiverAccountId,double amount)
    {
        Transaction transaction = new Transaction()
        {
            SrcAccountId = accountHolder.Id,
            Amount = amount,
            CreatedBy = accountHolder.Name,
            DstAccountId = receiverAccountId,
            CreatedOn = DateTime.Now,
            Id = $"TXN/{accountHolder.BankId}/{accountHolder.Id}/{DateTime.Now.ToOADate()}",
        };

        return transaction;
    }

    //public double DecideNewAmount(AccountHolder accountHolder , string receiverAccountId, double amount , int mode)
    //{
    //    double newAmount = amount;
    //    double charge;


    //    string receiverBankId = Utility.GetAccountDetail(receiverAccountId).BankId;

    //    if (accountHolder.BankId == receiverBankId)
    //    {
    //        if (mode == 2)
    //        {
    //            charge = model.IMPSSame / 100;
    //            newAmount = amount + (charge == 0.0 ? 0.05 : charge) * amount;
    //        }
    //    }
    //    else
    //    {
    //        if (mode == 1)
    //        {
    //            charge = model.RTGSDiff / 100;
    //            newAmount = amount + (charge == 0 ? 0.02 : charge) * amount;
    //        }
    //        else if (mode == 2)
    //        {
    //            charge = model.IMPSDiff / 100;
    //            newAmount = amount + (charge == 0 ? 0.06 : charge) * amount;
    //        }
    //        else
    //        {
    //            return Utility.StatusMessage.WrongSelection;
    //        }
    //    }
    //}

}
