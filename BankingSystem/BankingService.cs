using System;
using static System.Console;
public class BankingService
{
    public BankDetailsOfEmployee create_account(string bankId)
    {
        BankDetailsOfEmployee bankDetailsOfEmployee = new BankDetailsOfEmployee();
        WriteLine("Enter the Name");
        string? name = ReadLine();
        WriteLine("Enter the dob");
        string? dob = ReadLine();
        WriteLine("Enter InitialBalance");
        int? initialBalance = Convert.ToInt32(ReadLine());
        WriteLine("Enter the password You want for this account");
        string? password = ReadLine();

        bankDetailsOfEmployee._name = name;
        bankDetailsOfEmployee._dob = dob;
        bankDetailsOfEmployee._initialBalance = initialBalance;
        bankDetailsOfEmployee._curBalance = initialBalance;
        bankDetailsOfEmployee._bankId = bankId;
        bankDetailsOfEmployee._password = password;
        bankDetailsOfEmployee.getAccountId(name!);
        return bankDetailsOfEmployee;
    }

    public void UpdateAccount(ref BankDetailsOfEmployee[] ob, string id)
    {
        foreach (BankDetailsOfEmployee obj in ob)
        {
            if (obj?._accountId == id)
            {
                if (obj?._accountId == null)
                    break;
                Console.WriteLine("Enter the new Name");
                obj._name = ReadLine();
                WriteLine("Enter the new dob");
                obj._dob = ReadLine();
            }
        }

    }

    public void DeleteAccount(ref BankDetailsOfEmployee[] ob, string id)
    {
        ob = Array.FindAll(ob, i => i?._accountId != id);
    }

    public void ShowTransactionHistory(ref BankDetailsOfEmployee[] ob, string id, string bankId)
    {
        foreach (BankDetailsOfEmployee obj in ob)
        {
            if (obj?._accountId == id && obj?._bankId == bankId)
            {
                if (obj?._accountId == null)
                    break;
                WriteLine("TransactionId\t\tBankId\t\tAccountId\t\tAmount\t\t Action\t\t");
                foreach (TransactionDetails transaction in obj._transaction)
                {
                    if (transaction?._transId == null)
                        continue;
                    if (transaction?._accountId == null)
                        break;
                    string action = transaction._isCredit ? "Credit" : "Debit";
                    WriteLine("{0}\t{1}\t\t{2}\t\t{3}\t\t{4}", transaction._transId, transaction._bankId, transaction._accountId, transaction._amount, action);
                }

            }
        }
    }

    public void RevertTransaction(ref BankDetailsOfEmployee[] ob, string id, string bankId)
    { 
        WriteLine("Enter the TransId You want to Revert");
        string? trans_id = ReadLine();

        foreach (BankDetailsOfEmployee obj in ob)
        {
            if (obj?._accountId == id && obj?._bankId == bankId)
            {
                if (obj?._accountId == null)
                    break;
                foreach (TransactionDetails transaction in obj._transaction)
                {
                    if (transaction?._accountId == null)
                        break;
                    if (transaction?._transId == trans_id)
                    {
                        transaction!._transId = null;
                        if (transaction._isCredit)
                        {
                            obj._curBalance -= transaction._amount;
                        }
                        else
                            obj._curBalance += transaction._amount;
                    }
                }

            }
        }
    }
    public void ShowAll(ref BankDetailsOfEmployee[] ob, int cursize)
    {
        if (ob[0]?._accountId != null)
        {
            foreach (BankDetailsOfEmployee obj in ob)
            {
                if (obj?._accountId == null)
                    break;
                WriteLine(obj._accountId);
                WriteLine(obj._name);
                WriteLine(obj._dob);
                WriteLine(obj._curBalance);

            }
        }
        return;
    }

    public void ChangeService(string? bankId, BankCreation bankCreationObj, bool isSame)
    {
        int index = Array.IndexOf(bankCreationObj._bankIds, bankId);

        if (isSame)
        {
            WriteLine("Enter serive charge for RTGS in percent ");
            bankCreationObj._RTGSSame[index] = Convert.ToDouble(ReadLine());
            WriteLine("Enter serive charge for IMPS in percent ");
            bankCreationObj._IMPSSame[index] = Convert.ToDouble(ReadLine());
        }
        else
        {

            WriteLine("Enter serive charge for RTGS in percent ");
            bankCreationObj._RTGSDiff[index] = Convert.ToDouble(ReadLine());
            WriteLine("Enter serive charge for IMPS in percent ");
            bankCreationObj._IMPSDiff[index] = Convert.ToDouble(ReadLine());

        }
    }

    //public void exchangeCurRate(ref BankCreation bankCreation , string bankId)
    //{
    //	WriteLine("Enter the currency In Short form i.e INR");
    //	string? cur = ReadLine();
    //	WriteLine("Enter the ExchangeRate to INR");
    //	double? rate = Convert.ToDouble(ReadLine());
    //	Dictionary<string, double> dict = new Dictionary<string, double>();
    //	dict[cur] = rate;
    //	bankCreation.exchangeRate[bankId].
    //}
}
