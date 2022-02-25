using System;

/*
 * 1) Fix typos
 * 2) Separate data from logic
 * 3) Rename to "Record" suffix. to infer the data comes from some persistant storage. Makes it easier to tell apart from requests, responses, DTOs etc.
 */
public class BankAccountRecord
{
	public AccountType AccountType { get; protected set; }
	public decimal Balance { get; protected set; }
	public int Id { get; protected set; }

	protected BankAccountRecord()
	{
		// Protected so entityframework can create these things, but we cannot.
	}

	public void Withdraw(RequestContext requestContext, WithdrawRequest request)
	{
		Balance -= request.Amount;

		// update audit logs etc.
	}

	public void Deposit(decimal amount)
	{
		Balance += amount;

		// update audit logs etc.
	}
}
