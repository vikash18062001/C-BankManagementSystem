public class Bank
{
    public Bank()
    {
        this.Id = String.Empty;

        this.Name = String.Empty;

        this.CreaterName = String.Empty;

        this.Password = String.Empty;
    }

    public string Id { get; set; }

    public string Name { get; set; }

    public string CreaterName { get; set; }

    public string Password { get; set; }

    public double RTGSSame ;

    public double RTGSDiff ;

    public double IMPSSame ;

    public double IMPSDiff ;

}

