public class BankAccount
{
    // Private balance field
    private decimal balance;

    // Read-only Balance property
    public decimal Balance
    {
        get { return balance; }
    }

    // Deposit money
    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Deposit amount cannot be negative.");
        }

        balance += amount;
    }

    // Withdraw money
    public void Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Withdrawal amount cannot be negative.");
        }

        if (amount > balance)
        {
            throw new InvalidOperationException("Insufficient balance.");
        }

        balance -= amount;
    }
}
