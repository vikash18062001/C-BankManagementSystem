using System;
using BankingSystem.Models;
using Microsoft.Data.SqlClient;

namespace BankingSystem.Services
{
	public static class DatabaseServices
	{
        public static void InsertingData(string query)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.ExecuteNonQuery();
            }
        }
    }
}

