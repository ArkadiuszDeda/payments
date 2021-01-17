using ClearBank.DeveloperTest.Data;
using System.Configuration;

namespace ClearBank.DeveloperTest
{
    // Just an example how Payment service could be created using dependency injection
    // Another option is to use some kind of factory pattern and create Payment service with desired dependency there
    public class FakeExampleStartup
    {
        public void Register()
        {
            var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

            IAccountDataStore accountDataStore;
            if (dataStoreType == "Backup")
            {
                accountDataStore = new BackupAccountDataStore();
            }
            else
            {
                accountDataStore = new AccountDataStore();
            }

            // And then i.e. 
            // serviceCollection.AddSingleton<IPaymentService>(new PaymentService(accountDataStore));
            // This being just an example of home recruitment task I decided against adding actual DI packages etc here
            // like there is no real data access in DataStore
            // Also singleton is just an example
        }
    }
}
