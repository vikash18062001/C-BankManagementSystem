 using static System.Console;

public class AccountHolderMenu
{
    AccountHolderService AccountHolderService = new AccountHolderService();
    BankingService BankingService = new BankingService();

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

        AccountHolder accountDetail = this.AccountHolderService.GetAccountHolder(login.UserId);

        WriteLine("\n\n*****Account Holder Menu*****\n\n1 : Deposit amount\n2 : WithDraw amount\n3 : Transfer funds\n4 : Get transaction history\n5 : Return to previous menu");

        int option = Utility.GetIntInput("Choose what you want to do", true);

        switch (option)
        {
            case 1:
                this.Deposit(accountDetail);
                Menu(login);
                return;

            case 2:
                this.Withdraw(accountDetail);
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

    private void Deposit(AccountHolder accountHolder)
    {
        double amount = Utility.GetDoubleAmount("Enter the amount to deposit");

        Transaction transaction = new Transaction();
        transaction.SrcAccountId = accountHolder.Id;
        transaction.Amount = amount;
        transaction.CreatedBy = accountHolder.Name;
        transaction.CreatedOn = DateTime.Now;
        transaction.Type = true;
        transaction.Id = $"TXN/{accountHolder.BankId}/{accountHolder.Id}/{DateTime.Now.ToOADate()}";

        APIResponse apiResponse = BankingService.Deposit(transaction);
        WriteLine(apiResponse.Message);
    }

    private void Withdraw(AccountHolder accountHolder)
    {
        double amount = Utility.GetDoubleAmount("Enter the amount to withdraw");

        Transaction transaction = new Transaction();
        transaction.SrcAccountId = accountHolder.Id;
        transaction.Amount = amount;
        transaction.CreatedBy = accountHolder.Name;
        transaction.CreatedOn = DateTime.Now;
        transaction.Type = false;
        transaction.Id = $"TXN/{accountHolder.BankId}/{accountHolder.Id}/{DateTime.Now.ToOADate()}";

        APIResponse apiResponse = BankingService.WithDraw(transaction);
        WriteLine(apiResponse.Message);
    }

    private void TransferFunds(AccountHolder currentAccountHolder)
    {
        string bankId = Utility.GetInputString("Enter the bankid to which you want to transfer", true);
        string accountId = Utility.GetInputString("Enter accountId of the user you want to transfer money", true);
        double money = Utility.GetDoubleAmount("Enter amount you want to transfer");

        if (accountId == currentAccountHolder.Id)
        {
            WriteLine("Cannot transfer to the same account");
            return;
        }

        APIResponse response = BankingService.checkIfValidIdsOrNot(currentAccountHolder.BankId, currentAccountHolder.Id);
        if (response.IsSuccess)
        {
            APIResponse response2 = BankingService.checkIfValidIdsOrNot(bankId, accountId);
            if (response2.IsSuccess)
            {
                int mode = Utility.GetIntInput("Which mode is this 1:RTGS , 2 : IMPS", true);
                APIResponse response3 = AccountHolderService.MakeTransferFundObject(currentAccountHolder, accountId, money, mode);
                WriteLine(response3.Message);
            }
            else
                WriteLine(response2.Message);
        }
        else
            WriteLine(response.Message);
    }

    private void ShowTransactionHistory(AccountHolder accountHolder)
    {
        AccountHolderService AccountHolderService = new AccountHolderService();

        WriteLine("\t\tTransactionId\t\t\t\t\t\tSrcAccountId\t\tDstAccountId\t\tCreatedBy\t\tCreatedOn\t\tAmount\t\tAction\t\t");

        List<Transaction> userTransaction = AccountHolderService.GetTransactionHistory(accountHolder.Id, accountHolder.BankId);
        if (userTransaction.Count() != 0)
            ShowAllTransaction(userTransaction);
        else
            WriteLine("No transaction found");
    }

    public void ShowAllTransaction(List<Transaction> userTransactions)
    {
        foreach (Transaction transaction in userTransactions)
        {
            string action = transaction.Type ? "Credit" : "Debit";
            WriteLine("{0}\t{1}\t{2}\t\t{3}\t\t{4}\t\t{5}\t\t{6}", transaction.Id, transaction.SrcAccountId, transaction.DstAccountId, transaction.CreatedBy, transaction.CreatedOn, transaction.Amount, action);
        }
    }

}