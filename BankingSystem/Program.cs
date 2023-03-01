using BankingSystem.Models;
using Microsoft.Data.SqlClient;

public partial class Program
{
    static void Main()
    {
        //Connection();
        new BankApplication().Initialize();
    }

    //public static void Connection()
    //{
    //    using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
    //    {

    //        SqlCommand cmd = new SqlCommand("Select * from banks", conn);
    //        conn.Open();

    //        SqlDataReader reader = cmd.ExecuteReader();
    //        while (reader.Read())
    //        {
    //            Console.WriteLine(reader[0]);
    //        }
    //    }
    //}

}



