using static System.Console;

public class AccountService 
{

    public bool DepositMoney(string id, string bankId, double moneyToDeposit,string? senderName)
    {
        Transaction transaction = new Transaction();
        foreach (AccountHolder detail in GlobalDataService.AccountHolder)
        {
            if (detail?.Id == id && detail?.BankId == bankId)
            {
                detail.Balance += moneyToDeposit;
                transaction.Amount = moneyToDeposit;
                transaction.Type = true;
                transaction.AccountId = id;
                transaction.BankId = bankId;
                transaction.CreatedOn = DateTime.Now;
                transaction.CreatedBy = senderName??detail.Name;
                transaction.Id = "TXN" + detail.BankId + id + DateTime.Now.ToOADate();
                GlobalDataService.Transactions.Add(transaction);
                WriteLine("Successfully Deposited");
                return true;
            }
        }
        return false;
    }

    public bool WithDrawMoney(string id, string bankId, double moneyToWithDraw , string? senderName) // account id
    {
        Transaction transaction = new Transaction();
        foreach (AccountHolder detail in GlobalDataService.AccountHolder)
        {
            if (detail?.Id == id && detail?.BankId == bankId)
            {
                if (detail.Balance < moneyToWithDraw)
                {
                    WriteLine("Not enough money . Cur balance:{0}", detail.Balance);
                    return false;
                }
                transaction.Amount = moneyToWithDraw;
                transaction.AccountId = id;
                transaction.BankId = bankId;
                transaction.Type = false;
                transaction.CreatedOn = DateTime.Now;
                transaction.CreatedBy = senderName??detail.Name ;
                transaction.Id = "TXN" + detail.BankId + id + DateTime.Now.ToOADate();
                GlobalDataService.Transactions.Add(transaction);
                detail.Balance -= moneyToWithDraw;
                return true;
            }
        }
        return false;
    }

    public void TransferFund(string bankId1, string bankId2, string accountId1, string accountId2, double amount)
    {
        WriteLine("Which mode is this 1:RTGS , 2 : IMPS");
        int mode = Convert.ToInt32(Utility.GetInputString()); ;
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
            if (WithDrawMoney(accountId1,bankId1, newAmount,accountHolder1.Name))
                DepositMoney( accountId2,bankId2, amount,accountHolder1.Name);
            
        }
        else
            WriteLine("Transaction failed");
    }

    public void ShowTransactionHistory(string id, string bankId)
    {
        BankingService bankingService = new BankingService();
        bankingService.ShowTransactionHistory(id, bankId);
    }
}
