using ClearBank.DeveloperTest.Domain;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountDataStore : IAccountDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Account("", 0, AccountPaymentScheme.FasterPayments, AccountStatus.Live);
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
