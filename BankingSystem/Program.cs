using BankingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public partial class Program
{
    public static void Main()
    { 
        new BankApplication().Initialize();
    }
}