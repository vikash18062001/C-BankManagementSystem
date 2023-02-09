using static System.Console;

public class BankingStaff
{

    BankingService BankingService = new BankingService();

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
                        this.UpdateAccount();
                        HomePage(login);
                        return;

                    case 3:
                        this.DeleteAccount();
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
                BankId = Utility.GetBankId(login.UserId),
                CreatedOn = DateTime.Now,
                CreatedBy = Utility.GetAccountCreaterName(login.UserId),
            };

            Accountholder = BankingService.CreateAccount(Accountholder);
            if (Accountholder.Id == null)
            {
                WriteLine("Account Creation Unsuccessful");
                this.CreateAccountHolder(login);
            }
            else
                Utility.Message(true, "created");
        }
        catch (Exception ex)
        {
            this.CreateAccountHolder(login);
        }
    }

    private void UpdateAccount()
    {
        string curId = Utility.GetInputString("Enter your accountId", true);
        if (curId == null)
            return;

        string bankId = Utility.GetAccountDetail(curId).BankId;

        if (!Utility.checkIfValidIdsOrNot(bankId, curId))
        {
            WriteLine("No account Found");
                return;
        }


        Dictionary<string, object> updatedData = new Dictionary<string, object>();

        updatedData.Add("Email", Utility.GetInputEmail("Enter email", true));

        updatedData.Add("Mobile", Utility.GetInputMobileNo("Enter the mobileno", true));

        if (BankingService.UpdateAccount(curId, updatedData))
            Utility.Message(true, "updated");
        else
            Utility.Message(false, "Updation");
    }

    private void DeleteAccount()
    {

        string curId = Utility.GetInputString("Enter your accountId", true);

        if (curId == null)
            return;

        string bankId = Utility.GetAccountDetail(curId).BankId;

        if (!Utility.checkIfValidIdsOrNot(bankId, curId))
        {
            WriteLine("No account Found");
            return;
        }

        if (BankingService.DeleteAccount(curId))
            Utility.Message(true, "deleted");
        else
            Utility.Message(false, "Deletion");

    }

    private void ChangeServiceRate(bool isSame,LoginRequest login)
    {
        string curId = Utility.GetInputString("Enter the bankId you want to change service", true);

        string bankId = Utility.GetBankId(login.UserId);

        if(curId != bankId)
        {
            WriteLine("Please enter valid id");
            return;
        }

        Bank bankDetail = Utility.GetBankDetails(curId);

        if (string.IsNullOrEmpty(bankDetail.Id))
            return;

        if (isSame)
        {
            bankDetail.RTGSSame = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for RTGS in percent "));
            bankDetail.IMPSSame = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for IMPS in percent"));
        }
        else
        {
            bankDetail.RTGSDiff = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for RTGS in percent "));
            bankDetail.IMPSDiff = Convert.ToDouble(Utility.GetInputServiceCharge("Enter serive charge for IMPS in percent "));
        }
    }

    private void ShowTransactionHistory(LoginRequest login)
    {
        string curId = Utility.GetInputString("Enter the accountId for which you want to get transaction history", true)!;
        if (curId == null)
            return ;

        string newBankId = Utility.GetAccountDetail(curId).BankId;

        string bankId = Utility.GetBankId(login.UserId);

        if (newBankId != bankId)
        {
            WriteLine("No account found");
            return;
        }

        WriteLine("\t\tTransactionId\t\t\t\t\t\tSrcAccountId\t\tDstAccountId\t\tCreatedBy\t\tCreatedOn\t\tAmount\t\tAction\t\t");

        if(!BankingService.ShowTransactionHistory(curId, bankId))
            WriteLine("No Transaction.Check you Ids or you don't have any transaction");

    }

    private void RevertTransaction()
    {
        string curId = Utility.GetInputString("Enter the accountId for which you want to revert the transaction", true)!;
        if (curId == null)
            return;

        string? transId = Utility.GetInputString("Enter the transId you want to revert", true);
        if (transId == null)
            return;

        Utility.StatusMessage result = BankingService.RevertTransaction(curId,transId);
        if (result == Utility.StatusMessage.Success)
            WriteLine("Successfully Revert Money");
        else if (result == Utility.StatusMessage.Failed)
            WriteLine("Some error occured due to which transfer is unsucessful");
        else if (result == Utility.StatusMessage.Credential)
            WriteLine("Enter valid credentail");
    }

}
