public class User
{
	public User()
	{
		this.Id = string.Empty;

		this.Name = string.Empty;

		this.Password = string.Empty;

		this.Mobile = string.Empty;

		this.Type = string.Empty;

		this.Email = string.Empty;
	}

	public string CreatedBy { get; set; }

	public DateTime CreatedOn;

	public string Name { get; set; }

	public string Type { get; set; }

	public string Id { get; set; }

	public string Email { get; set; }

	public string Mobile { get; set; }

	public string Password { get; set; }

}
