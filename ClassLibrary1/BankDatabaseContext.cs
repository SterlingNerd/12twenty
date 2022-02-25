using System;

public class BankDatabaseContext
{
	// Typical EF stuff here. we'll fake it for this example.
	public IQueryable<BankAccountRecord> Accounts { get; } = null!;

	public void SaveChanges()
	{
		
	}
}
