using static System.Console;

public class Program
{
    static void Main()
    {
        BankingStaffMenu bankingStaffMenu = new BankingStaffMenu();
        AccountHolderMenu accountHolderMenu = new AccountHolderMenu();
        CreatingBank creatingBank = new CreatingBank();

        while (true)
        {
            WriteLine("****Banking Management System****");
            WriteLine("\n\n");
            WriteLine("Login Page");
            WriteLine("1 : Create A Bank");
            WriteLine("2 : Login as Bank Staff");
            WriteLine("3 : Login as Account Holder");
            WriteLine("4 : Exit");
            object? input = ReadLine();
            string? inputString = input?.ToString();

            switch (inputString)
            {
                case "1":
                    creatingBank.CreateBank();
                    break;
                case "2":
                    bankingStaffMenu.HomePage(ref creatingBank);
                    break;
                case "3":
                    accountHolderMenu.HomePage(ref creatingBank);
                    break;
                case "4":
                    Environment.Exit(1);
                    break;
                default:
                    WriteLine("Please Enter a Valid Value");
                    break;
            }
            ReadKey();
        }
    }
}
