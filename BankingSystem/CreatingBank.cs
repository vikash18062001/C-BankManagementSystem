using static System.Console;

public class CreatingBank
{

	//public GlobalDataService GlobalDataService = new GlobalDataService();


    string? BankId;
	int CurCount = 0;
	
	public void CreateBank()
	{
		WriteLine("Enter the BankName At Least it Would Be of Three Letter");
		string? bankName = ReadLine();
		if (bankName!.Length < 3)
		{
			WriteLine("Even After Telling Did not Entered Valid Name.Sir Please Enter a Valid Name");
			return;
		}
		this.BankId = bankName?.Substring(0, 3) + DateTime.Now.Date.ToOADate();
		WriteLine("Enter bank creator name");
		string? createrName = ReadLine();
        WriteLine("Enter bank creator Password");
        string? password = ReadLine();
		WriteLine(this.BankId);

        if (GlobalDataService.BankNames.Contains(bankName) && GlobalDataService.BankIds.Contains(BankId))
			WriteLine("Bank Already Exist");
		else if(bankName?.Length > 0)
		{
			GlobalDataService.BankIds[CurCount] = this.BankId;
			GlobalDataService.BankNames[CurCount] = bankName;
			GlobalDataService.BankCreaterName[CurCount] = createrName!;
			GlobalDataService.Passwords[CurCount] = password!;
			CurCount++;
			WriteLine("Successfully Created A Bank");
;		}
	}		
}


