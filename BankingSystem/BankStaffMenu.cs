using static System.Console;
using System;
public class BankingStaffMenu : BankCreation
{
    public static BankDetailsOfEmployee[] currentBankEmployee = new BankDetailsOfEmployee[10];

    static int curEmp = 0;
    string? bankId;

    BankingService bankingService = new BankingService();
    string? curId;
    public void HomePage(ref BankCreation ob)
    {
        WriteLine("Enter the bankId");
        string? id = ReadLine();
        WriteLine("Enter the username");
        string? username = ReadLine();
        WriteLine("Enter the password");
        string? password = ReadLine();
        bool isValid = validate(id!, username!, password!, ref ob);
        if (isValid)
        {

            while (true)
            {
                WriteLine("\n\n****Banking Management System****\n\n");
                WriteLine("Choose What You Want to do\n");
                WriteLine("1 : Creation of Account");
                WriteLine("2 : Update an Account");
                WriteLine("3 : Delete An Account");
                WriteLine("4 : Add New Accepted Currency");
                WriteLine("5 : Add Service Charge");
                WriteLine("6 : View Transaction History");
                WriteLine("7 : Revert a Transaction");
                WriteLine("8 : Exit");
                object? input = Console.ReadLine();
                string? inputString = input?.ToString();
                switch (inputString)
                {
                    case "1":
                        currentBankEmployee[curEmp++] = bankingService.create_account(bankId!);
                        break;
                    case "2":
                        WriteLine("Enter Your AccountId");
                        curId = ReadLine();
                        bankingService.update_account(ref currentBankEmployee, curId!);
                        break;
                    case "3":
                        WriteLine("Enter the AccountId You Want to delete");
                        curId = ReadLine();
                        bankingService.delete_account(ref currentBankEmployee, curId!);
                        curEmp--;
                        break;
                    case "4":
                        bankingService.showAll(ref currentBankEmployee, curEmp);
                        WriteLine("Enter the BankId You want to change Service");
                        curId = ReadLine();
                        changeService(curId, ob, true);
                        break;
                    case "5":
                        WriteLine("Enter the BankId You want to change Service");
                        curId = ReadLine();
                        changeService(curId, ob, false);
                        break;
                    case "6":
                        WriteLine("Enter the AccountId for which you want to get transaction history");
                        curId = ReadLine();
                        bankingService.showTransactionHistory(ref currentBankEmployee, curId!, this.bankId!);
                        break;
                    case "7":
                        WriteLine("Enter the AccountId for which you want to revert the transaction");
                        curId = ReadLine();
                        bankingService.revertTransaction(ref currentBankEmployee, curId!, this.bankId!);
                        break;
                    case "8":
                        return;
                }
                ReadKey();
            }
        }
        else
        {
            WriteLine("Enter Correct Details");

        }
        return;

    }

    public bool validate(string bankId, string username, string password, ref BankCreation ob)
    {


        for (int i = 0; i < 1; i++)
        {

            if ((ob.bankIds[i] != null) && (ob.bankIds[i] == bankId) && (ob.bankCreaterName[i] == username) && (ob.passwords[i] == password))
            {
                this.bankId = bankId;
                return true;
            }
        }
        return false;
    }

    public void changeService(string? bankId, BankCreation ob, bool isSame)
    {
        int index = Array.IndexOf(ob.bankIds, bankId);

        if (isSame)
        {
            WriteLine("Enter serive charge for RTGS in percent ");
            ob.RTGSSame[index] = Convert.ToDouble(ReadLine());
            WriteLine("Enter serive charge for RTGS in percent ");
            ob.IMPSSame[index] = Convert.ToDouble(ReadLine());
        }
        else
        {

            WriteLine("Enter serive charge for RTGS in percent ");
            ob.RTGSDiff[index] = Convert.ToDouble(ReadLine());
            WriteLine("Enter serive charge for RTGS in percent ");
            ob.IMPSDiff[index] = Convert.ToDouble(ReadLine());

        }

        //BankDetailsOfEmployee getEmployee(string id)
        //{
        //    foreach(BankDetailsOfEmployee ob in currentBankEmployee)
        //    {
        //        if (ob.accountId == id)
        //            return ob;
        //    }
        //    WriteLine("Account Not Found");
        //    return null!;

    }
}
