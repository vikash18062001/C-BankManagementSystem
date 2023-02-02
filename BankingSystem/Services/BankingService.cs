using static System.Console;

public class BankingService
{
    public AccountHolder CreateAccount(Dictionary<string,object> details)
    {
        AccountHolder accountHolder = new AccountHolder();

        accountHolder.Name = details["Name"].ToString()!;
        accountHolder.Balance = Convert.ToDouble(details["Balance"]);
        accountHolder.BankId = details["BankId"].ToString()!;
        accountHolder.Password = details["Password"].ToString()!;
        accountHolder.Id = details["Id"].ToString()!;
        accountHolder.Email = details["Email"].ToString()!;
        accountHolder.Mobile = details["Mobile"].ToString()!;
        accountHolder.Type = details["Type"].ToString()!;

        return accountHolder;
    }

    public bool UpdateAccount(string id , Dictionary<string,object> newDetail)
    {
        int flag = 0;
        foreach (AccountHolder obj in GlobalDataService.AccountHolder)
        {
            if (obj?.Id == id)
            {
                if (obj?.Id == null)
                    break;
                flag = 1;
                obj.Email = newDetail["Email"].ToString()!;
                obj.Mobile = newDetail["Mobile"].ToString()!;
            }
        }
        if ( flag == 0 )
        {
            return false;
        }
        return true;
        
    }

    public bool DeleteAccount(string id)
    {
        int flag = 0;
        GlobalDataService.AccountHolder.RemoveAll(r => {
            if (r.Id == id)
            {
                flag = 1;
                return true;
            }
            return false;
        });
        if (flag == 0)
            return false;
        DeleteTransactinon(id);
        return true;
        
    }

    public void ShowTransactionHistory( string id, string bankId)
    {
        WriteLine("\t\tTransactionId\t\t\t\t\t\tBankId\t\t\tAccountId\tCreatedBy\t\tCreatedOn\t\tAmount\t\tAction\t\t");
        int flag = 0;
        foreach (Transaction transaction in GlobalDataService.Transactions)
        {
            flag = 1;
            if(transaction?.AccountId == id && transaction?.BankId == bankId)
            {
                flag = 2;
                string action = transaction.Type ? "Credit" : "Debit";
                WriteLine("{0}\t{1}\t{2}\t\t{3}\t\t{4}\t\t{5}\t\t{6}", transaction.Id, transaction.BankId, transaction.AccountId, transaction.CreatedBy,transaction.CreatedOn,transaction.Amount,action);
            }
        }
        if (flag == 0)
            WriteLine("No Transaction");
        else if (flag == 1)
            WriteLine("Enter valid ID ");
    }

    public void RevertTransaction(string accountId, string bankId)
    { 
        WriteLine("Enter the transId you want to revert");
        string? transId = Utility.GetInputString();
        if (transId == null)
            return;
        foreach(Transaction transaction in GlobalDataService.Transactions)
        {
            if(transaction?.AccountId == accountId && transaction?.BankId == bankId)
            {
                if (transaction?.Id == transId)
                { 
                    AccountHolder detail = Utility.GetDetails(accountId, bankId);
                    if (transaction.Type)
                    {
                        detail.Balance -= transaction.Amount;
                    }
                    else
                        detail.Balance += transaction.Amount;
                    GlobalDataService.Transactions.Remove(transaction);
                    return;
                }
            }
        }
        WriteLine("Please enter valid transaction id");
        return;
    }

    public void DeleteTransactinon(string? accountId)
    {
        GlobalDataService.Transactions.RemoveAll(r=>(r.AccountId == accountId));
    }

    public void ShowAll(List<AccountHolder> ob, int cursize)
    {
        if (ob[0]?.Id != null)
        {
            foreach (AccountHolder obj in ob)
            {
                if (obj?.Id == null)
                    break;
                WriteLine(obj.Id);
                WriteLine(obj.Name);
                WriteLine(obj.Mobile);
                WriteLine(obj.Balance);
            }
        }
        return;
    }
  
}
