using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BankModel
{
    [Key]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    public double RTGSSame { get; set; }

    public double RTGSDiff { get; set; }

    public double IMPSSame { get; set; }

    public double IMPSDiff { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

}

