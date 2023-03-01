using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public string Id { get; set;}

    [Required]
    public string Name { get; set; }

    [Required]
	public string Password { get; set; }

	public string Type { get; set; }

	public string Email { get; set; }

    [StringLength(10,ErrorMessage ="Please enter only 10 digits")]
	public string Mobile { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

}
