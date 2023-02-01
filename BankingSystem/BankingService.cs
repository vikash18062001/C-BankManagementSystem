using static System.Console;

public class BankingService
{
    public BankDetailsOfEmployee CreateAccount(string bankId)
    {
        BankDetailsOfEmployee bankDetailsOfEmployee = new BankDetailsOfEmployee();
        int? initialBalance = 0;
        WriteLine("Enter the name");
        string? name = Utility.GetInputString();
        WriteLine("Enter the dob");
        string? dob = Utility.GetInputString();
        WriteLine("Enter initialbalance");

        try {
            initialBalance = Convert.ToInt32(Utility.GetInputString());
        } catch {
            WriteLine("Enter valid amount ");
             return null!;
        };

        WriteLine("Enter the password You want for this account");
        string? password = Utility.GetInputString();

        bankDetailsOfEmployee.Name = name;
        bankDetailsOfEmployee.DOB = dob;
        bankDetailsOfEmployee.InitialBalance = initialBalance;
        bankDetailsOfEmployee.CurBalance = initialBalance;
        bankDetailsOfEmployee.BankId = bankId;
        bankDetailsOfEmployee.Password = password;
        bankDetailsOfEmployee.GetAccountId(name!);
        return bankDetailsOfEmployee;
    }

    public void UpdateAccount(ref BankDetailsOfEmployee[] ob, string id)
    {
        int flag = 0;
        foreach (BankDetailsOfEmployee obj in ob)
        {
            if (obj?.AccountId == id)
            {
                if (obj?.AccountId == null)
                    break;
                flag = 1;
                Console.WriteLine("Enter the new Name");
                obj.Name = Utility.GetInputString();
                WriteLine("Enter the new dob");
                obj.DOB = Utility.GetInputString();
            }
        }
        if ( flag == 0 )
        {
            WriteLine("\nEnter a valid id my brother\n");
        }
        
    }

    public void DeleteAccount(ref BankDetailsOfEmployee[] ob, string id)
    {
        int flag = 0;
        ob = Array.FindAll(ob, i => {
            if (i?.AccountId == id)
                flag = 1;
            return i?.AccountId != id;
        });
        if (flag == 0)
            WriteLine("\nEnter valid id please\n");
        
    }

    public void ShowTransactionHistory(ref BankDetailsOfEmployee[] ob, string id, string bankId)
    {
        WriteLine("TransactionId\t\t\t\t\t\tBankId\t\tAccountId\t\tAmount\t\tisFundTransfer\t\tAction\t\t");
        int flag = 0;
        foreach (Transaction transaction in GlobalDataService.Transaction)
        {
            if(transaction?.AccountId == id && transaction?.BankId == bankId)
            {
                flag = 1;
                if (transaction?.Id == null)
                    continue;
                if (transaction?.AccountId == null)
                    break;
                string action = transaction.IsCredit ? "Credit" : "Debit";
                WriteLine("{0}\t{1}\t{2}\t\t{3}\t\t{4}\t\t{5}", transaction.Id, transaction.BankId, transaction.AccountId, transaction.Amount, transaction.IsFundTransfer, action);
            }
        }
        if(flag == 0) 
            Console.WriteLine("Enter valid ID ");
    }

    public void RevertTransaction(ref BankDetailsOfEmployee[] ob, string accountId, string bankId)
    { 
        WriteLine("Enter the transId you want to revert");
        string? transId = Utility.GetInputString();
        foreach(Transaction transaction in GlobalDataService.Transaction)
        {
            if(transaction?.AccountId == accountId && transaction?.BankId == bankId)
            {
                if (transaction?.Id == transId)
                {
                    transaction!.Id = null;
                    BankDetailsOfEmployee detail = Utility.GetBankDetailsOfEmployee(accountId, bankId);
                    if (transaction.IsCredit)
                    {
                        detail.CurBalance -= transaction.Amount;
                    }
                    else
                        detail.CurBalance += transaction.Amount;
                    return;
                }
            }
        }
        WriteLine("Please enter valid transaction id");
        return;
    }

    public void ShowAll(ref BankDetailsOfEmployee[] ob, int cursize)
    {
        if (ob[0]?.AccountId != null)
        {
            foreach (BankDetailsOfEmployee obj in ob)
            {
                if (obj?.AccountId == null)
                    break;
                WriteLine(obj.AccountId);
                WriteLine(obj.Name);
                WriteLine(obj.DOB);
                WriteLine(obj.CurBalance);
            }
        }
        return;
    }

    public void ChangeService(string? bankId, bool isSame)
    {
        //model model = bankCreationObj.GlobalService;
        //int index = Array.IndexOf(model.BankIds, bankId);
        BankDetailModel model = Utility.GetBankDetails(bankId);
        if (model == null)

        {
            WriteLine("Enter valid bankID");
            return;
        }

        if (isSame)
        {
            WriteLine("Enter serive charge for RTGS in percent ");
            model.RTGSSame = Convert.ToDouble(Utility.GetInputString());
            WriteLine("Enter serive charge for IMPS in percent ");
            model.IMPSSame = Convert.ToDouble(Utility.GetInputString());
        }
        else
        {

            WriteLine("Enter serive charge for RTGS in percent ");
            model.RTGSDiff = Convert.ToDouble(Utility.GetInputString());
            WriteLine("Enter serive charge for IMPS in percent ");
            model.IMPSDiff = Convert.ToDouble(Utility.GetInputString());

        }

    }

    //public void exchangeCurRate(ref BankCreation bankCreation , string bankId)
    //{
    //	WriteLine("Enter the currency In Short form i.e INR");
    //	string? cur = Utility.GetInputString();
    //	WriteLine("Enter the ExchangeRate to INR");
    //	double? rate = Convert.ToDouble(Utility.GetInputString());
    //	Dictionary<string, double> dict = new Dictionary<string, double>();
    //	dict[cur] = rate;
    //	bankCreation.exchangeRate[bankId].
    //}
}
