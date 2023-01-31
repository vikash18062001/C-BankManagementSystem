using System;

public class TransactionDetails
{
	public string? transId;
	public string? bankId= default;
	public string? accountId = default;
	public double? amount = default;
	public bool isCredit= default;
	public bool isFundTransfer;
}


