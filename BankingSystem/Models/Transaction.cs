﻿public class Transaction
{
    public Transaction()
    {

        this.Id = string.Empty;

        this.SrcAccountId = string.Empty;

        this.DstAccountId = string.Empty;

        this.CreatedBy = string.Empty;

    }

    public string Id { get; set; } // Transaction Id

	public string SrcAccountId { get; set; }

    public string DstAccountId { get; set; }

    public double Amount { get; set; }

	public bool Type { get; set; } // Is it a credit or debit

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; }
}


