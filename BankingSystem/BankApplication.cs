using static System.Console;

public class BankApplication
{
    BankingService BankingService = new BankingService();

    public void Initialize()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        WriteLine("****Banking Management System****\n\n1 : Create A Bank \n2 : Login\n3 : Exit");

        int option = Utility.GetIntInput("Choose a value from the menu", true);

        switch (option)
        {
            case 1:
                this.CreateBank();
                MainMenu();
                break;

            case 2:
                this.Login();
                MainMenu();
                break;

            case 3:
                Environment.Exit(1);
                return ;

            default:
                WriteLine("Please enter a valid value");
                MainMenu();
                break;
        }
       
    }

    private void CreateBank()
    {
        Bank bank = new Bank()
        {
            Name = Utility.GetInputString("Enter the bank name", true),
            CreatedBy = Utility.GetInputString("Enter the name of the creater.", true), // Is the Upper Man for that particular bank
            CreatedOn = DateTime.Now,
        };

        var employeeName = Utility.GetInputString("Enter the Employee Name You Want to make head for that particular bank",true);
        var password = Utility.GetPassword("Enter the password ", true);

        bank  = BankingService.CreateBank(bank);

        if (bank != null && string.IsNullOrEmpty(bank.Id))
            WriteLine("Bank creation is unsuccessful");
        else
        {
            this.AddEmployeeDetails(bank,employeeName,password);
            WriteLine($"Bank creation is successful.The bank Id is {bank.Id}");
        }
    }

    private void AddEmployeeDetails( Bank bank , string employeeName , string password )
    {
        Employee employee = new Employee()
        {
            Name = employeeName,
            Password = password,
            Email = Utility.GetInputEmail("Enter the email ", true),
            Mobile = Utility.GetInputMobileNo("Enter the mobileno", true),
            Salary = Utility.GetDoubleAmount("Enter the salary"),
            Type = Enums.LoginTypes.Employee.ToString(),
            Id = BankingService.GetEmployeeId(employeeName),
            BankId = bank.Id,
        };

        APIResponse response = BankingService.AddBankEmployee(employee);
        WriteLine(response.Message);    
    }

    private void Login()
    {
        LoginRequest login = new LoginRequest()
        {
            UserId = Utility.GetInputString("Enter the Id", true),
            Password = Utility.GetPassword("Enter the password", true),
        };

        string type = Utility.GetType(login);

        if (IsValidLogin(type,login))
        {
            WriteLine("Succesfully Logged In");
            login.Type = type;
            this.NavigateToUserMenuBy(login);
        }

    }

    private bool IsValidLogin(string type,LoginRequest login)
    {
        if (type == "Admin" || (string.IsNullOrEmpty(type) || !AccountService.IsAuthenticated(login, type)))
        {
            WriteLine("Check your credentials either it is incorrect or it ");
            if (ContinueCurrentProcessOrNot())
                Login();
            else
                return false;
        }

        return true;
    }

    private bool ContinueCurrentProcessOrNot()
    {
        WriteLine("Do you want to continure login . If yes type y and if NO type anything");
        string ch = ReadLine().ToLower().Trim();

        if (ch == "y")
            return true;
        else
            return false;

    }

    private void NavigateToUserMenuBy(LoginRequest login)
    {
        BankingStaff bankingStaffMenu = new BankingStaff();
        AccountHolderMenu accountHolderMenu = new AccountHolderMenu();

        if(login.Type == Enums.LoginTypes.Admin.ToString())
        {
            bankingStaffMenu.HomePage(login);
        }
        else if(login.Type == Enums.LoginTypes.Employee.ToString())
        {
            bankingStaffMenu.HomePage(login);
        }
        else if(login.Type == Enums.LoginTypes.Accountholder.ToString())
        {
            accountHolderMenu.HomePage(login);
        }
                
    }
}