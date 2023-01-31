using static System.Console;
public class AccountService : BankingStaffMenu
{
	int _curTransCount = 0;
	public void DepositMoney(string id , string bankId , double? moneyToDeposit , bool fundTransfer) // account id
	{
		TransactionDetails transactionDetails = new TransactionDetails();
		foreach(BankDetailsOfEmployee detail in currentBankEmployee)
		{
			if(detail?._accountId == id && detail?._bankId == bankId)
			{
				detail._curBalance += moneyToDeposit;
				WriteLine("Successfully Deposited");
				transactionDetails._accountId = id;
				transactionDetails._amount = moneyToDeposit;
				transactionDetails._bankId = detail._bankId;
				transactionDetails._isCredit = true;
				transactionDetails._isFundTransfer = fundTransfer;
				transactionDetails._transId = "TXN" + detail._bankId + id + DateTime.Now.ToOADate();
				detail._transaction[_curTransCount] = transactionDetails;
				_curTransCount++;
				break;
			}
		}
	}
    public void WithDrawMoney(string id , string bankId , double? moneyToWithDraw ,bool fundTransfer) // account id
    {
		TransactionDetails transactionDetails = new TransactionDetails();
        foreach (BankDetailsOfEmployee detail in currentBankEmployee)
        {
            if (detail?._accountId == id && detail?._bankId == bankId)
            {
				if (detail._curBalance < moneyToWithDraw)
				{
					WriteLine("Not Enough Money . Cur Balance:{0}", detail._curBalance);
					return;
				}
                WriteLine("Successful Withdraw");
                transactionDetails._accountId = id;
                transactionDetails._amount = moneyToWithDraw;
                transactionDetails._bankId = detail._bankId;
				transactionDetails._isFundTransfer = fundTransfer;
                transactionDetails._isCredit = false;
                transactionDetails._transId = "TXN" + detail._bankId + id + DateTime.Now.ToOADate();
                detail._transaction[_curTransCount] = transactionDetails;
				detail._curBalance -= moneyToWithDraw;
                _curTransCount++;
                break;
            }
        }
    }
	public void TransferFund(string bankId1,string bankId2 , string accountId1 , string accountId2,double? amount , ref BankCreation bankCreation)
	{
		WriteLine("Which Mode is this 1:RTGS , 2 : IMPS");
		int mode = Convert.ToInt32(ReadLine());
		double? newAmount = amount;
        int index = Array.IndexOf(bankCreation._bankIds, bankId1);


        if ( bankId1.Substring( 0, 3 ) == bankId2.Substring( 0, 3 ) )
		{
			if ( mode == 2 ) 
			{
				double charge = bankCreation._IMPSSame[index]/100;
				newAmount = amount + ( charge == 0.0 ? 0.05 :charge ) * amount;
			}
		}
		else
		{
            if ( mode == 1 )
			{
				double charge = bankCreation._RTGSDiff[index]/100;
				newAmount = amount + charge == 0.0 ? 0.02 : charge * amount;

			}
			else
			{
                double charge = bankCreation._IMPSDiff[index]/100;
				newAmount = amount + charge == 0.0 ? 0.06 : charge * amount;
            }
		}
		
		WithDrawMoney(accountId1,bankId1,newAmount,true);
		DepositMoney(accountId2, bankId2,amount,true);

	}

	public void ShowTransactionHistory(string id,string bankId)
	{
		BankingService bankingService = new BankingService();
		bankingService.ShowTransactionHistory(ref BankingStaffMenu.currentBankEmployee, id , bankId);
	}
}


