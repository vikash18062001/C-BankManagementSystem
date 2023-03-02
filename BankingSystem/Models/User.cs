public class User
{
	public User()
	{
		this.Id = string.Empty;

		this.Name = string.Empty;

		this.Password = string.Empty;

		this.Mobile = string.Empty;

        this.Email = string.Empty;

        this.Type = string.Empty;

		this.CreatedBy = string.Empty;
	}

    public string Id { get; set; }

    public string Name { get; set; }

	public string Type { get; set; }

	public string Email { get; set; }

	public string Mobile { get; set; }

	public string Password { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }
}
