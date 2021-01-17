using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Domain;
using ClearBank.DeveloperTest.Dto;
using ClearBank.DeveloperTest.Services;
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
            sut = new PaymentService(dataStore, new RequestedToAccountPaymentSchemeMapper());
        }

        [Theory]
        [InlineData(RequestedPaymentScheme.Bacs)]
        [InlineData(RequestedPaymentScheme.FasterPayments)]
        [InlineData(RequestedPaymentScheme.Chaps)]
        public void MakePayment_AccountIsNull_ReturnsFalse(RequestedPaymentScheme reuestedScheme)
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
            var request = new MakePaymentRequest { PaymentScheme = RequestedPaymentScheme.Bacs };

            var result = sut.MakePayment(request);

            Assert.True(result.Success);
        }

        [Fact]
        public void MakePayment_ValidAccount_UpdatesAccount()
        {
            var validAccount = GetValidAccount();
            dataStore.GetAccount("").ReturnsForAnyArgs(validAccount);
            var request = new MakePaymentRequest { PaymentScheme = RequestedPaymentScheme.Bacs };

            sut.MakePayment(request);

            dataStore.Received(1).UpdateAccount(validAccount);
        }

        private static Account GetValidAccount()
        {
            return new Account("", 1, AccountPaymentScheme.Bacs, AccountStatus.Live);
        }
    }
}
