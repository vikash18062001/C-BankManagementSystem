public class Transaction
{
	public string? Id;
	public string? BankId;
	public string? AccountId;
    public double? Amount;
	public bool IsCredit;
	public bool IsFundTransfer;
	public Transaction()
	{
		this.Id = String.Empty;
		this.BankId = String.Empty;
		this.AccountId = String.Empty;
        this.Amount = 0;
		this.IsCredit = false;
		this.IsFundTransfer = false;
	}
}


