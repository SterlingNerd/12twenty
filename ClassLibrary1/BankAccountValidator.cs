using System;

public class BankAccountValidator
{
	public void ValidateWithdrawRequest(BankAccountRecord account, decimal amount)
	{
		if (amount < 0)
			throw new Exception("Withdrawl amount must be positive.");

		if (account.Balance < amount
			&& account.AccountType != AccountType.Platinum)
		{
			throw new Exception("Only the 1% are allow to overdraft.");
		}
	}
}
