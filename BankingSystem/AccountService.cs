using static System.Console;

public class AccountService 
{

    public void DepositMoney(string id, string bankId, string accountId2, string bankId2, double? moneyToDeposit, bool fundTransfer)
    {
        Transaction transaction = new Transaction();
        foreach (BankDetailsOfEmployee detail in GlobalDataService.currentBankEmployee)
        {
            if (detail?.AccountId == id && detail?.BankId == bankId)
            {
                detail.CurBalance += moneyToDeposit;
                transaction.Amount = moneyToDeposit;
                transaction.IsCredit = true;
                transaction.IsFundTransfer = fundTransfer;
                //if (fundTransfer)
                //{
                //    transaction.AccountId = accountId2;
                //    transaction.BankId = bankId2;
                //}
                //else
                //{
                    transaction.AccountId = id;
                    transaction.BankId = bankId;
                //}
                transaction.Id = "TXN" + detail.BankId + id + DateTime.Now.ToOADate();
                GlobalDataService.Transaction[GlobalDataService.TotalTransaction++] = transaction;
                WriteLine("Successfully Deposited");
                break;
            }
        }
    }

    public bool WithDrawMoney(string id, string bankId, string accountId2, string bankId2, double? moneyToWithDraw, bool fundTransfer) // account id
    {
        Transaction transaction = new Transaction();
        foreach (BankDetailsOfEmployee detail in GlobalDataService.currentBankEmployee)
        {
            if (detail?.AccountId == id && detail?.BankId == bankId)
            {
                if (detail.CurBalance < moneyToWithDraw)
                {
                    WriteLine("Not enough money . Cur balance:{0}", detail.CurBalance);
                    return false;
                }
                WriteLine("Successful withdraw");
                transaction.Amount = moneyToWithDraw;
                transaction.IsFundTransfer = fundTransfer;
                //if (fundTransfer)
                //{
                //    transaction.AccountId = accountId2;
                //    transaction.BankId = bankId2;
                //}
                //else
                //{
                    transaction.AccountId = id;
                    transaction.BankId = bankId;
                //}
                transaction.IsCredit = false;
                transaction.Id = "TXN" + detail.BankId + id + DateTime.Now.ToOADate();
                GlobalDataService.Transaction[GlobalDataService.TotalTransaction++] = transaction;
                detail.CurBalance -= moneyToWithDraw;
                return true;
            }
        }
        return false;
    }

    public void TransferFund(string bankId1, string bankId2, string accountId1, string accountId2, double? amount)
    {
        WriteLine("Which mode is this 1:RTGS , 2 : IMPS");
        int mode = Convert.ToInt32(Utility.GetInputString()); ;
        double? newAmount = amount;

        BankDetailModel model = Utility.GetBankDetails(bankId1);


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

        if (WithDrawMoney(accountId1, bankId1, accountId2, bankId2, newAmount, true))
            DepositMoney(accountId2, bankId2, accountId1, bankId1, amount, true);
        else
            WriteLine("Transaction failed");
    }

    public void ShowTransactionHistory(string id, string bankId)
    {
        BankingService bankingService = new BankingService();
        bankingService.ShowTransactionHistory(ref GlobalDataService.currentBankEmployee, id, bankId);
    }
}


