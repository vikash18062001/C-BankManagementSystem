public class AccountHolder : User
{
	public AccountHolder()
	{
		this.BankId = string.Empty;
	}

	public double Balance { get; set; }

	public string BankId { get; set; }

}


