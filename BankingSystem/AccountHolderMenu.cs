using System;
using static System.Console;

public class AccountHolderMenu : BankingStaffMenu
{

	string? bankId;
	string? accountId;
	public new void HomePage(ref BankCreation bankCreation)
	{
		WriteLine("Enter the bankId");
		string? login_bank_id = ReadLine();
		WriteLine("Enter the AccountId");
		string? login_accountId = ReadLine();
		WriteLine("Enter the password");
		string? password = ReadLine();
		bool isValid = validate(login_bank_id!, login_accountId!, password!);
		if (isValid)
		{
			AccountService accountService = new AccountService();
			while (true)
			{
				WriteLine("\n\n*****Account Holder Menu****\n\n");
				WriteLine("Choose What You Want to Do\n");
				WriteLine("1 : Deposit Amount");
				WriteLine("2 : WithDraw Amount");
				WriteLine("3 : Transfer Funds");
				WriteLine("4 : Get Transaction History");
				WriteLine("5 : Exit ");

				object? input = ReadLine();
				string? inputString = input?.ToString();
				string? id;
                double money;
                switch (inputString)
				{
					case "1":
						WriteLine("Enter the AccountId ");
						id = ReadLine();
						WriteLine("Enter amoun to Deposit");
                        money = Convert.ToDouble(ReadLine());
                        accountService.deposit_money(id!,this.bankId!,money,false);
						break;
					case "2":
						WriteLine("Enter the AccountId");
						id = ReadLine();
                        WriteLine("Enter amount to WithDraw");
                        money = Convert.ToDouble(ReadLine());
                        accountService.withdraw_money(id!,this.bankId!,money,false);
						break;
					case "3":
						WriteLine("Enter bankid To which you want to transfer");
						string? bankId = ReadLine();
						WriteLine("Enter AccountId of the user you want to transfer money");
						string? accountId = ReadLine();
						WriteLine("Enter Amount You want to Transfer");
						double? amount = Convert.ToDouble(ReadLine());
						accountService.transfer_fund(this.bankId!, bankId!, this.accountId!, accountId!, amount,ref bankCreation);
						break;
					case "4":
						accountService.showTransactionHistory(this.accountId!,this.bankId!);
						break;
					case "5":
						return;
						
				}


			}
		}
		else
		{
			WriteLine("Invalid Credentials");
			return;
		}

		// Add Validation for Invalid Input


	}
	public bool validate(string bankId,string accountId , string password)
	{
		WriteLine(currentBankEmployee);
		foreach(BankDetailsOfEmployee ob in currentBankEmployee)
		{
			if (ob.bankId == bankId && ob.accountId == accountId && ob.password == password)
			{
				this.bankId = bankId;
				this.accountId = accountId;
				return true;
			}
		}
		return false;
	}
	
		
}


