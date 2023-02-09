using static System.Console;
using System.Text.RegularExpressions;
using System;

public static class Utility
{
    public enum LoginTypes { Admin, Employee, Accountholder }

    public enum StatusMessage { Balance,Success,Failed,Credential,WrongSelection}

    public static int GetIntInput(string helpText , bool isRequired)
    {
        int input = 0;
        WriteLine(helpText);

        try
        {
            input = Convert.ToInt32(ReadLine().Trim());
            if (isRequired && input == 0)
            {
                WriteLine("Enter valid input");
                return GetIntInput(helpText, isRequired);
            }
        }
        catch(Exception e)
        {
            WriteLine("Enter valid input");
            return GetIntInput(helpText, isRequired);
        }

        return input;
    }

    public static string GetInputString(string helpText, bool isRequired)
    {
        string? input = string.Empty;
        WriteLine(helpText);

        try
        {
            input = ReadLine();
            if((isRequired && string.IsNullOrEmpty(input)) || input.Length<3)
            {
                WriteLine("Enter valid input");
                return GetInputString(helpText, isRequired);
            }
        }
        catch(Exception e)
        {
            WriteLine("Enter valid input");
            return GetInputString(helpText, isRequired);
        }

        return input.Trim();
    }

    public static string GetInputEmail(string helpText,bool isRequired)
    {
        string? input = string.Empty;
        WriteLine(helpText);
        try
        {
            input = ReadLine();
            if((isRequired && !string.IsNullOrEmpty(input)) && IsValidEmail(input))
            {
                return input.Trim();
            }
            return GetInputEmail(helpText, isRequired);
        }
        catch(Exception e)
        {
            WriteLine("Enter the valid email");
            return GetInputEmail(helpText, isRequired);
        }
    }

    public static string GetInputMobileNo(string helpText,bool isRequired)
    {
        string? input = string.Empty;
        WriteLine(helpText);
        try
        {
            input = ReadLine();
            if ((isRequired && !string.IsNullOrEmpty(input)) && IsValidMobileNo(input))
                return input.Trim();
            return GetInputMobileNo(helpText, isRequired);

        }
        catch(Exception e)
        {
            WriteLine("Enter valid mobile no.Mobileno should  be of length 10");
            return GetInputMobileNo(helpText, isRequired);
        }
    }

    public static string GetPassword(string helpText, bool isRequired)
    {
        string? input = string.Empty;
        WriteLine(helpText);
        try
        {
            input = ReadLine();
            if(isRequired && string.IsNullOrEmpty(input))
            {
                WriteLine("Enter valid password");
                return GetPassword(helpText, isRequired);
            }
            else if(input.Length < 6)
            {
                WriteLine("Enter strong password");
                return GetPassword(helpText, isRequired);
            }
        }
        catch(Exception e)
        {
            WriteLine("Enter valid password");
            return GetPassword(helpText, isRequired);
        }
        return input.Trim();
    }

    public static string GenerateBankId(string name)
    {
        try
        {
            return "BNK"+name.Substring(0, 3) + DateTime.Now.ToOADate();
        }
        catch(Exception e)
        {
            return string.Empty;
        }
    }

    public static string GetAccountId(string name)
    {
        string? accountId = "ACH"+name.Substring(0, 3) + DateTime.Now.ToOADate().ToString();
        Console.WriteLine(accountId);
        return accountId.Trim();
    }

    public static Bank GetBankDetails(string? id)
    {
        Bank bank = DataBaseService.GetBankDetails(id);
        if (!string.IsNullOrEmpty(bank.Id))
            return bank;

        WriteLine("Cannot get the bank details.Enter valid id");

        return new Bank();
    }

    public static AccountHolder GetAccountDetail(string? accountId)
    {
        AccountHolder accountHolder = DataBaseService.GetAccountDetail(accountId);

        if (string.IsNullOrEmpty(accountHolder.Id))
        {
            WriteLine("Enter valid id");
            return new AccountHolder();
        }

        return accountHolder;
    }

    public static bool checkIfValidIdsOrNot(string? bankId,string? accountId)
    {
        bool result = DataBaseService.checkIfValidIdsOrNot(bankId, accountId);
        return result;
    }


    public static double GetInputServiceCharge(string helpText )
    {
        double rate;
        WriteLine(helpText);
        try
        {
            rate = Convert.ToDouble(ReadLine());
        }
        catch (Exception e)
        {
            WriteLine("Enter valid rate");
            return GetInputServiceCharge(helpText);
        }
        return rate;
    }

    public static double GetDoubleAmount(string helpText)
    {
        double amount;
        WriteLine(helpText);
        try
        {
            amount = Convert.ToDouble(ReadLine());
        }
        catch (Exception e)
        {
            WriteLine("\nEnter valid amount\n");
            return GetInputServiceCharge(helpText);
        }
        return amount;
    }

    public static void Message(bool isSuccess , string field,string type="account")
    {
        if (isSuccess)
            WriteLine($"Successfully {field} the {type}");
        else
            WriteLine($"{field} Unsuccessful");
    }

    public static bool IsValidEmail(string email)
    {
        try
        {
            if (email.Contains('@') && email.Length > 6)
                return true;
            WriteLine("\nEnter valid email id\n");
            return false;
        }
        catch(Exception e )
        {
            WriteLine("\nEnter valid email id\n");
            return IsValidEmail(email);
        }
    }

    public static bool IsValidMobileNo(string mobileNo)
    {
        try
        {
        string mobileRegex = "^[0-9]{10}";
        Regex re = new Regex(mobileRegex);

            if (mobileNo.Length == 10 && re.IsMatch(mobileNo))
            {
                return true;
            }
            WriteLine("\nEnter valid mobileno\n");
        return false;
        }
        catch(Exception e )
        {
            WriteLine("\nEnter valid mobileno\n");
            return IsValidMobileNo(mobileNo);
        }
    }

    public static string GetType(LoginRequest login)
    {
        if (login.UserId.StartsWith("EMP"))
        {
            return Utility.LoginTypes.Employee.ToString();
        }
        else if (login.UserId.StartsWith("ACH"))
        {
            return Utility.LoginTypes.Accountholder.ToString();
        }
        else if(login.UserId.StartsWith("BNK"))
        {
            return Utility.LoginTypes.Admin.ToString();
        }
        else
        {
            return "";
        }
      
    }

    public static string GetEmployeeId(string name)
    {
        string? empId = "EMP" + name.Substring(0, 3) + DateTime.Now.ToOADate().ToString();
        Console.WriteLine(empId);
        return empId.Trim();
    }

    public static string GetBankId(string empId)
    {
        string bankId = string.Empty;
        try
        {
            bankId = DataBaseService.GetBankId(empId);
            if (string.IsNullOrEmpty(bankId))
                WriteLine("Not able to get the bankId");
            return bankId;

        }
        catch
        {

        }
        return bankId;
       
    }

    public static string GetAccountCreaterName(string empId)
    {
        string employeeName = string.Empty;
        try
        {
            employeeName = DataBaseService.GetAccountCreaterName(empId);
            if (string.IsNullOrEmpty(employeeName))
                WriteLine("Not able to get the employee name. Please enter valid Ids");
            return employeeName;

        }
        catch
        {

        }
        return employeeName;
    }

    public static bool ShowTransactionTable(Transaction transaction, string id, string bankId, string retrivedBankId, string accountId)
    {
        if((accountId == id && bankId == retrivedBankId ))
        {
            if (string.IsNullOrEmpty(transaction.DstAccountId))
            {
                string action = transaction.Type ? "Credit" : "Debit";
                WriteLine("{0}\t{1}\t{2}\t\t{3}\t\t{4}\t\t{5}\t\t{6}", transaction.Id, transaction.SrcAccountId, transaction.DstAccountId, transaction.CreatedBy, transaction.CreatedOn, transaction.Amount, action);
            }
            else if(transaction.Type == false)
            {
                string action = transaction.Type ? "Credit" : "Debit";
                WriteLine("{0}\t{1}\t{2}\t\t{3}\t\t{4}\t\t{5}\t\t{6}", transaction.Id, transaction.SrcAccountId, transaction.DstAccountId, transaction.CreatedBy, transaction.CreatedOn, transaction.Amount, action);
            }
            return true;
        }
        else if(transaction.DstAccountId == id && transaction.Type == true)
        {
            string action = transaction.Type ? "Credit" : "Debit";
            WriteLine("{0}\t{1}\t{2}\t\t{3}\t\t{4}\t\t{5}\t\t{6}", transaction.Id, transaction.SrcAccountId, transaction.DstAccountId, transaction.CreatedBy, transaction.CreatedOn, transaction.Amount, action);
            return true;
        }

        return false;
        
    }

}
