public class Bank
{
    public Bank()
    {
        this.Id = string.Empty;

        this.Name = string.Empty;

        this.CreatedBy = string.Empty;
    }

    public string Id { get; set; }

    public string Name { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public double RTGSSame ;

    public double RTGSDiff ;

    public double IMPSSame ;

    public double IMPSDiff ;
}

