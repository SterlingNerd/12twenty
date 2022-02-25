using System;

public class WithdrawRequest
{
	public int AccountId { get; }
	public decimal Amount { get; }

	public WithdrawRequest(int accountId, decimal amount)
	{
		AccountId = accountId;
		Amount = amount;
	}
}
