public class LoginRequest
{
    public LoginRequest()
    {
        this.Type = string.Empty;

        this.Password = string.Empty;

        this.UserId = string.Empty;
    }

    public string Password { get; set; }

    public string UserId { get; set; }

    public string Type { get; set; }
}

