namespace DataCon.Models
{
    public class BankAccount
    {
        public decimal Balance { get; private set; }
        public string AccountNumber { get; private set; }

        public BankAccount(decimal initialBalance, string accountNumber)
        {
            Balance = initialBalance;
            AccountNumber = accountNumber;
        }

        public void Deposit(decimal amount, string currency)
        {
            decimal convertedAmount = CurrencyConverter.ConvertToCAD(amount, currency);
            Balance += convertedAmount;
        }

        public void Withdraw(decimal amount, string currency)
        {
            decimal convertedAmount = CurrencyConverter.ConvertToCAD(amount, currency);
            if (convertedAmount <= Balance)
            {
                Balance -= convertedAmount;
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }

        public void SetBalance(decimal amount)
        {
            Balance = amount;
        }
    }
}
