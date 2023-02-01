using static System.Console;

public class AccountService : BankingStaffMenu
{

    public void DepositMoney(string id, string bankId, string accountId2, string bankId2, double? moneyToDeposit, bool fundTransfer)
    {
        Transaction transaction = new Transaction();
        foreach (BankDetailsOfEmployee detail in currentBankEmployee)
        {
            if (detail?.AccountId == id && detail?.BankId == bankId)
            {
                detail.CurBalance += moneyToDeposit;
                transaction.Amount = moneyToDeposit;
                transaction.IsCredit = true;
                transaction.IsFundTransfer = fundTransfer;
                if (fundTransfer)
                {
                    transaction.AccountId = accountId2;
                    transaction.BankId = bankId2;
                }
                else
                {
                    transaction.AccountId = id;
                    transaction.BankId = bankId;
                }
                transaction.Id = "TXN" + detail.BankId + id + DateTime.Now.ToOADate();
                detail.Transaction[detail.TotalTransaction++] = transaction;
                WriteLine("Successfully Deposited");
                break;
            }
        }
    }

    public bool WithDrawMoney(string id, string bankId, string accountId2, string bankId2, double? moneyToWithDraw, bool fundTransfer) // account id
    {
        Transaction transaction = new Transaction();
        foreach (BankDetailsOfEmployee detail in currentBankEmployee)
        {
            if (detail?.AccountId == id && detail?.BankId == bankId)
            {
                if (detail.CurBalance < moneyToWithDraw)
                {
                    WriteLine("Not Enough Money . Cur Balance:{0}", detail.CurBalance);
                    return false;
                }
                WriteLine("Successful Withdraw");
                transaction.Amount = moneyToWithDraw;
                transaction.IsFundTransfer = fundTransfer;
                if (fundTransfer)
                {
                    transaction.AccountId = accountId2;
                    transaction.BankId = bankId2;
                }
                else
                {
                    transaction.AccountId = id;
                    transaction.BankId = bankId;
                }
                transaction.IsCredit = false;
                transaction.Id = "TXN" + detail.BankId + id + DateTime.Now.ToOADate();
                detail.Transaction[detail.TotalTransaction++] = transaction;
                detail.CurBalance -= moneyToWithDraw;
                return true;
            }
        }
        return false;
    }

    public void TransferFund(string bankId1, string bankId2, string accountId1, string accountId2, double? amount, ref CreatingBank bankCreation)
    {
        WriteLine("Which Mode is this 1:RTGS , 2 : IMPS");
        int mode = Convert.ToInt32(ReadLine());
        double? newAmount = amount;
        int index = Array.IndexOf(GlobalDataService.BankIds, bankId1);


        if (bankId1.Substring(0, 3) == bankId2.Substring(0, 3))
        {
            if (mode == 2)
            {
                double charge = GlobalDataService.IMPSSame[index] / 100;
                newAmount = amount + (charge == 0.0 ? 0.05 : charge) * amount;
            }
        }
        else
        {
            if (mode == 1)
            {
                double charge = GlobalDataService.RTGSDiff[index] / 100;
                newAmount = amount + (charge == 0 ? 0.02 : charge) * amount;

            }
            else
            {
                double charge = GlobalDataService.IMPSDiff[index] / 100;
                newAmount = amount + (charge == 0 ? 0.06 : charge) * amount;
            }
        }

        if (WithDrawMoney(accountId1, bankId1, accountId2, bankId2, newAmount, true))
            DepositMoney(accountId2, bankId2, accountId1, bankId1, amount, true);
        else
            WriteLine("Transaction Failed");
    }

    public void ShowTransactionHistory(string id, string bankId)
    {
        BankingService bankingService = new BankingService();
        bankingService.ShowTransactionHistory(ref BankingStaffMenu.currentBankEmployee, id, bankId);
    }
}


