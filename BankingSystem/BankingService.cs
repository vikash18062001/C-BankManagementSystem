using static System.Console;

public class BankingService
{
    public BankDetailsOfEmployee CreateAccount(string bankId)
    {
        BankDetailsOfEmployee bankDetailsOfEmployee = new BankDetailsOfEmployee();
        int? initialBalance = 0;
        WriteLine("Enter the Name");
        string? name = ReadLine();
        WriteLine("Enter the dob");
        string? dob = ReadLine();
        WriteLine("Enter InitialBalance");

        try {
            initialBalance = Convert.ToInt32(ReadLine());
        } catch {
            WriteLine("Enter Valid Amount ");
            return null!;
        };

        WriteLine("Enter the password You want for this account");
        string? password = ReadLine();

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
                obj.Name = ReadLine();
                WriteLine("Enter the new dob");
                obj.DOB = ReadLine();
            }
        }
        if ( flag == 0 )
        {
            WriteLine("\nEnter a Valid Id My Brother\n");
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
            WriteLine("\nEnter Valid id Please\n");
        
    }

    public void ShowTransactionHistory(ref BankDetailsOfEmployee[] ob, string id, string bankId)
    {

        foreach (BankDetailsOfEmployee obj in ob)
        {
            if (obj?.AccountId == id && obj?.BankId == bankId)
            {
                WriteLine("TransactionId\t\t\t\tBankId\t\tAccountId\t\tAmount\t\tisFundTransfer\t\tAction\t\t");
                foreach (Transaction transaction in obj.Transaction)
                {
                    if (transaction?.Id == null)
                        continue;
                    if (transaction?.AccountId == null)
                        break;
                    string action = transaction.IsCredit ? "Credit" : "Debit";
                    WriteLine("{0}\t{1}\t{2}\t\t{3}\t\t{4}\t\t{5}", transaction.Id, transaction.BankId, transaction.AccountId, transaction.Amount,transaction.IsFundTransfer,action);
                }
                return;
            }
        }
        Console.WriteLine("Enter Valid ID ");
    }

    public void RevertTransaction(ref BankDetailsOfEmployee[] ob, string id, string bankId)
    { 
        WriteLine("Enter the TransId You want to Revert");
        string? trans_id = ReadLine();

        foreach (BankDetailsOfEmployee obj in ob)
        {
            if ((obj?.AccountId != null))
            {
                foreach (Transaction transaction in obj.Transaction)
                {
                    if (transaction?.Id == trans_id)
                    {
                        transaction!.Id = null;
                        if (transaction.IsCredit)
                        {
                            obj.CurBalance -= transaction.Amount;
                        }
                        else
                            obj.CurBalance += transaction.Amount;
                        return;

                    }
                }
            }
        }
        WriteLine("Please Enter Valid Transaction Id");
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

    public void ChangeService(string? bankId, ref CreatingBank bankCreationObj, bool isSame)
    {
        //GlobalDataService GlobalDataService = bankCreationObj.GlobalService;
        int index = Array.IndexOf(GlobalDataService.BankIds, bankId);
        if (index == -1)
        {
            WriteLine("Enter Valid BankID");
            return;
        }

        if (isSame)
        {
            WriteLine("Enter serive charge for RTGS in percent ");
            GlobalDataService.RTGSSame[index] = Convert.ToDouble(ReadLine());
            WriteLine("Enter serive charge for IMPS in percent ");
            GlobalDataService.IMPSSame[index] = Convert.ToDouble(ReadLine());
        }
        else
        {

            WriteLine("Enter serive charge for RTGS in percent ");
            GlobalDataService.RTGSDiff[index] = Convert.ToDouble(ReadLine());
            WriteLine("Enter serive charge for IMPS in percent ");
            GlobalDataService.IMPSDiff[index] = Convert.ToDouble(ReadLine());

        }

    }

    //public void exchangeCurRate(ref BankCreation bankCreation , string bankId)
    //{
    //	WriteLine("Enter the currency In Short form i.e INR");
    //	string? cur = ReadLine();
    //	WriteLine("Enter the ExchangeRate to INR");
    //	double? rate = Convert.ToDouble(ReadLine());
    //	Dictionary<string, double> dict = new Dictionary<string, double>();
    //	dict[cur] = rate;
    //	bankCreation.exchangeRate[bankId].
    //}
}
