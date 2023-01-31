using System;
using static System.Console;

public class BankCreation
{
	public string[] _bankIds = new string[10];
	public string[] _bankNames = new string[10];
	public string[] _bankCreaterName = new string[10];
	public string[] _passwords = new string[10];
	public double[] _RTGSSame = new double[10];
    public double[] _RTGSDiff = new double[10];
    public double[] _IMPSSame = new double[10];
    public double[] _IMPSDiff = new double[10];
	public Dictionary<string, Dictionary<string, double>> exchangeRate = new Dictionary<string, Dictionary<string, double>>();



    string? _bankId;
	int _curCount = 0;
	
	public void CreateBank()
	{
		WriteLine("Enter the BankName");
		string? bankName = ReadLine();
		this._bankId = bankName?.Substring(0, 3) + DateTime.Now.Date.ToOADate();
		WriteLine("Enter bank creator name");
		string? createrName = ReadLine();
        WriteLine("Enter bank creator Password");
        string? password = ReadLine();
		WriteLine(this._bankId);

        if (_bankNames.Contains(bankName) && _bankIds.Contains(_bankId))
			WriteLine("Bank Already Exist");
		else if(bankName?.Length > 0)
		{
			this._bankIds[_curCount] = this._bankId;
			this._bankNames[_curCount] = bankName;
			this._bankCreaterName[_curCount] = createrName!;
			this._passwords[_curCount] = password!;
			_curCount++;
			WriteLine("Successfully Created A Bank");
;		}
	}		
}


