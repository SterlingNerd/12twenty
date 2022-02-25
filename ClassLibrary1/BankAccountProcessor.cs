using System;

using Microsoft.Extensions.Logging;

public class BankAccountProcessor
{
	private readonly BankDatabaseContext _dbContext;
	private readonly BankAccountValidator _validator;
	private readonly ILogger<BankAccountProcessor> _logger;

	public BankAccountProcessor(BankDatabaseContext dbContext, BankAccountValidator validator, ILogger<BankAccountProcessor> logger)
	{
		// Pretend these have interfaces. excluded for brevity.
		_dbContext = dbContext;
		_validator = validator;
		_logger = logger;
	}

	public void Withdraw(int accountId, decimal amount)
	{
		BankAccountRecord account = _dbContext.Accounts.FirstOrDefault(x => x.Id == accountId) ?? throw new Exception("Account not found.");
		_validator.ValidateWithdrawRequest(account, amount);

		account.Withdraw(amount);
		_dbContext.SaveChanges();
		_logger.LogInformation("Withdrew {0} on {1}", amount, DateTime.Now);
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
			_logger.LogInformation("Accumulated {0} interest on {1}", interest, DateTime.Now);
		}

		_dbContext.SaveChanges();
	}
}
