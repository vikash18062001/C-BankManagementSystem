using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Transaction
{
    [Key]
    public string Id { get; set; } // Transaction Id

    [Required]
	public string SrcAccountId { get; set; }

    public string DstAccountId { get; set; }

    [Required]
    public double Amount { get; set; }

	public bool Type { get; set; } // Is it a credit or debit

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; }
}


