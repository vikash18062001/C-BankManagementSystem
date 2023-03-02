﻿using BankingSystem.Models;

public static class AccountService
{
    private static readonly BankingDbContext _context = new BankingDbContext();

    public static bool IsAuthenticated(LoginRequest login, string type)
    {
        try
        {
            if (type == Enums.LoginTypes.Employee.ToString())
            {
                var data = _context.Employees.Where(obj => (obj.Id == login.UserId) && (obj.Password == login.Password)).Any();
                if (data)
                    return data;

                return false;
            }
            else if (type == Enums.LoginTypes.Accountholder.ToString())
            {
                var data = _context.AccountHolders.Where(obj => (obj.Id == login.UserId) && (obj.Password == login.Password)).Any();
                if (data)
                    return data;

                return false;
            }
        }
        catch(Exception e)
        {

        }

        return false;
    }

    public static string GetAccountCreaterName(string empId)
    {
        try
        {
            List<Employee> employee = (from emp in _context.Employees where emp.Id == empId select emp).ToList<Employee>();
            if (employee.Count != 0)
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

    public static string GenerateAccountId(string name)
    {
        string? accountId = "ACH" + name.Substring(0, 3) + DateTime.Now.ToOADate().ToString();
        Console.WriteLine(accountId);
        return accountId.Trim();
    }

}
