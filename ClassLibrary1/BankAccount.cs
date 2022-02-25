﻿/*
Refactor the code
* Think in terms of business logic, coding style, encapsulation, design patterns, organization, bugs, performance, etc. 
* Assume the code will get complicated over time via e.g. more complex business rules and other business or engineering needs.
* What else should the team consider, e.g. persistence, validation, authorization? What would that code look like?
* Feel free to completely restructure the code, e.g. add new classes, etc.
* Feel free to pseudo-code/stub out classes/methods. Code does not need to compile or run.

Add some new features
* More complicated interest rate accumulation based on customer and account type (make up your own rules, e.g. 'platinum' accounts get a higher interest rate)
* Integration with other systems, e.g. sending a notification when a withrawal is made, depending on the user's settings

Timebox: 25m-30m
*/
public class BankAccount
{
    public decimal Balance { get; private set; }

    public void Withdraw(decimal amount)
    {
        Balance -= amount;
        Log("Withdrew {0} on {1}", amount, DateTime.Now);
    }

    public void AccumulateInterest(decimal baseRate)
    {
        decimal interest;

        if (Balance < 10000)
        {
            interest = Balance * baseRate;
        }
        else
        {
            interest = Balance * (baseRate + 0.01m);
        }
		
        Log("Accumulated {0} interest on {1}", interest, DateTime.Now);
    }

    void Log(string message, params object[] parameters)
    {
        FileStream fs = File.Open("auditlog.txt", FileMode.OpenOrCreate);
		
        StreamWriter writer = new StreamWriter(fs);
        writer.WriteLine(message, parameters);
    }
}
