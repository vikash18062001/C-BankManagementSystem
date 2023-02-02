public class Transaction
{
    public Transaction()
    {
        this.Id = String.Empty;

        this.BankId = String.Empty;

        this.AccountId = String.Empty;

    }
    public string Id { get; set; } // Transaction Id

	public string BankId { get; set; }

	public string AccountId { get; set; }

    public double Amount { get; set; }

	public bool Type { get; set; } // Is it a credit or debit

    public DateTime CreatedOn { get; set; }

    public string? CreatedBy { get; set; }
	
}


