using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

//[Keyless]
public class Employee : User
{
    public double Salary { get; set; }

    public string BankId { get; set; }

}
