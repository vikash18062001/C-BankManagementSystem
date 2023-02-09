public static class GlobalDataService
{
    public static List<Bank> Banks = new List<Bank>();
    public static List<Transaction> Transactions = new List<Transaction>();
    public static List<AccountHolder> AccountHolders = new List<AccountHolder>();
    public static List<Employee> Employees = new List<Employee>();

    static GlobalDataService()
    {
        AccountHolders.Add(new AccountHolder
        {
            BankId = "BNKvikash",
            Id = "ACHvikash",
            Balance = 10000,
            Type = "Account",
            CreatedOn = DateTime.Now,
            CreatedBy = "Vikash",
            Password = "vikash",
            Name = "Vikash",
        });

        AccountHolders.Add(new AccountHolder
        {
            BankId = "BNKvivek",
            Id = "ACHvivek",
            Balance = 10000,
            Type = "Account",
            CreatedOn = DateTime.Now,
            CreatedBy = "Vivek",
            Password = "vivek1",
            Name = "Vivek"
        });

        Banks.Add(new Bank
        {
            Id = "BNKvikash"
        });

        Banks.Add(new Bank
        {
            Id = "BNKvivek"
        });

        Employees.Add(new Employee
        {
            BankId = "BNKvivek",
            Id = "EMPvivek",
            Type = "Account",
            CreatedOn = DateTime.Now,
            CreatedBy = "Vivek",
            Password = "vivek1"
        });

        Employees.Add(new Employee
        {
            BankId = "BNKvikash",
            Id = "EMPvikash",
            Type = "Account",
            CreatedOn = DateTime.Now,
            CreatedBy = "Vikash",
            Password = "vikash"
        });
    }

}                                                          