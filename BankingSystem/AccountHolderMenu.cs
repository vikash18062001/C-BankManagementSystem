using System;
using static System.Console;

public class AccountHolderMenu : BankingStaffMenu
{

    string? BankId;
    string? AccountId;

    public new void HomePage(ref CreatingBank bankCreation)
    {
        WriteLine(" Note : AccountId is of format .First 3 letter of username + " +
                    "{0}\nBankdId is Of format.First 3 letter of bankName {1} + \n",
                    DateTime.Now.Date.ToOADate(), DateTime.Now.Date.ToOADate());
        WriteLine("Enter the BankId");
        string? loginBankId = ReadLine();
        WriteLine("Enter the AccountId");
        string? loginAccountId = ReadLine();
        WriteLine("Enter the password");
        string? password = ReadLine();
        bool isValid = Validate(loginBankId!, loginAccountId!, password!);
        if (isValid)
        {
            Menu(ref bankCreation);
        }
        else
        {
            WriteLine("Invalid Credentials");
            return;
        }
    }

    public void Menu(ref CreatingBank bankCreation)
    {
        AccountService accountService = new AccountService();
        while (true)
        {

            WriteLine("\n\n*****Account Holder Menu****\n\n");
            WriteLine("Choose What You Want to Do\n");
            WriteLine("1 : Deposit Amount");
            WriteLine("2 : WithDraw Amount");
            WriteLine("3 : Transfer Funds");
            WriteLine("4 : Get Transaction History");
            WriteLine("5 : Exit ");

            object? input = ReadLine();
            string? inputString = input?.ToString();
            string? id;
            double money;

            switch (inputString)
            {
                case "1":
                    WriteLine("Enter the AccountId ");
                    id = ReadLine();
                    WriteLine("Enter amount to Deposit");
                    try { money = Convert.ToDouble(ReadLine()); } catch { WriteLine("Enter Valid Amount"); break; };
                    accountService.DepositMoney(id!, this.BankId!, String.Empty, String.Empty, money, false);
                    break;
                case "2":
                    WriteLine("Enter the AccountId");
                    id = ReadLine();
                    WriteLine("Enter amount to WithDraw");
                    try { money = Convert.ToDouble(ReadLine()); } catch { WriteLine("Enter Valid Amount"); break; };
                    accountService.WithDrawMoney(id!, this.BankId!, String.Empty, String.Empty, money, false);
                    break;
                case "3":
                    WriteLine("Enter BankId To which you want to transfer");
                    string? bankId = ReadLine();
                    WriteLine("Enter AccountId of the user you want to transfer money");
                    string? accountId = ReadLine();
                    WriteLine("Enter Amount You want to Transfer");
                    try { money = Convert.ToDouble(ReadLine()); } catch { WriteLine("Enter Valid Amount"); break; };
                    accountService.TransferFund(this.BankId!, bankId!, this.AccountId!, accountId!, money, ref bankCreation);
                    break;
                case "4":
                    accountService.ShowTransactionHistory(this.AccountId!, this.BankId!);
                    break;
                case "5":
                    return;
                default:
                    WriteLine("Please Enter A Valid Input");
                    break;
            }
        }
    }

    public bool Validate(string bankId, string accountId, string password)
    {
        WriteLine(currentBankEmployee);
        foreach (BankDetailsOfEmployee detail in currentBankEmployee)
        {
            if (detail != null && detail.BankId == bankId && detail.AccountId == accountId && detail.Password == password)
            {
                this.BankId = bankId;
                this.AccountId = accountId;
                return true;
            }
        }
        return false;
    }
}


