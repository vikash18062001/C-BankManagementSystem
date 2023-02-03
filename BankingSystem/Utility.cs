using static System.Console;
public static class Utility
{
    static string? InputString;

    static Utility()
    {
        InputString = String.Empty;
    }

    public static string? GetInputString()
    {
        InputString = ReadLine();
        if (String.IsNullOrEmpty(InputString))
            return null;
        return InputString;
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


}


