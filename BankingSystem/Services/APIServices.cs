using System;
using System.Net.Http.Json;
using BankingSystem.Models;

namespace BankingSystem.Services
{
	public class APIServices<T>
	{
		public bool Post(string uri,T obj) // make return type as api response
		{
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BaseUrl);
                var response = client.PostAsJsonAsync(uri, obj).Result; 
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }
	}
}
