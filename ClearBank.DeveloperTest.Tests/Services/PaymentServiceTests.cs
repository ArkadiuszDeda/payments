using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Domain;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using NSubstitute;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        private readonly PaymentService sut;
        private readonly IAccountDataStore dataStore;
        
        public PaymentServiceTests()
        {
            dataStore = Substitute.For<IAccountDataStore>();
            sut = new PaymentService(dataStore);
        }

        [Theory]
        [InlineData(PaymentScheme.Bacs)]
        [InlineData(PaymentScheme.FasterPayments)]
        [InlineData(PaymentScheme.Chaps)]
        public void MakePayment_AccountIsNull_ReturnsFalse(PaymentScheme reuestedScheme)
        {
            dataStore.GetAccount("").ReturnsForAnyArgs(default(Account));
            var request = new MakePaymentRequest { PaymentScheme = reuestedScheme };

            var result = sut.MakePayment(request);

            Assert.False(result.Success);
        }

        [Fact]
        public void MakePayment_ValidAccount_ReturnsTrue()
        {
            var validAccount = GetValidAccount();
            dataStore.GetAccount("").ReturnsForAnyArgs(validAccount);
            var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Bacs };

            var result = sut.MakePayment(request);

            Assert.True(result.Success);
        }

        [Fact]
        public void MakePayment_ValidAccount_UpdatesAccount()
        {
            var validAccount = GetValidAccount();
            dataStore.GetAccount("").ReturnsForAnyArgs(validAccount);
            var request = new MakePaymentRequest { PaymentScheme = PaymentScheme.Bacs };

            sut.MakePayment(request);

            dataStore.Received(1).UpdateAccount(validAccount);
        }

        private static Account GetValidAccount()
        {
            return new Account("", 1, AllowedPaymentSchemes.Bacs, AccountStatus.Live);
        }
    }
}
