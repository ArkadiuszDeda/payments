namespace ClearBank.DeveloperTest.Domain
{
    public class Account
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public AccountStatus Status { get; private set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; private set; }

        // Probably in prod ready code we do not need some of the below arguments, instead we would default it to specific values
        public Account(string accountNumber, decimal balance, AllowedPaymentSchemes allowedPaymentSchemes, AccountStatus status)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            AllowedPaymentSchemes = allowedPaymentSchemes;
            Status = status;
        }

        public bool Withdraw(decimal amounth, PaymentScheme scheme)
        {
            return true;
        }

    }
}
