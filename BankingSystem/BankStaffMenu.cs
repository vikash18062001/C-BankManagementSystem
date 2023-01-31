using static System.Console;
using System;
public class BankingStaffMenu : BankCreation
{
    public static BankDetailsOfEmployee[] currentBankEmployee = new BankDetailsOfEmployee[10];
    static int _curEmp = 0;
    BankingService _bankingService = new BankingService();
    string? _bankId;
    string? _curId;

    public void HomePage(ref BankCreation bankCreationObj)
    {
        WriteLine("Enter the bankId");
        string? id = ReadLine();
        WriteLine("Enter the userName");
        string? userName = ReadLine();
        WriteLine("Enter the password");
        string? password = ReadLine();
        bool isValid = Validate(id!, userName!, password!, ref bankCreationObj);
        if (isValid)
        {

            while (true)
            {
                WriteLine("\n\n****Banking Management System****\n\n");
                WriteLine("Choose What You Want to do\n");
                WriteLine("1 : Creation of Account");
                WriteLine("2 : Update an Account");
                WriteLine("3 : Delete An Account");
                WriteLine("4 : Add Service Charge For Same Bank");
                WriteLine("5 : Add Service Charge For Different Bank");
                WriteLine("6 : View Transaction History");
                WriteLine("7 : Revert a Transaction");
                WriteLine("8 : Exit");
                object? input = Console.ReadLine();
                string? inputString = input?.ToString();
                switch (inputString)
                {
                    case "1":
                        currentBankEmployee[_curEmp++] = _bankingService.create_account(_bankId!);
                        break;
                    case "2":
                        WriteLine("Enter Your AccountId");
                        _curId = ReadLine();
                        _bankingService.UpdateAccount(ref currentBankEmployee, _curId!);
                        break;
                    case "3":
                        WriteLine("Enter the AccountId You Want to delete");
                        _curId = ReadLine();
                        _bankingService.DeleteAccount(ref currentBankEmployee, _curId!);
                        _curEmp--;
                        break;
                    case "4":
                        _bankingService.ShowAll(ref currentBankEmployee, _curEmp);
                        WriteLine("Enter the BankId You want to change Service");
                        _curId = ReadLine();
                        _bankingService.ChangeService(_curId, bankCreationObj, true);
                        break;
                    case "5":
                        WriteLine("Enter the BankId You want to change Service");
                        _curId = ReadLine();
                        _bankingService.ChangeService(_curId, bankCreationObj, false);
                        break;
                    case "6":
                        WriteLine("Enter the AccountId for which you want to get transaction history");
                        _curId = ReadLine();
                        _bankingService.ShowTransactionHistory(ref currentBankEmployee, _curId!, this._bankId!);
                        break;
                    case "7":
                        WriteLine("Enter the AccountId for which you want to revert the transaction");
                        _curId = ReadLine();
                        _bankingService.RevertTransaction(ref currentBankEmployee, _curId!, this._bankId!);
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

    public bool Validate(string bankId, string userName, string password, ref BankCreation bankCreationObj)
    {
        for (int i = 0; i < 1; i++)
        {
            if ((bankCreationObj._bankIds[i] != null) && (bankCreationObj._bankIds[i] == bankId) && (bankCreationObj._bankCreaterName[i] == userName) && (bankCreationObj._passwords[i] == password))
            {
                this._bankId = bankId;
                return true;
            }
        }
        return false;
    }
}
