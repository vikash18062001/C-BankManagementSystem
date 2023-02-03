using static System.Console;

public class HomePage
{
    BankingService BankingService = new BankingService();
    public void Initialize()
    {
        Clear();
        BankingStaffMenu bankingStaffMenu = new BankingStaffMenu();
        AccountHolderMenu accountHolderMenu = new AccountHolderMenu();
        
        while (true)
        {
            WriteLine("****Banking Management System****");
            WriteLine("\n\n");
            WriteLine("1 : Create A Bank \n2 : Login as Bank Staff\n3 : Login as Account Holder\n4 : Exit");

            int input = Utility.GetIntInput("Choose a value from the menu",true);

            switch (input)
            {
                case 1:
                    CreateBank();
                    break;

                case 2:
                    bankingStaffMenu.HomePage();
                    break;

                case 3:
                    accountHolderMenu.HomePage();
                    break;

                case 4:
                    Environment.Exit(1);
                    break;

                default:
                    WriteLine("Please enter a valid value");
                    break;
            }
        }
    }

    public void CreateBank()
    {
        Bank Bank = new Bank
        {
            Name = Utility.GetInputString("Enter the bank name", true),
            CreaterName = Utility.GetInputString("Enter the creater name", true),
            Password = Utility.GetPassword("Enter the password ", true),
        };
        Bank bank  = BankingService.CreateBank(Bank);
        if (string.IsNullOrEmpty(bank.Id))
        {
            WriteLine("Bank creation is unsuccessful");
        }
        else
        {
            WriteLine(bank.Id);
            WriteLine("Bank creation is successful");
        }
        
    }
}


