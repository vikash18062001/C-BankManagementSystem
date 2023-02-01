using static System.Console;

public class CreatingBank
{

	string? BankId;
	int CurCount = 0;
	
	public void CreateBank()
	{
		WriteLine("Enter the bankname at least it would be of three letter");
		string? bankName = Utility.GetInputString();

		if (bankName!.Length < 3)
		{
			WriteLine("Even after telling did not entered valid name.Sir please enter a valid name");
			return;
		}

		this.BankId = bankName?.Substring(0, 3) + DateTime.Now.ToOADate().ToString();
		WriteLine("Enter bank creator name");
		string? createrName = Utility.GetInputString();
		WriteLine("Enter bank creator password");
		string? password = Utility.GetInputString();
		WriteLine(this.BankId);

		foreach (BankDetailModel model in GlobalDataService.BankDetails)
		{
			if (model?.BankId !=null && model.BankId == this.BankId && model.BankName == bankName)
			{
				WriteLine("Bank already exist");
				return;
			}
		}

		BankDetailModel detailModelObj = new BankDetailModel();
		detailModelObj.BankId = this.BankId;
		detailModelObj.BankName = bankName;
		detailModelObj.BankCreaterName = createrName!;
		detailModelObj.Password = password!;
		GlobalDataService.BankDetails[CurCount] = detailModelObj;
		CurCount++;
		WriteLine("Successfully created a bank");
	}
}		



