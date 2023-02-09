using static System.Console;

public static class DataBaseService
{
    public static string GetAccountCreaterName(string empId)
    {
        try
        {
            List<Employee> employee = (from emp in GlobalDataService.Employees where emp.Id == empId select emp).ToList<Employee>();
            if(employee.Count != 0)
            {
                return employee.First().Name;
            }
            return string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

        public static string GetBankId(string empId)
    {
        try
        {
            List<Employee> employee = (from emp in GlobalDataService.Employees where emp.Id == empId select emp).ToList<Employee>();
            if (employee.Count != 0)
            {
                return employee.First().BankId;
            }
            return string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public static Bank GetBankDetails(string? id)
    {
        List<Bank> newId = (from bank in GlobalDataService.Banks where bank.Id == id select bank).ToList<Bank>();

        if (newId.Count == 0)
            return new Bank();

        return newId.First();

    }

    public static AccountHolder GetAccountDetail(string? accountId)
    {
        List<AccountHolder> accountHolder = (from account in GlobalDataService.AccountHolders where account.Id == accountId select account).ToList<AccountHolder>();
        if (accountHolder.Count == 0)
            return new AccountHolder();
        return accountHolder.First();
    }

    public static bool checkIfValidIdsOrNot(string? bankId, string? accountId)
    {
        return (from account in GlobalDataService.AccountHolders where account.BankId == bankId && account.Id == accountId select account).Any();
    }

}

