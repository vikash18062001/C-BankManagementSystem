using System;
using static System.Console;
  public class Home
    {
        static void Main()
        {

            BankingStaffMenu bankingStaffMenu = new BankingStaffMenu();
            AccountHolderMenu accountHolderMenu = new AccountHolderMenu();
            BankCreation bankCreation = new BankCreation();
            while (true)
            {
                WriteLine("****Banking Management System****");
                WriteLine("\n\n");
                WriteLine("Login Page");
                WriteLine("0 : Create A Bank");
                WriteLine("1 : Login as Bank Staff");
                WriteLine("2 : Login as Account Holder");
                WriteLine("3 : Exit");
                object? input = ReadLine();
                string? inputString = input?.ToString();

                switch (inputString)
                {
                    case "0":
                        bankCreation.CreateBank();
                        break;
                    case "1":
                        bankingStaffMenu.HomePage(ref bankCreation);
                        break;
                    case "2":
                        accountHolderMenu.HomePage(ref bankCreation);
                        break;
                    case "3":
                        Environment.Exit(1);
                        break;

                }
                ReadKey();
            }

        }
    }
