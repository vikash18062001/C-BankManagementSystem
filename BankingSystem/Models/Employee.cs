public class Employee : User
{
    public Employee()
    {
        this.BankId = string.Empty;
    }

    public double Salary { get; set; }

    public string BankId { get; set; }
}
