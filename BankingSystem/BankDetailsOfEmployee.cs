using System;

public class BankDetailsOfEmployee
{
	public string? accountId = default;
	public string? password;
	public string? name = default;
	public string? dob = default;
	public string? bankId = default;
	public double? initialBalance = 0;
	public double? curBalance; // Initialize it with initalBalance 
	public TransactionDetails[] transaction = new TransactionDetails[10]; 

	public void getAccountId(string name)
	{
		this.accountId = name.Substring(0, 3) + DateTime.Now.Date.ToOADate();
		Console.WriteLine(this.accountId);
	}

}


