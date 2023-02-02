using static System.Console;

public class BankingStaffMenu
{
    static int CurEmp = 0;
    BankingService BankingService = new BankingService();
    string BankId { get; set; }
    string CurId { get; set; }

    public BankingStaffMenu()
    {
        this.BankId = string.Empty;
        this.CurId = string.Empty;
    }

    public void HomePage()
    {
        WriteLine("Enter the bankId");
        string? id = Utility.GetInputString();
        if(Utility.isNull(id))
        {
            Message();
            return;
        }
        this.BankId = id!;

        WriteLine("Enter the userName");
        string? userName = Utility.GetInputString();
        if (Utility.isNull(userName))
        {
            Message();
            return;
        }

        WriteLine("Enter the password");
        string? password = Utility.GetInputString();
        if (Utility.isNull(password))
        {
            Message();
            return;
        }

        bool isValid = Validate(id!, userName!, password!);
        if (isValid)
        {
            while (true)
            {
                WriteLine("\n\n****Banking Management System****\n\n");
                WriteLine("Choose what you want to do\n");
                WriteLine("1 : Creation of Account");
                WriteLine("2 : Update an Account");
                WriteLine("3 : Delete An Account");
                WriteLine("4 : Add Service Charge For Same Bank");
                WriteLine("5 : Add Service Charge For Different Bank");
                WriteLine("6 : View Transaction History");
                WriteLine("7 : Revert a Transaction");
                WriteLine("8 : Return to previous menu");
                
                string? input = Utility.GetInputString();
                if(Utility.isNull(input))
                {
                    Message();
                    return;
                }
                string? inputString = input?.ToString();
                switch (inputString)
                {
                    case "1":
                        Dictionary<string,object> detail = getInput() ;
                        if (detail == null)
                            break;
                        AccountHolder accountHolder = BankingService.CreateAccount(detail);
                        GlobalDataService.AccountHolder.Add(accountHolder); 
                        break;

                    case "2":
                        WriteLine("Enter Your accountId");
                        CurId = Utility.GetInputString()!;
                        if (CurId == null)
                            break;
                        Dictionary<string,object> newDetail = UpdateAccount();
                        if (!BankingService.UpdateAccount(CurId!, newDetail))
                            break;
                        break;

                    case "3":
                        WriteLine("Enter the accountId you want to delete");
                        CurId = Utility.GetInputString()!;
                        if (CurId == null)
                            break;
                        if(!BankingService.DeleteAccount(CurId!))
                            WriteLine("\nEnter valid id please\n");
                        break;

                    case "4":
                        BankingService.ShowAll(GlobalDataService.AccountHolder, CurEmp);
                        WriteLine("Enter the bankId you want to change service");
                        CurId = Utility.GetInputString()!;
                        if (CurId == null)
                            break;
                        ChangeServiceRate(CurId, true);
                        break;

                    case "5":
                        WriteLine("Enter the bankId You want to change service");
                        CurId = Utility.GetInputString()!;
                        if (CurId == null)
                            break;
                        ChangeServiceRate(CurId, false);
                        break;

                    case "6":
                        WriteLine("Enter the accountId for which you want to get transaction history");
                        CurId = Utility.GetInputString()!;
                        if (CurId == null)
                            break;
                        BankingService.ShowTransactionHistory( CurId!, this.BankId!);
                        break;

                    case "7":
                        WriteLine("Enter the accountId for which you want to revert the transaction");
                        CurId = Utility.GetInputString()!;
                        if (CurId == null)
                            break;
                        BankingService.RevertTransaction( CurId!, this.BankId!);
                        break;

                    case "8":
                        return;
                }
            }
        }
        else
        {
            WriteLine("Enter correct details");

        }
        return;
    }

    public bool Validate(string bankId, string userName, string password)
    {
        int size = GlobalDataService.Bank.Count();
        for (int i = 0; i < size; i++)
        {
            if ((GlobalDataService.Bank[i]?.Id != null) && (GlobalDataService.Bank[i].Id == bankId) && (GlobalDataService.Bank[i].CreaterName == userName) && (GlobalDataService.Bank[i].Password == password))
            {
                this.BankId = bankId;
                return true;
            }
        }
        return false;
    }

    public void Message()
    {
        WriteLine( "Value cannot be Empty.Please Enter valid Value");
    }

    public Dictionary<string,object> getInput()
    {
        Dictionary<string, object> inputData = new Dictionary<string, object>();
    
        WriteLine("Enter the name");
        string? name = Utility.GetInputString();
        if (Utility.isNull(name))
        {
            Message();
            return null!;
        }
        if (name?.Length < 3)
        {
            WriteLine("Please enter valid name");
            return null!;
        }
        WriteLine("Enter the mobileno");
        string? mobile = Utility.GetInputString();
        if (Utility.isNull(mobile))
        {
            Message();
            return null!;
        }

        WriteLine("Enter Email");
        string? email = Utility.GetInputString();
        if(Utility.isNull(email))
        {
            Message();
            return null!;
        }

        WriteLine("Enter the password You want for this account");
        string? password = Utility.GetInputString();
        if (Utility.isNull(email))
        {
            Message();
            return null!;
        }


        string? accountId = Utility.GetAccountId(name!);
    
        inputData.Add("Name",name!);
        inputData.Add("Email", email!);
        inputData.Add("Mobile", mobile!);
        inputData.Add("Password", password!);
        inputData.Add("Type", "AccountHolder");
        inputData.Add("Id", accountId);
        inputData.Add("BankId", this.BankId);
        inputData.Add("Balance", 0);

        return inputData;
    }

    public Dictionary<string, object> UpdateAccount()
    {
        string? mobile=null, email=null;
        Dictionary<string, object> inputData = new Dictionary<string, object>();

        if (UpdateMessage("mobileno"))
        {
            WriteLine("Enter the mobileno");
            mobile = Utility.GetInputString();
            if (Utility.isNull(mobile))
            {
                Message();
                return null!;
            }
        }

        if (UpdateMessage("email"))
        {
            WriteLine("Enter Email");
            email = Utility.GetInputString();
            if (Utility.isNull(email))
            {
                Message();
                return null!;
            }
        }

        inputData.Add("Name", null!);
        inputData.Add("Email", email!);
        inputData.Add("Mobile", mobile!);
        inputData.Add("Password", null!);
        inputData.Add("Type", "AccountHolder");
        inputData.Add("Id", null!);
        inputData.Add("BankId", null!);
        inputData.Add("Balance", null!);

        return inputData;


    }

    public bool UpdateMessage(string? field)
    {

        WriteLine("Do you want to change {0} .If yes type Y else anything.", field);
        string? input = ReadLine()?.ToLower();
        if (input == "y")
            return true;
        else 
            return false;
       

    }


    public void ChangeServiceRate(string? bankId,bool isSame)
    {
        Bank model = Utility.GetBankDetails(bankId);
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
}