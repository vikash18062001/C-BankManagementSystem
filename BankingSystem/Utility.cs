using static System.Console;
using System.Text.RegularExpressions;
public static class Utility
{
    public static int GetIntInput(string helpText , bool isRequired)
    {
        int input = 0;
        WriteLine(helpText);

        try
        {
            input = Convert.ToInt32(ReadLine());
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
        return input;
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
                return input;
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
                return input;
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
        return input;
    }

    public static string GenerateBankId(string name)
    {
        try
        {
            return name.Substring(0, 3) + DateTime.Now.ToOADate();
        }
        catch(Exception e)
        {
            return "";
        }
    }
    // change the get account Id
    public static string GetAccountId(string name)
    {
        string? accountId = name.Substring(0, 3) + DateTime.Now.ToOADate().ToString();
        Console.WriteLine(accountId);
        return accountId;
    }

    public static Bank GetBankDetails(string? id)
    {
        foreach(Bank model in GlobalDataService.Bank)
        {
            if (model.Id == id)
                return model;
        }
        WriteLine("Enter valid id");
        return null!;
    }

    public static AccountHolder GetDetails(string? accountId, string? bankId)
    {
        foreach(AccountHolder detail in GlobalDataService.AccountHolder)
        {
            if (detail?.Id != null && detail?.Id == accountId && detail?.BankId == bankId)
                return detail!;
        }
        return null!;
    }

    public static bool isNull(string? input)
    {
        return String.IsNullOrEmpty(input);
    }

    public static bool checkIfValidIdsOrNot(string? bankId,string? accountId)
    {
        foreach(AccountHolder accountHolder in GlobalDataService.AccountHolder)
        {
            if(accountHolder.BankId == bankId && accountHolder.Id == accountId)
            {
                return true;
            }
        }
        return false;
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
}


//viv44963.49371486111
//viv44963.49478612268


//vik44963.49390548611
//vik44963.4944037963
