using System;
public class BankDetailModel
{

    public string? BankId ;
    public string? BankName ;
    public string? BankCreaterName ;
    public string? Password ;
    public double RTGSSame ;
    public double RTGSDiff ;
    public double IMPSSame ;
    public double IMPSDiff ;
    //change rtgs and imps value 
    public BankDetailModel()
    {
        this.BankId = String.Empty;
        this.BankName = String.Empty;
        this.BankCreaterName = String.Empty;
        this.Password = String.Empty;
        this.RTGSSame = 0;
        this.RTGSDiff = 0;
        this.IMPSSame = 0;
        this.IMPSDiff = 0;
    }
}

