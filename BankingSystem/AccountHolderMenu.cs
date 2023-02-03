using System;
using static System.Console;

public class AccountHolderMenu 
{

    AccountService AccountService = new AccountService();

    public void HomePage()
    {
        string? loginBankId = Utility.GetInputString("Enter the bankId",true);

        string? loginAccountId = Utility.GetInputString("Enter the accountid",true);

        string? password = Utility.GetInputString("Enter the password ",true);

        AccountHolder accountDetail = AccountService.ValidateAccountDetails(loginBankId!, loginAccountId!, password!);
        if (accountDetail != null)
        {
            Menu(accountDetail);
        }
        else
        {
            WriteLine("Invalid credentials");
            return;
        }
    }

    public void Menu(AccountHolder accountDetail)
    {
        AccountService accountService = new AccountService();
        while (true)
        {

            WriteLine("\n\n*****Account Holder Menu****\n\n");
            WriteLine("1 : Deposit amount\n2 : WithDraw amount\n3 : Transfer funds\n4 : Get transaction history\n5 : Return to previous menu");

            int input = Utility.GetIntInput("Choose what you want to do",true);
            string? id;
            double money;

            switch (input)
            {
                case 1:
                    id = Utility.GetInputString("Enter the accountid",true);
                    money = Utility.GetDoubleAmount("Enter the amount to deposit");
                    DepositMoney(id, accountDetail.BankId, money);
                    break;

                case 2:
                    id = Utility.GetInputString("Enter the accountid",true);
                    money = Utility.GetDoubleAmount("Enter the amount to withdraw");
                    WithdrawMoney(id, accountDetail.BankId, money);
                    break;

                case 3:
                    string bankId = Utility.GetInputString("Enter the bankid to which you want to transfer",true);
                    string accountId = Utility.GetInputString("Enter accountId of the user you want to transfer money",true);
                    money = Utility.GetDoubleAmount("Enter amount you want to transfer");
                    if (Utility.checkIfValidIdsOrNot(accountDetail.BankId, accountDetail.Id) && Utility.checkIfValidIdsOrNot(bankId, accountId))
                        accountService.TransferFund(accountDetail.BankId, bankId, accountDetail.Id, accountId, money);
                    else
                        WriteLine("Enter valid ids");
                    break;

                case 4:
                    accountService.ShowTransactionHistory(accountDetail.Id!, accountDetail.BankId!);
                    break;

                case 5:
                    return;

                default:
                    WriteLine("Please enter a valid input");
                    break;
            }
        }
    }
    public void DepositMoney(string  id , string bankId, double amount)
    {
        Dictionary<string, object> credential = new Dictionary<string, object>();
        credential.Add("AccountId2", id);
        credential.Add("BankId2", bankId);
        credential.Add("Amount", amount);
        credential.Add("IsFundTransfer", false);
        credential.Add("Name", null);

        if(!AccountService.DepositMoney(credential))
            WriteLine("Enter valid credentials");
        else
            WriteLine("Successfully Deposited");


    }

    public void WithdrawMoney(string id, string bankId, double amount)
    {
        Dictionary<string, object> credential = new Dictionary<string, object>();
        credential.Add("AccountId", id);
        credential.Add("BankId", bankId);
        credential.Add("Amount", amount);
        credential.Add("IsFundTransfer", false);
        credential.Add("Name", null);

        if (AccountService.WithDrawMoney(credential))
            WriteLine("Successful withdraw");
        else
            WriteLine("Withdraw unsucessful either check the balance or check the credentials");
       


    }


}

