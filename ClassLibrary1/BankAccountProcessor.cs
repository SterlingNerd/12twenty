using System;

using Microsoft.Extensions.Logging;

public class BankAccountProcessor
{
	private readonly BankDatabaseContext _dbContext;
	private readonly BankAccountValidator _validator;
	private readonly AuthorizationProcessor _authorizationProcessor;
	private readonly ILogger<BankAccountProcessor> _logger;

	public BankAccountProcessor(BankDatabaseContext dbContext, BankAccountValidator validator,AuthorizationProcessor authorizationProcessor, ILogger<BankAccountProcessor> logger)
	{
		// Pretend these have interfaces. excluded for brevity.
		_dbContext = dbContext;
		_validator = validator;
		_authorizationProcessor = authorizationProcessor;
		_logger = logger;
	}

	public void Withdraw(RequestContext context, WithdrawRequest request)
	{
		_authorizationProcessor.VerifyAuthorization(context, request);

		BankAccountRecord account = _dbContext.Accounts.FirstOrDefault(x => x.Id == request.AccountId) ?? throw new Exception("Account not found.");
		_validator.ValidateWithdrawRequest(account, request);

		account.Withdraw(context, request);
		_dbContext.SaveChanges();

		_logger.LogInformation("User {2} withdrew {0} on {1}", request.Amount, DateTime.UtcNow, context.UserId);
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
			_logger.LogInformation("Accumulated {0} interest on {1}", interest, DateTime.UtcNow);
		}

		_dbContext.SaveChanges();
	}
}
