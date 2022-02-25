using System;

public class BankAccountValidator
{
	public void ValidateWithdrawRequest(BankAccountRecord account, WithdrawRequest request)
	{
		if (request.Amount < 0)
		{
			throw new Exception("Withdrawl amount must be positive.");
		}

		if (account.Balance < request.Amount
			&& account.AccountType != AccountType.Platinum)
		{
			throw new Exception("Only the 1% are allow to overdraft.");
		}
	}
}
