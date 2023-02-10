public class APIResponse
{
    public APIResponse()
    {
        this.Message = string.Empty;
    }

    public bool IsSuccess { get; set; }

    public string Message { get; set; }
}
