using System;
using static System.Console;

public class AccountHolderMenu : BankingStaffMenu
{

	string? _bankId;
	string? _accountId;
	public new void HomePage(ref BankCreation bankCreation)
	{
		WriteLine("Enter the _bankId");
		string? loginBankId = ReadLine();
		WriteLine("Enter the AccountId");
		string? loginAccountId = ReadLine();
		WriteLine("Enter the password");
		string? password = ReadLine();
		bool _IsValid = Validate(loginBankId!, loginAccountId!, password!);
		if (_IsValid)
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
                        accountService.DepositMoney(id!,this._bankId!,money,false);
						break;
					case "2":
						WriteLine("Enter the AccountId");
						id = ReadLine();
                        WriteLine("Enter amount to WithDraw");
                        money = Convert.ToDouble(ReadLine());
                        accountService.WithDrawMoney(id!,this._bankId!,money,false);
						break;
					case "3":
						WriteLine("Enter _bankId To which you want to transfer");
						string? bankId = ReadLine();
						WriteLine("Enter AccountId of the user you want to transfer money");
						string? accountId = ReadLine();
						WriteLine("Enter Amount You want to Transfer");
						double? amount = Convert.ToDouble(ReadLine());
						accountService.TransferFund(this._bankId!, bankId!, this._accountId!, accountId!, amount,ref bankCreation);
						break;
					case "4":
						accountService.ShowTransactionHistory(this._accountId!,this._bankId!);
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
	}
	public bool Validate(string bankId,string accountId , string password)
	{
		WriteLine(currentBankEmployee);
		foreach(BankDetailsOfEmployee detail in currentBankEmployee)
		{
			if (detail._bankId == bankId && detail._accountId == accountId && detail._password == password)
			{
				this._bankId = bankId;
				this._accountId = accountId;
				return true;
			}
		}
		return false;
	}		
}


