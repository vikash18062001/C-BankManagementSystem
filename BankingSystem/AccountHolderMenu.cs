using System;
using static System.Console;

public class AccountHolderMenu
{

    AccountHolderService AccountHolderService = new AccountHolderService();

    public void HomePage(LoginRequest login)
    {
        if (login != null)
        {
            Menu(login);
        }
        else
        {
            WriteLine("Invalid credentials");
            return;
        }
    }

    private void Menu(LoginRequest login)
    {

        AccountHolder accountDetail = Utility.GetAccountDetail(login.UserId);

        WriteLine("\n\n*****Account Holder Menu*****\n\n1 : Deposit amount\n2 : WithDraw amount\n3 : Transfer funds\n4 : Get transaction history\n5 : Return to previous menu");

        int option = Utility.GetIntInput("Choose what you want to do", true);

        switch (option)
        {
            case 1:
                this.DepositMoney(accountDetail);
                Menu(login);
                return;

            case 2:
                this.WithdrawMoney(accountDetail);
                Menu(login);
                return;

            case 3:
                this.TransferFunds(accountDetail);
                Menu(login);
                return;

            case 4:
                this.ShowTransactionHistory(accountDetail);
                Menu(login);
                return;

            case 5:
                return;

            default:
                WriteLine("Please enter a valid input");
                Menu(login);
                return;

        }
    }

    private void DepositMoney(AccountHolder accountHolder)
    {
        double amount = Utility.GetDoubleAmount("Enter the amount to deposit");

        Transaction transaction = new Transaction();
        transaction.SrcAccountId = accountHolder.Id;
        transaction.Amount = amount;
        transaction.CreatedBy = accountHolder.Name;
        transaction.CreatedOn = DateTime.Now;
        transaction.Type = true;
        transaction.Id = $"TXN/{accountHolder.BankId}/{accountHolder.Id}/{DateTime.Now.ToOADate()}";

        if (!AccountHolderService.DepositMoney(transaction))
            WriteLine("Enter valid credentials");
        else
            WriteLine("Successfully Deposited");
    }

    private void WithdrawMoney(AccountHolder accountHolder)
    {
        double amount = Utility.GetDoubleAmount("Enter the amount to withdraw");

        Transaction transaction = new Transaction();
        transaction.SrcAccountId = accountHolder.Id;
        transaction.Amount = amount;
        transaction.CreatedBy = accountHolder.Name;
        transaction.CreatedOn = DateTime.Now;
        transaction.Type = false;
        transaction.Id = $"TXN/{accountHolder.BankId}/{accountHolder.Id}/{DateTime.Now.ToOADate()}";

        Utility.StatusMessage result = AccountHolderService.WithDrawMoney(transaction);
        if (result == Utility.StatusMessage.Success)
        {
            WriteLine("Successful withdraw");
        }
        else if (result == Utility.StatusMessage.Balance)
        {
            WriteLine("Not enough money . Cur balance:{0}", accountHolder.Balance);
        }
        else
        {
            WriteLine("Withdraw unsucessful check the credentials");
        }

    }

    private void TransferFunds(AccountHolder currentAccountHolder)
    {
        string bankId = Utility.GetInputString("Enter the bankid to which you want to transfer", true);
        string accountId = Utility.GetInputString("Enter accountId of the user you want to transfer money", true);
        double money = Utility.GetDoubleAmount("Enter amount you want to transfer");

        if(accountId == currentAccountHolder.Id)
        {
            WriteLine("Cannot transfer to the same account");
            return;
        }

        if (Utility.checkIfValidIdsOrNot(currentAccountHolder.BankId, currentAccountHolder.Id) && Utility.checkIfValidIdsOrNot(bankId, accountId))
        {
            int mode = Utility.GetIntInput("Which mode is this 1:RTGS , 2 : IMPS", true);
            Utility.StatusMessage result = AccountHolderService.TransferFund(currentAccountHolder, accountId, money, mode);
            ShowMessage(result);
        }
        else
            WriteLine("Enter valid ids");
        return;

    }

    private void ShowTransactionHistory(AccountHolder accountHolder)
    {
        AccountHolderService AccountHolderService = new AccountHolderService();

        WriteLine("\t\tTransactionId\t\t\t\t\t\tSrcAccountId\t\tDstAccountId\t\tCreatedBy\t\tCreatedOn\t\tAmount\t\tAction\t\t");

        AccountHolderService.ShowTransactionHistory(accountHolder.Id, accountHolder.BankId);
    }

   private void ShowMessage(Utility.StatusMessage result)
    {
        if (result == Utility.StatusMessage.Success)
            Utility.Message(true, "Transferd", "money");
        else if (result == Utility.StatusMessage.Balance)
            WriteLine("Not Enough balance");
        else if (result == Utility.StatusMessage.Failed)
            Utility.Message(false, "Transfer");
        else if (result == Utility.StatusMessage.WrongSelection)
            WriteLine("Enter correct mode");
    }

}