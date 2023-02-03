using static System.Console;

public class BankingStaffMenu
{

    BankingService BankingService = new BankingService();

    string CurId { get; set; }

    public BankingStaffMenu()
    {
        this.CurId = string.Empty;
    }

    public void HomePage()
    { 
        
        string id = Utility.GetInputString("Enter the BankId",true);
       
        string userName = Utility.GetInputString("Enter the username",true);

        string password = Utility.GetInputString("Enter the password",true);
      
        Bank bank = BankingService.Validate(id!, userName!, password!);

        if (bank != null)
        {
            while (true)
            {
                WriteLine("\n\n****Banking Management System****\n\n");
                WriteLine("1 : Creation of Account\n2 : Update an Account\n3 : Delete An Account\n4 : Add Service Charge For Same Bank\n5 : Add Service Charge For Different Bank\n6 : View Transaction History\n7 : Revert a Transaction\n8 : Return to previous menu");
                
                int input = Utility.GetIntInput("Choose what you want to do",true);

                switch (input)
                {
                    case 1:
                        Dictionary<string,object> detail = getInput(bank) ;
                        if (detail == null)
                            break;
                        Utility.Message((BankingService.CreateAccount(detail)), "created");
                        break;

                    case 2:
                        CurId = Utility.GetInputString("Enter your accountId",true);
                        if (CurId == null)
                            break;
                        Dictionary<string,object> newDetail = UpdateAccount();
                        Utility.Message(BankingService.UpdateAccount(CurId, newDetail),"updated");
                        break;

                    case 3:
                        CurId = Utility.GetInputString("Enter the accountId you want to delete",true);
                        if (CurId == null)
                            break;
                        Utility.Message(BankingService.DeleteAccount(CurId),"deleted");
                        break;

                    case 4:
                        //BankingService.ShowAll(GlobalDataService.AccountHolder, CurEmp);
                        CurId = Utility.GetInputString("Enter the bankId you want to change service",true);
                        if (CurId == null)
                            break;
                        ChangeServiceRate(CurId, true);
                        break;

                    case 5:
                        CurId = Utility.GetInputString("Enter the bankId You want to change service",true);
                        if (CurId == null)
                            break;
                        ChangeServiceRate(CurId, false);
                        break;

                    case 6:
                        CurId = Utility.GetInputString("Enter the accountId for which you want to get transaction history",true)!;
                        if (CurId == null)
                            break;
                        BankingService.ShowTransactionHistory( CurId, bank.Id);
                        break;

                    case 7:
                        CurId = Utility.GetInputString("Enter the accountId for which you want to revert the transaction",true)!;
                        if (CurId == null)
                            break;
                        BankingService.RevertTransaction( CurId, bank.Id);
                        break;

                    case 8:
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

    public Dictionary<string,object> getInput(Bank bank)
    {
        Dictionary<string, object> inputData = new Dictionary<string, object>();

        string name;
    
        inputData.Add("Name",name = Utility.GetInputString("Enter the name", true));

        inputData.Add("Email", Utility.GetInputString("Enter email", true));

        inputData.Add("Mobile", Utility.GetInputString("Enter the mobileno", true));

        inputData.Add("Password", Utility.GetPassword("Enter password", true));

        inputData.Add("Type", "AccountHolder");

        inputData.Add("Id", Utility.GetAccountId(name!));

        inputData.Add("BankId", bank.Id);

        inputData.Add("Balance", 0);

        return inputData;
    }

    public Dictionary<string, object> UpdateAccount()
    {

        Dictionary<string, object> inputData = new Dictionary<string, object>();

        inputData.Add("Name", null!);

        inputData.Add("Email", Utility.GetInputString("Enter email", true));

        inputData.Add("Mobile", Utility.GetInputString("Enter the mobileno", true));

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
            return;

        if (isSame)
        {

            model.RTGSSame = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for RTGS in percent "));

            model.IMPSSame = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for IMPS in percent"));
        }
        else
        {
            model.RTGSDiff = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for RTGS in percent "));

            model.IMPSDiff = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for IMPS in percent "));

        }
    }

}
