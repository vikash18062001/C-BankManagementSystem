using System.Xml.Linq;
using static System.Console;
using System;
using System.Net.Http.Json;
using BankingSystem.Models;


using System.Text.Json;
using System.Text.Json.Serialization;

public class BankingService
{
    AccountHolderService AccountHolderService = new AccountHolderService();

    //not able to use dependency injection
    private static readonly  BankingDbContext _context = new BankingDbContext();

    public BankingService()
    {
    }

    public APIResponse Deposit(Transaction transaction)
    {
        APIResponse apiResponse = new APIResponse();
        try
        {
            AccountHolder accountHolder = this.AccountHolderService.GetAccountHolder(transaction.SrcAccountId);
            if (accountHolder != null && !string.IsNullOrEmpty(accountHolder.Id))
            {
                accountHolder.Balance += transaction.Amount;
                apiResponse = AccountHolderService.UpdateAccountHolderAfterDeposit(accountHolder);
                //GlobalData.Transactions.Add(transaction);
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }
            else
            {
                apiResponse = Utility.SetApiMessage(false, "Check the ids");
            }
        }
        catch (Exception ex)
        {
            apiResponse = Utility.SetApiMessage(false, "Unable to deposit the money");
        }

        return apiResponse;
    }

    public APIResponse WithDraw(Transaction transaction) // account id
    {
        APIResponse apiResponse = new APIResponse();
        try
        {
            AccountHolder accountHolder = this.AccountHolderService.GetAccountHolder(transaction.SrcAccountId);
            if (accountHolder != null && !string.IsNullOrEmpty(accountHolder.Id))
            {
                if (accountHolder.Balance < transaction.Amount)
                {
                    return Utility.SetApiMessage(false, $"Not enough balance .Cur balance {accountHolder.Balance}");
                }
                accountHolder.Balance -= transaction.Amount;
                apiResponse = this.AccountHolderService.UpdateAccountHolderAfterWithdraw(accountHolder);
                //GlobalData.Transactions.Add(transaction);
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }
            else
            {
                apiResponse = Utility.SetApiMessage(false, "Check the Ids");
            }
        }
        catch (Exception ex)
        {
            apiResponse = Utility.SetApiMessage(false, "Unable to withdraw the money");
        }

        return apiResponse;
    }


    public Bank CreateBank(Bank bank)
    {
        try
        {
            bank.Id = this.GenerateBankId(bank.Name);

            if (!string.IsNullOrEmpty(bank.Id) && !string.IsNullOrEmpty(bank.Name) && !string.IsNullOrEmpty(bank.CreatedBy))
            {
                _context.Banks.Add(bank);
                _context.SaveChanges();
            }
        }
        catch (Exception e)
        {
            Console.Write(e);
            return bank;
        }

        return bank;
    }

    public APIResponse AddBankEmployee(Employee employee)
    {
        APIResponse apiResponse = new APIResponse();

        try
        {
            //var response = apiService.Post("employeeCreation", employee);

            _context.Employees.Add(employee);
            _context.SaveChanges();
            apiResponse = Utility.SetApiMessage(true, $"Successfully added the employee.The employee Id is {employee.Id}");
        }
        catch (Exception e)
        {
            apiResponse = Utility.SetApiMessage(false, "Unsuccessful opeartion.Some error occured");
        }

        return apiResponse;
    }

    public APIResponse CreateAccount(AccountHolder accountHolder)
    {
        APIResponse apiResponse = new APIResponse();
        try
        {
            accountHolder.Id = AccountService.GenerateAccountId(accountHolder.Name);
            //GlobalData.AccountHolders.Add(AccountHolder);
            _context.AccountHolders.Add(accountHolder);
            _context.SaveChanges();

            apiResponse = Utility.SetApiMessage(true, "Successfully created the account");
        }
        catch (Exception e)
        {
            apiResponse = Utility.SetApiMessage(false, "Some error occured during creation please try again");
            //Log exception
        }

        return apiResponse;
    }

    public APIResponse UpdateAccount(string id, Dictionary<string, object> updatedDetails)
    {
        APIResponse apiResponse = new APIResponse();
        try
        {
            List<AccountHolder> accountHolders = (from account in _context.AccountHolders where account.Id == id select account).ToList<AccountHolder>();

            if (accountHolders.Count() == 0)
                apiResponse = Utility.SetApiMessage(false, "Please check the id ");
            else
            {
                accountHolders.First().Email = updatedDetails["Email"].ToString()!;
                accountHolders.First().Mobile = updatedDetails["Mobile"].ToString()!;
                _context.SaveChanges();
                apiResponse = Utility.SetApiMessage(true, "Updation is successful");
            }
        }
        catch
        {
            apiResponse = Utility.SetApiMessage(false, "Some error occured please try again");
        }

        return apiResponse;

    }

    public APIResponse DeleteAccount(string id)
    {
        APIResponse apiResponse = new APIResponse();
        try
        {
            List<AccountHolder> accountHolder = _context.AccountHolders.Where(account => account.Id == id).ToList<AccountHolder>();
            if (accountHolder.Count > 0)
            {
                _context.AccountHolders.Remove(accountHolder.First());
                _context.SaveChanges();
                apiResponse = Utility.SetApiMessage(true, "Successful deletion");
            }
            //GlobalData.AccountHolders.RemoveAll(r =>
            //{
            //    if (r.Id == id)
            //    {
            //        apiResponse = Utility.SetApiMessage(true, "Successful deletion");
            //        return true;
            //    }
            //    return false;
            //});
            //if (apiResponse.IsSuccess)
            //    apiResponse = DeleteTransactinon(id);
            else
                apiResponse = Utility.SetApiMessage(false, "Unsuccessful Deletion");
        }
        catch (Exception ex)
        {
            apiResponse = Utility.SetApiMessage(false, "Some error please try again");
        }

        return apiResponse;
    }

    public List<Transaction> GetTransactionHistory(string id, string bankId)
    {
        int startIndex, endIndex;
        List<Transaction> userTranasactions = new List<Transaction>();
        try
        {
            foreach (Transaction transaction in _context.Transactions)
            {
                endIndex = transaction.Id.IndexOf('/', startIndex = transaction.Id.IndexOf('/') + 1);
                string retrivedBankId = transaction.Id.Substring(startIndex, endIndex - startIndex);

                endIndex = transaction.Id.IndexOf('/', startIndex = transaction.Id.IndexOf('/', transaction.Id.IndexOf('/') + 1) + 1);
                string accountId = transaction.Id.Substring(startIndex, endIndex - startIndex);

                if (this.IsAnyTransaction(transaction, id, bankId, retrivedBankId, accountId))
                    userTranasactions.Add(transaction);
            }
        }
        catch (Exception ex)
        {
            new List<Transaction>();
        }

        return userTranasactions;
    }

    public APIResponse RevertTransaction(string transId, string accountId)
    {
        APIResponse apiResponse = new APIResponse();
        try
        {
            List<Transaction> transactions = (from trans in _context.Transactions where trans.Id == transId select trans).ToList<Transaction>();
            if (transactions.Count() == 0)
            {
                return Utility.SetApiMessage(false, "Revert is unsucessful please check the id");
            }
            else if (transactions.Count == 1)
            {
                if (transactions.First().SrcAccountId != accountId)
                    return Utility.SetApiMessage(false, "No transaction found check the id");
            }
            apiResponse = RevertTheAmount(transactions.First(), transId);
            if (!apiResponse.IsSuccess && !string.IsNullOrEmpty(transactions.First().DstAccountId))
                apiResponse = RevertTheAmount(transactions.Last(), transId);
        }
        catch
        {
            apiResponse = Utility.SetApiMessage(false, "Some error occured please try again");
        }

        return apiResponse;

    }

    public APIResponse DeleteTransactinon(string? transId)
    {
        APIResponse apiResponse = new APIResponse();
        try
        {
            List<Transaction> transactions = (from transaction in _context.Transactions where transaction.Id == transId select transaction).ToList<Transaction>();
            foreach(var trans in transactions)
            {
                _context.Transactions.Remove(trans);
                _context.SaveChanges();
            }
            apiResponse = Utility.SetApiMessage(true, "Successful Deletion");
        }
        catch
        {
            Utility.SetApiMessage(false, "Deletion is Unsuccessful");
        }

        return apiResponse;
    }

    public APIResponse RevertTheAmount(Transaction transaction, string transId)
    {
        AccountHolder senderAccount = this.AccountHolderService.GetAccountHolder(transaction.SrcAccountId);
        AccountHolder receiverAccount = this.AccountHolderService.GetAccountHolder(transaction.DstAccountId);

        if (!string.IsNullOrEmpty(transaction.DstAccountId) && transaction.Type && receiverAccount != null)
        {
            receiverAccount.Balance -= transaction.Amount;
            senderAccount.Balance += transaction.Amount;
            APIResponse apiResponse = this.DeleteTransactinon(transId);
            if (apiResponse.IsSuccess)
                return Utility.SetApiMessage(true, "Revert is succesfull");
            return Utility.SetApiMessage(false, "Revert is Unsuccessfull");
        }
        else if (string.IsNullOrEmpty(transaction.DstAccountId))
        {
            if (transaction.Type)
                senderAccount.Balance -= transaction.Amount;
            else
                senderAccount.Balance += transaction.Amount;

            APIResponse apiResponse = this.DeleteTransactinon(transId);
            return Utility.SetApiMessage(true, "Revert is successfull");
        }
        return Utility.SetApiMessage(false, "Revert is unsuccessful");
    }

    public APIResponse checkIfValidIdsOrNot(string? bankId, string? accountId)
    {
        APIResponse apiResponse = new APIResponse();
        try
        {
            bool isValid = (from account in _context.AccountHolders where account.BankId == bankId && account.Id == accountId select account).Any();
            if (isValid)
                apiResponse = Utility.SetApiMessage(true, "Found the account ");
            else
                apiResponse = Utility.SetApiMessage(false, "No account found");

            return apiResponse;
        }
        catch
        {
            apiResponse = Utility.SetApiMessage(false, "Some error occured please try again");
        }
        return apiResponse;
    }

    public string GenerateBankId(string name)
    {
        try
        {
            return "BNK" + name.Substring(0, 3) + DateTime.Now.ToOADate();
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }

    public string GetEmployeeId(string name)
    {
        string empId = "EMP" + name.Substring(0, 3) + DateTime.Now.ToOADate().ToString();
        return empId;
    }

    public bool IsAnyTransaction(Transaction transaction, string id, string bankId, string transactionBankId, string transactionAccountId)
    {
        if ((transactionAccountId == id && bankId == transactionBankId))
        {
            if (string.IsNullOrEmpty(transaction.DstAccountId))
            {
                return true;
            }
            else if (transaction.Type == false)
            {
                return true;
            }
            return false;
        }
        else if (transaction.DstAccountId == id && transaction.Type == true)
        {
            return true;
        }

        return false;
    }


    public string GetBankId(string empId)
    {
        try
        {
            List<Employee> employee = (from emp in _context.Employees where emp.Id == empId select emp).ToList<Employee>();
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

    public static Bank GetBankDetail(string? id) // changed from static to normal
    {
        try
        {
            List<Bank> newId = (from bank in _context.Banks where bank.Id == id select bank).ToList<Bank>();
            if (newId.Count == 0)
                return new Bank();

            return newId.First();
        }
        catch (Exception ex)
        {
            return new Bank();
        }
    }


}