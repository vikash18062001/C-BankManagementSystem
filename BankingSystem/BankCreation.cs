using System;
using static System.Console;

public class BankCreation
{
	public string[] bankIds = new string[10];
	public string[] bankNames = new string[10];
	public string[] bankCreaterName = new string[10];
	public string[] passwords = new string[10];
	public double[] RTGSSame = new double[10];
    public double[] RTGSDiff = new double[10];
    public double[] IMPSSame = new double[10];
    public double[] IMPSDiff = new double[10];
	public Dictionary<string, Dictionary<string, double>> exchangeRate = new Dictionary<string, Dictionary<string, double>>();



    string? bankId;
	int curCount = 0;
	
	public void create_bank()
	{
		WriteLine("Enter the BankName");
		string? bankName = ReadLine();
		this.bankId = bankName?.Substring(0, 3) + DateTime.Now.Date.ToOADate();
		WriteLine("Enter bank creator name");
		string? createrName = ReadLine();
        WriteLine("Enter bank creator Password");
        string? password = ReadLine();
		WriteLine(this.bankId);

        if (bankNames.Contains(bankName) && bankIds.Contains(bankId))
			WriteLine("Bank Already Exist");
		else if(bankName?.Length > 0)
		{
			this.bankIds[curCount] = this.bankId;
			this.bankNames[curCount] = bankName;
			this.bankCreaterName[curCount] = createrName!;
			this.passwords[curCount] = password!;
			curCount++;
			WriteLine("Successfully Created A Bank");
;		}
	}		
}


