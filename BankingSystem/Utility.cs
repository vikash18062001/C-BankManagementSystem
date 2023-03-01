using static System.Console;
using System.Text.RegularExpressions;
using System.Transactions;

public static class Utility
{
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
            input = ReadLine().Trim();
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
            input = ReadLine().Trim();
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
            input = ReadLine().Trim();
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
            input = ReadLine().Trim();
            if(isRequired && string.IsNullOrEmpty(input))
            {
                WriteLine("Enter valid password");
                return GetPassword(helpText, isRequired);
            }
            else if(input.Length < 6)
            {
                WriteLine("Enter valid password.It must be atleast of 6 character");
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

    public static double GetInputServiceCharge(string helpText )
    {
        double rate;
        WriteLine(helpText);
        try
        {
            rate = Convert.ToDouble(ReadLine().Trim());
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
            amount = Convert.ToDouble(ReadLine().Trim());
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
            return Enums.LoginTypes.Employee.ToString();
        }
        else if (login.UserId.StartsWith("ACH"))
        {
            return Enums.LoginTypes.Accountholder.ToString();
        }
        else if(login.UserId.StartsWith("BNK"))
        {
            return Enums.LoginTypes.Admin.ToString();
        }
        else
        {
            return string.Empty;
        }
      
    }

    public static APIResponse SetApiMessage(bool isSuccess, string message)
    {
        APIResponse response = new APIResponse()
        {
            IsSuccess = isSuccess,
            Message = message
        };

        return response;
    }

}