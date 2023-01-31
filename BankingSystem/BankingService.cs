using System;
using static System.Console;
public class BankingService
{
	
	public BankDetailsOfEmployee create_account(string bankId)
	{
		BankDetailsOfEmployee bankDetailsOfEmployee = new BankDetailsOfEmployee();
		WriteLine("Enter the Name");
		string? name = ReadLine();
		WriteLine("Enter the dob");
		string? dob = ReadLine();
		WriteLine("Enter InitialBalance");
		int? initialBalance = Convert.ToInt32(ReadLine());
		WriteLine("Enter the password You want for this account");
		string? password = ReadLine();

		bankDetailsOfEmployee.name = name;
		bankDetailsOfEmployee.dob = dob;
		bankDetailsOfEmployee.initialBalance = initialBalance;
        bankDetailsOfEmployee.curBalance= initialBalance;
		bankDetailsOfEmployee.bankId = bankId;
		bankDetailsOfEmployee.password = password;
        bankDetailsOfEmployee.getAccountId(name!);
		return bankDetailsOfEmployee;
		
	}

	public void update_account(ref BankDetailsOfEmployee[] ob,string id)
	{
		foreach(BankDetailsOfEmployee obj in ob)
		{
			if(obj?.accountId == id)
			{
				if (obj?.accountId == null)
					break;
                Console.WriteLine("Enter the new Name");
                obj.name = ReadLine();
                WriteLine("Enter the new dob");
                obj.dob = ReadLine();
            }
		}
		
	}
	public void delete_account(ref BankDetailsOfEmployee[] ob, string id)
	{
		ob = Array.FindAll(ob, i => i?.accountId != id);
    }
	public void showTransactionHistory(ref BankDetailsOfEmployee[] ob,string id , string bankId)
	{
        foreach (BankDetailsOfEmployee obj in ob)
        {
            if (obj?.accountId == id && obj?.bankId == bankId)
            {
                if (obj?.accountId == null)
                    break;
				WriteLine("TransactionId\t\tBankId\t\tAccountId\t\tAmount\t\t Action\t\t");
				foreach(TransactionDetails transaction in obj.transaction)
				{
					
					if (transaction?.transId == null)
						continue;
                    if (transaction?.accountId == null)
                        break;
                    string action = transaction.isCredit ? "Credit" : "Debit";
					WriteLine("{0}\t{1}\t\t{2}\t\t{3}\t\t{4}", transaction.transId,transaction.bankId, transaction.accountId, transaction.amount, action);
				}
                
            }
        }
    }
    public void revertTransaction(ref BankDetailsOfEmployee[] ob, string id, string bankId)
    {
		//have to implement
		WriteLine("Enter the TransId You want to Revert");
		string? trans_id = ReadLine();

        foreach (BankDetailsOfEmployee obj in ob)
        {
            if (obj?.accountId == id && obj?.bankId == bankId)
            {
                if (obj?.accountId == null)
                    break;
                foreach (TransactionDetails transaction in obj.transaction)
                {
                    if (transaction?.accountId == null)
                        break;
                    if(transaction?.transId == trans_id)
					{
						transaction!.transId = null;
						if (transaction.isCredit)
						{
							obj.curBalance -= transaction.amount;
						}
						else
							obj.curBalance += transaction.amount;
					}
                }

            }
        }
        
    }
    public void showAll(ref BankDetailsOfEmployee[] ob , int cursize)
	{
		if (ob[0]?.accountId != null)
		{
			foreach (BankDetailsOfEmployee obj in ob)
			{
				if (obj?.accountId == null)
					break;
				WriteLine(obj.accountId);
				WriteLine(obj.name);
				WriteLine(obj.dob);
				WriteLine(obj.curBalance);

			}
		}
		return;
    }

	//public void exchangeCurRate(ref BankCreation bankCreation , string bankId)
	//{
	//	WriteLine("Enter the currency In Short form i.e INR");
	//	string? cur = ReadLine();
	//	WriteLine("Enter the ExchangeRate to INR");
	//	double? rate = Convert.ToDouble(ReadLine());
	//	Dictionary<string, double> dict = new Dictionary<string, double>();
	//	dict[cur] = rate;
	//	bankCreation.exchangeRate[bankId].
	//}
}
