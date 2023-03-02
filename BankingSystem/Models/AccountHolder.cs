public class AccountHolder : User
{
	public AccountHolder()
	{
		this.BankId = string.Empty;
	}

	public string AccountHolderID { get; set; }

	public string BankId { get; set; }

	public double Balance { get; set; }
}