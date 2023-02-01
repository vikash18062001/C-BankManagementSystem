using static System.Console;

public class Program
{
    static void Main()
    {
        Clear();
        BankingStaffMenu bankingStaffMenu = new BankingStaffMenu();
        AccountHolderMenu accountHolderMenu = new AccountHolderMenu();
        CreatingBank creatingBank = new CreatingBank();

        while (true)
        {
            WriteLine("****Banking Management System****");
            WriteLine("\n\n");
            WriteLine("Login page");
            WriteLine("1 : Create A Bank");
            WriteLine("2 : Login as Bank Staff");
            WriteLine("3 : Login as Account Holder");
            WriteLine("4 : Exit");
            object? input = Utility.GetInputString();
            string? inputString = input?.ToString();

            switch (inputString)
            {
                case "1":
                    creatingBank.CreateBank();
                    break;
                case "2":
                    bankingStaffMenu.HomePage();
                    break;
                case "3":
                    accountHolderMenu.HomePage();
                    break;
                case "4":
                    Environment.Exit(1);
                    break;
                default:
                    WriteLine("Please enter a valid value");
                    break;
            }
            ReadKey();
        }
    }
}
