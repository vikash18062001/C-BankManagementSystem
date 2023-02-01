using static System.Console;

public class BankingStaffMenu
{
    static int CurEmp = 0;
    BankingService BankingService = new BankingService();
    string? BankId;
    string? CurId;

    public void HomePage()
    {
        WriteLine("Note : AccountId is of format .First 3 letter of username + date format string \nBankdId is of format.First 3 letter of bankName + date format string \n");
        WriteLine("Enter the bankId");
        string? id = Utility.GetInputString();
        WriteLine("Enter the userName");
        string? userName = Utility.GetInputString();
        WriteLine("Enter the password");
        string? password = Utility.GetInputString();
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
                WriteLine("8 : Exit");
                WriteLine("Note : AccountId is of format .First 3 letter of username + date format string  \n BankdId is of format.First 3 letter of bankName + date format  string \n ");
                object? input = Utility.GetInputString();
                string? inputString = input?.ToString();
                switch (inputString)
                {
                    case "1":
                        BankDetailsOfEmployee detail = BankingService.CreateAccount(BankId!);
                        if (detail == null)
                            break;
                        GlobalDataService.currentBankEmployee[CurEmp++] = detail;
                        break;
                    case "2":
                        WriteLine("Enter Your accountId");
                        CurId = Utility.GetInputString();
                        BankingService.UpdateAccount(CurId!);
                        break;
                    case "3":
                        WriteLine("Enter the accountId you want to delete");
                        CurId = Utility.GetInputString();
                        BankingService.DeleteAccount(CurId!);
                        CurEmp--;
                        break;
                    case "4":
                        BankingService.ShowAll(ref GlobalDataService.currentBankEmployee, CurEmp);
                        WriteLine("Enter the bankId you want to change service");
                        CurId = Utility.GetInputString();
                        BankingService.ChangeService(CurId, true);
                        break;
                    case "5":
                        WriteLine("Enter the bankId You want to change service");
                        CurId = Utility.GetInputString();
                        BankingService.ChangeService(CurId, false);
                        break;
                    case "6":
                        WriteLine("Enter the accountId for which you want to get transaction history");
                        CurId = Utility.GetInputString();
                        BankingService.ShowTransactionHistory( CurId!, this.BankId!);
                        break;
                    case "7":
                        WriteLine("Enter the accountId for which you want to revert the transaction");
                        CurId = Utility.GetInputString();
                        BankingService.RevertTransaction( CurId!, this.BankId!);
                        break;
                    case "8":
                        return;
                }
                ReadKey();
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
        for (int i = 0; i < 20; i++)
        {
            if ((GlobalDataService.BankDetails[i]?.BankId != null) && (GlobalDataService.BankDetails[i].BankId == bankId) && (GlobalDataService.BankDetails[i].BankCreaterName == userName) && (GlobalDataService.BankDetails[i].Password == password))
            {
                this.BankId = bankId;
                return true;
            }
        }
        return false;
    }
}
