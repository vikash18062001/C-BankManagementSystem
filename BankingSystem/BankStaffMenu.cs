using static System.Console;

public class BankingStaffMenu
{
    public static BankDetailsOfEmployee[] currentBankEmployee = new BankDetailsOfEmployee[10];
    static int CurEmp = 0;
    BankingService BankingService = new BankingService();
    string? BankId;
    string? CurId;

    public void HomePage(ref CreatingBank bankCreationObj)
    {
        WriteLine("Note : AccountId is of format .First 3 letter of username + {0}\nBankdId is Of format.First 3 letter of bankName {1}\n"
                          , DateTime.Now.Date.ToOADate(), DateTime.Now.Date.ToOADate());
        WriteLine("Enter the bankId");
        string? id = ReadLine();
        WriteLine("Enter the userName");
        string? userName = ReadLine();
        WriteLine("Enter the password");
        string? password = ReadLine();
        bool isValid = Validate(id!, userName!, password!, ref bankCreationObj);
        if (isValid)
        {

            while (true)
            {
                WriteLine("\n\n****Banking Management System****\n\n");
                WriteLine("Choose What You Want to do\n");
                WriteLine("1 : Creation of Account");
                WriteLine("2 : Update an Account");
                WriteLine("3 : Delete An Account");
                WriteLine("4 : Add Service Charge For Same Bank");
                WriteLine("5 : Add Service Charge For Different Bank");
                WriteLine("6 : View Transaction History");
                WriteLine("7 : Revert a Transaction");
                WriteLine("8 : Exit");
                WriteLine("Note : AccountId is of format .First 3 letter of username + {0}\nBankdId is Of format.First 3 letter of bankName {1}\n"
                          , DateTime.Now.Date.ToOADate(), DateTime.Now.Date.ToOADate());
                object? input = Console.ReadLine();
                string? inputString = input?.ToString();
                switch (inputString)
                {
                    case "1":
                        BankDetailsOfEmployee detail = BankingService.CreateAccount(BankId!);
                        if (detail == null)
                            break;
                        currentBankEmployee[CurEmp++] = detail;
                        break;
                    case "2":
                        WriteLine("Enter Your AccountId");
                        CurId = ReadLine();
                        BankingService.UpdateAccount(ref currentBankEmployee, CurId!);
                        break;
                    case "3":
                        WriteLine("Enter the AccountId You Want to delete");
                        CurId = ReadLine();
                        BankingService.DeleteAccount(ref currentBankEmployee, CurId!);
                        CurEmp--;
                        break;
                    case "4":
                        BankingService.ShowAll(ref currentBankEmployee, CurEmp);
                        WriteLine("Enter the BankId You want to change Service");
                        CurId = ReadLine();
                        BankingService.ChangeService(CurId, ref bankCreationObj, true);
                        break;
                    case "5":
                        WriteLine("Enter the BankId You want to change Service");
                        CurId = ReadLine();
                        BankingService.ChangeService(CurId, ref bankCreationObj, false);
                        break;
                    case "6":
                        WriteLine("Enter the AccountId for which you want to get transaction history");
                        CurId = ReadLine();
                        BankingService.ShowTransactionHistory(ref currentBankEmployee, CurId!, this.BankId!);
                        break;
                    case "7":
                        WriteLine("Enter the AccountId for which you want to revert the transaction");
                        CurId = ReadLine();
                        BankingService.RevertTransaction(ref currentBankEmployee, CurId!, this.BankId!);
                        break;
                    case "8":
                        return;
                }
                ReadKey();

            }
        }
        else
        {
            WriteLine("Enter Correct Details");

        }
        return;
    }

    public bool Validate(string bankId, string userName, string password, ref CreatingBank bankCreationObj)
    {
        for (int i = 0; i < 10; i++)
        {
            if ((GlobalDataService.BankIds[i] != null) && (GlobalDataService.BankIds[i] == bankId) && (GlobalDataService.BankCreaterName[i] == userName) && (GlobalDataService.Passwords[i] == password))
            {
                this.BankId = bankId;
                return true;
            }
        }
        return false;
    }
}
