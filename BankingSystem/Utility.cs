using static System.Console;
public static class Utility
{
    static string? InputString;

    static Utility()
    {
        InputString = String.Empty;
    }

    public static string GetInputString()
    {
        InputString = ReadLine();
        return InputString!;
    }

    public static BankDetailModel GetBankDetails(string? id)
    {
        foreach(BankDetailModel model in GlobalDataService.BankDetails)
        {
            if (model.BankId == id)
                return model;
        }
        return null!;
    }

    public static BankDetailsOfEmployee GetBankDetailsOfEmployee(string? accountId, string? bankId)
    {
        foreach(BankDetailsOfEmployee detail in GlobalDataService.currentBankEmployee)
        {
            if (detail?.AccountId != null && detail?.AccountId == accountId && detail?.BankId == bankId)
                return detail!;
        }
        return null!;
    }


}


