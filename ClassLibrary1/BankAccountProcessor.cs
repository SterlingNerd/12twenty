using System;

public class BankAccountProcessor
{
	private readonly BankDatabaseContext _dbContext;
	private readonly BankAccountValidator _validator;

	public BankAccountProcessor(BankDatabaseContext dbContext, BankAccountValidator validator)
	{
		// Pretend these have interfaces. excluded for brevity.
		_dbContext = dbContext;
		_validator = validator;
	}

	public void Withdraw(int accountId, decimal amount)
	{
		BankAccountRecord account = _dbContext.Accounts.FirstOrDefault(x => x.Id == accountId) ?? throw new Exception("Account not found.");
		_validator.ValidateWithdrawRequest(account, amount);

		account.Withdraw(amount);
		_dbContext.SaveChanges();
		Log("Withdrew {0} on {1}", amount, DateTime.Now);
	}

	public void AccumulateInterest(decimal baseRate)
	{
		foreach (var account in _dbContext.Accounts)
		{
			decimal interest;

			if (account.Balance < 10000)
			{
				interest = account.Balance * baseRate;
			}
			else
			{
				interest = account.Balance * (baseRate + 0.01m);
			}

			account.Deposit(interest);
			Log("Accumulated {0} interest on {1}", interest, DateTime.Now);
		}

		_dbContext.SaveChanges();
	}

	void Log(string message, params object[] parameters)
	{
		FileStream fs = File.Open("auditlog.txt", FileMode.OpenOrCreate);

		StreamWriter writer = new StreamWriter(fs);
		writer.WriteLine(message, parameters);
	}
}
