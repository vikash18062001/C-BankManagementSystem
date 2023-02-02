//checked
using static System.Console;

public static class HomePage
{
    public static void HomePageLogin()
    {
        Clear();
        BankingStaffMenu bankingStaffMenu = new BankingStaffMenu();
        AccountHolderMenu accountHolderMenu = new AccountHolderMenu();
        CreatingBank creatingBank = new CreatingBank();

        while (true)
        {
            WriteLine("****Banking Management System****");
            WriteLine("\n\n");
            WriteLine("1 : Create A Bank");
            WriteLine("2 : Login as Bank Staff");
            WriteLine("3 : Login as Account Holder");
            WriteLine("4 : Exit");
            string? input = Utility.GetInputString();
            if (Utility.isNull(input))
            {
                WriteLine("Please enter a valid string.It cannot be empty.");
                continue;
            }

            switch (input)
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
        }
    }
}
	


