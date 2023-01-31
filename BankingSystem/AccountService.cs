using System;
using static System.Console;
public class AccountService : BankingStaffMenu
{
	//double curBalance;
	int curTransCount = 0;
	public void deposit_money(string id , string bankId , double? moneyToDeposit , bool FundTransfer) // account id
	{
		TransactionDetails td = new TransactionDetails();
		foreach(BankDetailsOfEmployee ob in currentBankEmployee)
		{
			if(ob?.accountId == id && ob?.bankId == bankId)
			{
				ob.curBalance += moneyToDeposit;
				WriteLine("Successfully Deposited");
				td.accountId = id;
				td.amount = moneyToDeposit;
				td.bankId = ob.bankId;
				td.isCredit = true;
				td.isFundTransfer = FundTransfer;
				td.transId = "TXN" + ob.bankId + id + DateTime.Now.ToOADate();
				ob.transaction[curTransCount] = td;
				curTransCount++;
				//WriteLine(ob.transaction.Append(td));
				
				//ob.transaction.Append(td);
				break;
			}
		}
	}
    public void withdraw_money(string id , string bankId , double? moneyToWithDraw ,bool fundTransfer) // account id
    {
		TransactionDetails td = new TransactionDetails();
        foreach (BankDetailsOfEmployee ob in currentBankEmployee)
        {
            if (ob?.accountId == id && ob?.bankId == bankId)
            {
				if (ob.curBalance < moneyToWithDraw)
				{
					WriteLine("Not Enough Money . Cur Balance:{0}", ob.curBalance);
					return;
				}
                WriteLine("Successful Withdraw");
                td.accountId = id;
                td.amount = moneyToWithDraw;
                td.bankId = ob.bankId;
				td.isFundTransfer = fundTransfer;
                td.isCredit = false;
                td.transId = "TXN" + ob.bankId + id + DateTime.Now.ToOADate();
                ob.transaction[curTransCount] = td;
				ob.curBalance -= moneyToWithDraw;
                curTransCount++;
                break;
            }
        }
    }
	public void transfer_fund(string bankId1,string bankid2 , string accountId1 , string accountid2,double? amount , ref BankCreation bankCreation)
	{
		WriteLine("Which Mode is this 1:RTGS , 2 : IMPS");
		int mode = Convert.ToInt32(ReadLine());
		double? newamount = amount;
        int index = Array.IndexOf(bankCreation.bankIds, bankId1);


        if (bankId1.Substring(0, 3) == bankid2.Substring(0, 3))
		{
			if (mode == 2)
			{
				double charge = bankCreation.IMPSSame[index];

                newamount = amount + (charge==0 ? 0.05 :charge) * amount;
			}
		}
		else
		{
            if (mode == 1)
			{
            double charge = bankCreation.RTGSDiff[index];
				newamount = amount + charge==0 ? 0.02 : charge * amount;

			}
			else
			{
                double charge = bankCreation.IMPSDiff[index];
				newamount = amount + charge==0 ? 0.06 : charge * amount;
            }
		}
		
		withdraw_money(accountId1,bankId1,newamount,true);
		deposit_money(accountid2, bankid2,amount,true);

	}

	public void showTransactionHistory(string id,string bankId)
	{
		BankingService bankingService = new BankingService();
		bankingService.showTransactionHistory(ref BankingStaffMenu.currentBankEmployee, id , bankId);
	}
}


