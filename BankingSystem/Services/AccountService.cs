public static class AccountService
{
    public static bool IsAuthenticated(LoginRequest login, string type)
    {
        if (type == Utility.LoginTypes.Employee.ToString())
        {
            var data = GlobalDataService.Employees.Where(obj => (obj.Id == login.UserId) && (obj.Password == login.Password)).Any();
            if (data)
                return data;

            return false;

        }
        else if (type == Utility.LoginTypes.Accountholder.ToString())
        {
            var data = GlobalDataService.AccountHolders.Where(obj => (obj.Id == login.UserId) && (obj.Password == login.Password)).Any();
            if (data)
                return data;
            return false;
        }

        return false;
    }
}
