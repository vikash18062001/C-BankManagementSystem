public class BankDetailsOfEmployee
{
    public string? AccountId ;
	public string? Password;
	public string? Name;
	public string? DOB ;
	public string? BankId ;
	public double? InitialBalance ;
	public double? CurBalance;

    public BankDetailsOfEmployee()
    {
        this.AccountId = String.Empty;
        this.Password = String.Empty;
        this.Name= String.Empty;
		this.DOB = String.Empty;
		this.BankId = String.Empty;
		this.InitialBalance = 0;
		this.CurBalance = 0;
    }

	

	public void GetAccountId(string name)
	{
		this.AccountId = name.Substring(0, 3) + DateTime.Now.ToOADate().ToString();
		Console.WriteLine(this.AccountId);
	}

}


