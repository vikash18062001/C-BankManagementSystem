using System.Security.Cryptography;
using static System.Console;

public class AccountService 
{

    public bool DepositMoney(Dictionary<string,object> credential)
    {
        Transaction transaction = new Transaction();
        foreach (AccountHolder detail in GlobalDataService.AccountHolder)
        {
            if (detail?.Id == credential["AccountId2"].ToString() && detail?.BankId == credential["BankId2"].ToString())
            {
                detail.Balance += Convert.ToDouble(credential["Amount"]);
                transaction.Amount = Convert.ToDouble(credential["Amount"]);
                transaction.Type = true;
                transaction.CreatedOn = DateTime.Now;
                transaction.isFundTransfer = (bool)credential["IsFundTransfer"];
                transaction.CreatedBy = credential["Name"]?.ToString() ?? detail.Name;
                if ((bool)credential["IsFundTransfer"])
                {
                    transaction.AccountId = credential["AccountId"].ToString();
                    transaction.BankId = credential["BankId"].ToString();
                }
                else
                {
                    transaction.AccountId = credential["AccountId2"].ToString();
                    transaction.BankId = credential["BankId2"].ToString();
                }
                //transaction.Id = "TXN/" + detail.BankId + credential["Id"].ToString() + DateTime.Now.ToOADate();
                transaction.Id = $"TXN/{detail.BankId}/{detail.Id}/{DateTime.Now.ToOADate()}";
                GlobalDataService.Transactions.Add(transaction);
                
                return true;
            }
        }
        return false;
    }

    public bool WithDrawMoney(Dictionary<string, object> credential) // account id
    {
        Transaction transaction = new Transaction();
        foreach (AccountHolder detail in GlobalDataService.AccountHolder)
        {
            if (detail?.Id == credential["AccountId"].ToString() && detail?.BankId == credential["BankId"].ToString())
            {
                if (detail.Balance < Convert.ToDouble(credential["Amount"]))
                {
                    WriteLine("Not enough money . Cur balance:{0}", detail.Balance);
                    return false;
                }
                transaction.Amount = Convert.ToDouble(credential["Amount"]);
                if ((bool)credential["IsFundTransfer"])
                {
                    transaction.AccountId = credential["AccountId2"].ToString();
                    transaction.BankId = credential["BankId2"].ToString();
                }
                else
                {
                    transaction.AccountId = credential["AccountId"].ToString();
                    transaction.BankId = credential["BankId"].ToString();
                }
                transaction.Type = false;
                transaction.CreatedOn = DateTime.Now;
                transaction.CreatedBy = credential["Name"]?.ToString() ?? detail.Name ;
                transaction.isFundTransfer = (bool)credential["IsFundTransfer"];
                transaction.Id = $"TXN/{detail.BankId}/{detail.Id}/{DateTime.Now.ToOADate()}";
                GlobalDataService.Transactions.Add(transaction);
                   detail.Balance -= Convert.ToDouble(credential["Amount"]);
                return true;
            }
        }
        return false;
    }

    public void TransferFund(string bankId1, string bankId2, string accountId1, string accountId2, double amount)
    {
        int mode = Utility.GetIntInput("Which mode is this 1:RTGS , 2 : IMPS",true); 
        double newAmount = amount;

        Bank model = Utility.GetBankDetails(bankId1);
       
        if (bankId1.Substring(0, 3) == bankId2.Substring(0, 3))
        {
            if (mode == 2)
            {
                double charge = model.IMPSSame / 100;
                newAmount = amount + (charge == 0.0 ? 0.05 : charge) * amount;
            }
        }
        else
        {
            if (mode == 1)
            {
                double charge = model.RTGSDiff / 100;
                newAmount = amount + (charge == 0 ? 0.02 : charge) * amount;
            }
            else
            {
                double charge = model.IMPSDiff / 100;
                newAmount = amount + (charge == 0 ? 0.06 : charge) * amount;
            }
        }

        if (Utility.checkIfValidIdsOrNot(bankId1, accountId1) && Utility.checkIfValidIdsOrNot(bankId2, accountId2))
        {
            AccountHolder accountHolder1 = Utility.GetDetails(accountId1, bankId1);
            Dictionary<string, object> data = getDictionary(accountId1,accountId2, bankId1,bankId2, newAmount, accountHolder1.Name, true);
            if (WithDrawMoney(data))
            {
                data["Amount"] = amount;
                DepositMoney(data);
                Utility.Message(true,"Transferd", "money");
            }
            else
            {
                Utility.Message(false, "Transfer");
            }
            
            
        }
        else
            WriteLine("Transaction failed");
    }

    public void ShowTransactionHistory(string id, string bankId)
    {
        BankingService bankingService = new BankingService();
        bankingService.ShowTransactionHistory(id, bankId);
    }

    public AccountHolder ValidateAccountDetails(string bankId, string accountId, string password)
    {
        WriteLine(GlobalDataService.AccountHolder);
        foreach (AccountHolder detail in GlobalDataService.AccountHolder)
        {
            if (detail != null && detail.BankId == bankId && detail.Id == accountId && detail.Password == password)
            {
                return detail;
            }
        }

        return null;
    }

    public Dictionary<string,object> getDictionary(string senderAccountId, string receiverAccountId, string senderBankId, string receiverBankId,   double amount,string name , bool isTransfer)
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("AccountId", senderAccountId);
        data.Add("BankId", senderBankId);
        data.Add("Amount", amount);
        data.Add("Name", name);
        data.Add("IsFundTransfer", isTransfer);
        data.Add("BankId2", receiverBankId);
        data.Add("AccountId2", receiverAccountId);


        return data;
    }
}

//vik44963.50194956019
//vik44963.50226185185