using System;

public class BankDetailsOfEmployee
{
	public string? _accountId = default;
	public string? _password;
	public string? _name = default;
	public string? _dob = default;
	public string? _bankId = default;
	public double? _initialBalance = 0;
	public double? _curBalance;
	public TransactionDetails[] _transaction = new TransactionDetails[10]; 

	public void getAccountId(string name)
	{
		this._accountId = name.Substring(0, 3) + DateTime.Now.Date.ToOADate();
		Console.WriteLine(this._accountId);
	}

}


