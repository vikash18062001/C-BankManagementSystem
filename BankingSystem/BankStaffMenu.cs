using static System.Console;

public class BankingStaffMenu
{
    static int CurEmp = 0;
    BankingService BankingService = new BankingService();
    string? BankId;
    string? CurId;

    public void HomePage()
    {
        WriteLine("Note : AccountId is of format .First 3 letter of username + {0}\nBankdId is of format.First 3 letter of bankName {1}\n"
                          , DateTime.Now.Date.ToOADate(), DateTime.Now.Date.ToOADate());
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
                WriteLine("Note : AccountId is of format .First 3 letter of username + {0}\nBankdId is of format.First 3 letter of bankName {1}\n"
                          , DateTime.Now.Date.ToOADate(), DateTime.Now.Date.ToOADate());
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
                        BankingService.UpdateAccount(ref GlobalDataService.currentBankEmployee, CurId!);
                        break;
                    case "3":
                        WriteLine("Enter the accountId you want to delete");
                        CurId = Utility.GetInputString();
                        BankingService.DeleteAccount(ref GlobalDataService.currentBankEmployee, CurId!);
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
                        BankingService.ShowTransactionHistory(ref GlobalDataService.currentBankEmployee, CurId!, this.BankId!);
                        break;
                    case "7":
                        WriteLine("Enter the accountId for which you want to revert the transaction");
                        CurId = Utility.GetInputString();
                        BankingService.RevertTransaction(ref GlobalDataService.currentBankEmployee, CurId!, this.BankId!);
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
