namespace ClearBank.DeveloperTest.Domain
{
    public class Account
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public AccountStatus Status { get; private set; }
        public AccountPaymentScheme AllowedPaymentSchemes { get; private set; }

        // Probably in prod ready code we do not need some of the below arguments, instead we would default it to specific values
        public Account(string accountNumber, decimal balance, AccountPaymentScheme allowedPaymentSchemes, AccountStatus status)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            AllowedPaymentSchemes = allowedPaymentSchemes;
            Status = status;
        }

        public bool Withdraw(decimal amounth, AccountPaymentScheme scheme)
        {
            if (Status != AccountStatus.Live)
            {
                return false;
            }
            if (Balance < amounth)
            {
                return false;
            }
            if(AllowedPaymentSchemes.HasFlag(scheme) == false)
            {
                return false;
            }

            // More realistic approach would be event sourc, where we add proper event, and calculate the balance from it (and the rest of the aggregate state in that case)
            Balance -= amounth;
            return true;
        }

    }
}
