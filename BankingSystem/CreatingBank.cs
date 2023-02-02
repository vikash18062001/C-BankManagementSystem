//checked
using static System.Console;

public class CreatingBank
{ 
	string BankId { get; set; }

	public CreatingBank()
	{
		this.BankId = string.Empty;
	}
	
	public void CreateBank()
	{
		WriteLine("Enter the bankname at least it would be of three letter");
		string? bankName = Utility.GetInputString();

		if (Utility.isNull(bankName))
		{
			WriteLine("Enter valid name it cannot be empty");
			return;
		}

		if (bankName?.Length < 3)
		{
			WriteLine("Please enter valid name");
			return;
		}
		this.BankId = bankName?.Substring(0, 3) + DateTime.Now.ToOADate().ToString();

		WriteLine("Enter bank creator name");
		string? createrName = Utility.GetInputString();

		if(Utility.isNull(createrName))
		{
			WriteLine("Enter valid name it cannot be empty");
			return;
		}

		WriteLine("Enter bank creator password");
		string? password = Utility.GetInputString();

        if (Utility.isNull(password))
        {
            WriteLine("Enter valid name it cannot be empty");
			return;
        }
        WriteLine(this.BankId);

		foreach (Bank bank in GlobalDataService.Bank)
		{
			if (bank?.Id !=null && bank.Id == this.BankId && bank.Name == bankName)
			{
				WriteLine("Bank already exist");
				return;
			}
		}

		Bank bankObj = new Bank();
		bankObj.Id = this.BankId;
		bankObj.Name = bankName!;
		bankObj.CreaterName = createrName!;
		bankObj.Password = password!;
		GlobalDataService.Bank.Add(bankObj);

		WriteLine("Successfully created a bank");
	}


}

//a
//viv44959 .72081893519




//b
//    vik44959.72016200231
//vik44959.722017002314


// change the trnsaction history action,created by and created on 