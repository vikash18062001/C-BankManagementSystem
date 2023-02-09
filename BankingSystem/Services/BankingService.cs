using System.Xml.Linq;
using static System.Console;

public class BankingService
{
    public Bank CreateBank(Bank bank)
    {
        try
        {
            bank.Id = Utility.GenerateBankId(bank.Name);
            if(!string.IsNullOrEmpty(bank.Id) && !string.IsNullOrEmpty(bank.Name) && !string.IsNullOrEmpty(bank.CreatedBy))
            {
                GlobalDataService.Banks.Add(bank);
            }
        }
        catch(Exception e)
        {
            
        }

        return bank;
    }

    public bool AddBankEmployee(Employee employee)
    {
        try
        {
            GlobalDataService.Employees.Add(employee);

            return true;
           
        }
        catch(Exception e)
        {
            return false;
        }
    }
    
    public AccountHolder CreateAccount(AccountHolder AccountHolder)
    {
        try
        {
            AccountHolder.Id = Utility.GetAccountId(AccountHolder.Name);
            GlobalDataService.AccountHolders.Add(AccountHolder);
        }
        catch (Exception e)
        {
            //Log exception
        }

        return AccountHolder;
    }

    public bool UpdateAccount(string id , Dictionary<string,object> updatedDetails)
    {
        List<AccountHolder> accountHolders = (from account in GlobalDataService.AccountHolders where account.Id == id select account).ToList<AccountHolder>();

        if (accountHolders.Count() == 0)
            return false;

        accountHolders.First().Email = updatedDetails["Email"].ToString()!;
        accountHolders.First().Mobile = updatedDetails["Mobile"].ToString()!;

        return true;
        
    }

    public bool DeleteAccount(string id)
    {
        int flag = 0;

        GlobalDataService.AccountHolders.RemoveAll(r => {
            if (r.Id == id)
            {
                flag = 1;
                return true;
            }
            return false;
        });

        if (flag == 0)
        {
            return false;
        }

        return DeleteTransactinon(id);
        
    }

    public bool ShowTransactionHistory( string id, string bankId)
    {
        int flag = 0,x,y;

        foreach (Transaction transaction in GlobalDataService.Transactions)
        {
            y = transaction.Id.IndexOf('/', x = transaction.Id.IndexOf('/') + 1);
            string retrivedBankId = transaction.Id.Substring(x, y - x);

            y = transaction.Id.IndexOf('/', x = transaction.Id.IndexOf('/', transaction.Id.IndexOf('/') + 1) + 1);
            string accountId = transaction.Id.Substring(x, y - x);

            if (Utility.ShowTransactionTable(transaction, id, bankId, retrivedBankId, accountId))
                flag = 1;

        }

        if (flag == 0)
            return false;
        else
            return true;
            
    }

    public Utility.StatusMessage RevertTransaction(string accountId,string transId)
    { 

        foreach(Transaction transaction in GlobalDataService.Transactions)
        {
            if (transaction?.Id == transId)
            {
                try
                {
                    RevertTheMoney(transaction, transId);
                    return Utility.StatusMessage.Success;
                }
                catch (Exception e)
                {
                    return Utility.StatusMessage.Failed;
                }
            }
        }
                
        return Utility.StatusMessage.Credential;

    }

    public bool DeleteTransactinon(string? accountId)
    {
        try
        {
            GlobalDataService.Transactions.RemoveAll(r => (r.SrcAccountId == accountId));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void RevertTheMoney(Transaction transaction, string transId)
    {
        AccountHolder senderAccount = Utility.GetAccountDetail(transaction.SrcAccountId);
        AccountHolder receiverAccount = Utility.GetAccountDetail(transaction.DstAccountId);

        if (!string.IsNullOrEmpty(transaction.DstAccountId) && transaction.Type)
        {
            receiverAccount.Balance -= transaction.Amount;
            senderAccount.Balance += transaction.Amount;
            GlobalDataService.Transactions.RemoveAll(item => item.Id == transId);
            return;
        }
        else if (string.IsNullOrEmpty(transaction.DstAccountId))
        {
            if (transaction.Type)
            {
                senderAccount.Balance -= transaction.Amount;
            }
            else
            {
                senderAccount.Balance += transaction.Amount;
            }
            GlobalDataService.Transactions.RemoveAll(item => item.Id == transId);
            return;

        }

        return;
    }

}
//TXN/BNKvikash/ACHvikash/44965.94019907407       ACHvikash                       Vikash          2/8/2023 10:33:53 PM            10              Debit