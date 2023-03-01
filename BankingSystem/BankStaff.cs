using static System.Console;

public class BankingStaff
{

    BankingService BankingService = new BankingService();
    AccountHolderService AccountHolderService = new AccountHolderService();

    public void HomePage(LoginRequest login)
    {
        if (login != null)
        {
                WriteLine("\n\n****Banking Management System****\n\n1 : Creation of Account\n2 : Update an Account\n3 : Delete An Account\n4 : Add Service Charge For Same Bank\n5 : Add Service Charge For Different Bank\n6 : View Transaction History\n7 : Revert a Transaction\n8 : Return to previous menu");
                
                int option = Utility.GetIntInput("Choose what you want to do",true);
                switch (option)
                {
                    case 1:
                        this.CreateAccountHolder(login);
                        HomePage(login);
                        return ;

                    case 2:
                        this.UpdateAccount(login);
                        HomePage(login);
                        return;

                    case 3:
                        this.DeleteAccount(login);
                        HomePage(login);
                        return;

                    case 4:
                        this.ChangeServiceRate(true,login);
                        HomePage(login);
                        return;

                    case 5:
                        this.ChangeServiceRate(false,login);
                        HomePage(login);
                        return;

                    case 6:
                        this.ShowTransactionHistory(login);
                        HomePage(login);
                        return;

                    case 7:
                        this.RevertTransaction();
                        HomePage(login);
                        return;

                    case 8:
                            return;

                    default:
                        WriteLine("Enter correct value");
                        return;
                }
        }
        else
        {
            WriteLine("Enter correct details");
        }

        return;
    }

    private void CreateAccountHolder(LoginRequest login)
    { 
        try
        {
            AccountHolder Accountholder = new AccountHolder()
            {
                Name = Utility.GetInputString("Enter the name for the account", true),
                Email = Utility.GetInputEmail("Enter the email", true),
                Mobile = Utility.GetInputMobileNo("Enter the mobile no ", true),
                Password = Utility.GetPassword("Enter the password for the account", true),
                CreatedOn = DateTime.Now,
            };
            string bankId = BankingService.GetBankId(login.UserId);
            string createdBy = AccountService.GetAccountCreaterName(login.UserId);
            if(string.IsNullOrEmpty(createdBy) || string.IsNullOrEmpty(bankId))
            {
                WriteLine("Not able to create account please check your id");
                return;
            }    
            Accountholder.BankId = bankId;
            Accountholder.CreatedBy = createdBy;
            APIResponse  response = BankingService.CreateAccount(Accountholder);
            WriteLine(response.Message);
        }
        catch (Exception ex)
        {
            this.CreateAccountHolder(login);
        }
    }

    private void UpdateAccount(LoginRequest login)
    {
        AccountHolder accountHolder = this.GetAccountHolder(login);
        if (string.IsNullOrEmpty(accountHolder.Id))
            return;

        APIResponse apiResponse = BankingService.checkIfValidIdsOrNot(accountHolder.BankId, accountHolder.Id);
        WriteLine(apiResponse.Message);
        if (!apiResponse.IsSuccess)
            return;

        Dictionary<string, object> updatedData = new Dictionary<string, object>();
        updatedData.Add("Email", Utility.GetInputEmail("Enter email", true));
        updatedData.Add("Mobile", Utility.GetInputMobileNo("Enter the mobileno", true));

        APIResponse resposne = BankingService.UpdateAccount(accountHolder.Id, updatedData);
        WriteLine(apiResponse.Message);
    }

    private void DeleteAccount(LoginRequest login)
    {
        AccountHolder accountHolder = this.GetAccountHolder(login);
        if (string.IsNullOrEmpty(accountHolder.Id))
            return;

        APIResponse apiResponse = BankingService.checkIfValidIdsOrNot(accountHolder.BankId, accountHolder.Id);
        WriteLine(apiResponse.Message);
        if(apiResponse.IsSuccess)
        {
            APIResponse response = BankingService.DeleteAccount(accountHolder.Id);
            WriteLine(response.Message);
        }
    }

    private void ChangeServiceRate(bool isSame,LoginRequest login)
    {
        string bankId = BankingService.GetBankId(login.UserId);
        if(string.IsNullOrEmpty(bankId))
        {
            WriteLine("Please enter valid id");
            return;
        }
        Bank bankDetail = BankingService.GetBankDetail(bankId);
        if (bankDetail == null || string.IsNullOrEmpty(bankDetail.Id))
        {
            WriteLine("Cannot find the bank please enter valid id");
            return;
        }
        InitializeServiceRate(isSame, bankDetail);
        WriteLine("Sucessfully Changed the charges");

    }

    private void ShowTransactionHistory(LoginRequest login)
    {
        AccountHolder accountHolder = this.GetAccountHolder(login);
        if (string.IsNullOrEmpty(accountHolder.Id))
            return;

        string bankId = BankingService.GetBankId(login.UserId);
        WriteLine("\t\tTransactionId\t\t\t\t\t\tSrcAccountId\t\tDstAccountId\t\tCreatedBy\t\tCreatedOn\t\tAmount\t\tAction\t\t");
        List<Transaction> userTransactions= BankingService.GetTransactionHistory(accountHolder.Id, bankId);
        if (userTransactions.Count == 0)
            WriteLine("No Transaction.Check you Ids or you don't have any transaction");
        else
            this.ShowAllTransaction(userTransactions);

    }

    private void RevertTransaction()
    {
        string curId = Utility.GetInputString("Enter the accountId for which you want to revert the transaction", true)!;
        if (curId == null)
            return;

        string? transId = Utility.GetInputString("Enter the transId you want to revert", true);
        if (transId == null)
            return;

        APIResponse response = BankingService.RevertTransaction(transId,curId);
        WriteLine(response.Message);
    }

    public void ShowAllTransaction(List<Transaction> userTransactions)
    {
        foreach (Transaction transaction in userTransactions)
        {
            string action = transaction.Type ? "Credit" : "Debit";
            WriteLine("{0}\t{1}\t{2}\t\t{3}\t\t{4}\t\t{5}\t\t{6}", transaction.Id, transaction.SrcAccountId, transaction.DstAccountId, transaction.CreatedBy, transaction.CreatedOn, transaction.Amount, action);
        }
    }

    private void InitializeServiceRate(bool isSame , Bank bank)
    {
        if (isSame)
        {
            bank.RTGSSame = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for RTGS in percent "));
            bank.IMPSSame = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for IMPS in percent"));
        }
        else
        {
            bank.RTGSDiff = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for RTGS in percent "));
            bank.IMPSDiff = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for IMPS in percent "));
        }
    }



    public AccountHolder GetAccountHolder(LoginRequest login)
    {
        string curId = Utility.GetInputString("Enter your accountId", true);
        if (curId == null)
            return new AccountHolder();
        string empBankId = BankingService.GetBankId(login.UserId);

        AccountHolder accountHolder = AccountHolderService.GetAccountHolder(curId);
        if (accountHolder == null || string.IsNullOrEmpty(accountHolder.BankId) || accountHolder.BankId != empBankId)
        {
            WriteLine("No account found check the Id");
            return new AccountHolder();
        }

        return accountHolder;
    }

}
