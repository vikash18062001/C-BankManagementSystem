using System;
using static System.Console;

public class AccountHolderMenu 
{

    string? BankId;
    string? AccountId;

    public void HomePage()
    {
        WriteLine("Enter the bankId");
        string? loginBankId = Utility.GetInputString();
        WriteLine("Enter the accountId");
        string? loginAccountId = Utility.GetInputString();
        WriteLine("Enter the password");
        string? password = Utility.GetInputString();

        bool isValid = Validate(loginBankId!, loginAccountId!, password!);
        if (isValid)
        {
            Menu();
        }
        else
        {
            WriteLine("Invalid credentials");
            return;
        }
    }

    public void Menu()
    {
        AccountService accountService = new AccountService();
        while (true)
        {

            WriteLine("\n\n*****Account Holder Menu****\n\n");
            WriteLine("Choose what you want to do\n");
            WriteLine("1 : Deposit amount");
            WriteLine("2 : WithDraw amount");
            WriteLine("3 : Transfer funds");
            WriteLine("4 : Get transaction history");
            WriteLine("5 : Exit ");

            object? input = Utility.GetInputString();
            string? inputString = input?.ToString();
            string? id;
            double money;

            switch (inputString)
            {
                case "1":
                    WriteLine("Enter the accountId ");
                    id = Utility.GetInputString();
                    if (Utility.isNull(id))
                        break;
                    WriteLine("Enter amount to deposit");
                    try { money = Convert.ToDouble(Utility.GetInputString()); } catch { WriteLine("Enter Valid Amount"); break; };
                    if (!accountService.DepositMoney(id!, this.BankId!, money,null))
                        WriteLine("Enter valid credentials");
                    break;

                case "2":
                    WriteLine("Enter the accountId");
                    id = Utility.GetInputString();
                    if (Utility.isNull(id))
                        break;

                    WriteLine("Enter amount to withDraw");
                    try { money = Convert.ToDouble(Utility.GetInputString()); } catch { WriteLine("Enter Valid Amount"); break; };
                    if (accountService.WithDrawMoney(id!, this.BankId!, money,null))
                        WriteLine("Successful withdraw");
                    else
                        WriteLine("Withdraw unsucessful either check the balance or check the credentials");
                    break;

                case "3":
                    WriteLine("Enter bankId to which you want to transfer");
                    string? bankId = Utility.GetInputString();
                    if (Utility.isNull(bankId))
                        break;

                    WriteLine("Enter accountId of the user you want to transfer money");
                    string? accountId = Utility.GetInputString();
                    if (Utility.isNull(accountId))
                        break;

                    WriteLine("Enter amount you want to transfer");
                    try { money = Convert.ToDouble(Utility.GetInputString()); } catch { WriteLine("Enter Valid Amount"); break; };

                    if (Utility.checkIfValidIdsOrNot(this.BankId!, this.AccountId) && Utility.checkIfValidIdsOrNot(bankId, accountId))
                        accountService.TransferFund(this.BankId!, bankId!, this.AccountId!, accountId!, money);
                    else
                        WriteLine("Enter valid ids");
                    break;

                case "4":
                    accountService.ShowTransactionHistory(this.AccountId!, this.BankId!);
                    break;

                case "5":
                    return;

                default:
                    WriteLine("Please enter a valid input");
                    break;
            }
        }
    }

    public bool Validate(string bankId, string accountId, string password)
    {
        WriteLine(GlobalDataService.AccountHolder);
        foreach (AccountHolder detail in GlobalDataService.AccountHolder)
        {
            if (detail != null && detail.BankId == bankId && detail.Id == accountId && detail.Password == password)
            {
                this.BankId = bankId;
                this.AccountId = accountId;
                return true;
            }
        }
        return false;
    }
}
