using ClearBank.DeveloperTest.Domain;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Domain
{
    public class AccountWithdrawTests
    {
        [Theory]
        [InlineData(AccountStatus.Disabled, false)]
        [InlineData(AccountStatus.InboundPaymentsOnly, false)]
        [InlineData(AccountStatus.Live, true)]
        public void Withdraw_AccountStatusCheck_ReturnsCorrectResult(AccountStatus status, bool expected)
        {
            var sut = new Account("any", 2, AccountPaymentScheme.Bacs, status);

            var result = sut.Withdraw(1, AccountPaymentScheme.Bacs);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(AccountStatus.Disabled)]
        [InlineData(AccountStatus.InboundPaymentsOnly)]
        public void Withdraw_AccountNotLive_DoesNotChangeBalance(AccountStatus status)
        {
            var expectedBalance = 2;
            var sut = new Account("any", 2, AccountPaymentScheme.Bacs, status);

            var result = sut.Withdraw(1, AccountPaymentScheme.Bacs);

            Assert.Equal(expectedBalance, sut.Balance);
        }

        [Fact]
        public void Withdraw_BalanceSmallerThatRequestedAmmount_DoesNotChangeBalance()
        {
            var expectedBalance = 1;
            var sut = new Account("any", expectedBalance, AccountPaymentScheme.Bacs, AccountStatus.Live);

            var result = sut.Withdraw(2, AccountPaymentScheme.Bacs);

            Assert.Equal(expectedBalance, sut.Balance);
            Assert.False(result);
        }

        [Fact]
        public void Withdraw_BalanceEqualRequestedAmmount_DeductsRequestedAmmount()
        {
            var expectedBalance = 0;
            var sut = new Account("any", 1, AccountPaymentScheme.Bacs, AccountStatus.Live);

            var result = sut.Withdraw(1, AccountPaymentScheme.Bacs);

            Assert.Equal(expectedBalance, sut.Balance);
            Assert.True(result);
        }

        [Fact]
        public void Withdraw_BalanceGreaterRequestedAmmount_DeductsRequestedAmmount()
        {
            var expectedBalance = 4;
            var sut = new Account("any", 5, AccountPaymentScheme.Bacs, AccountStatus.Live);

            var result = sut.Withdraw(1, AccountPaymentScheme.Bacs);
            
            Assert.Equal(expectedBalance, sut.Balance);
            Assert.True(result);
        }

        [Theory]
        [InlineData(AccountPaymentScheme.Bacs)]
        [InlineData(AccountPaymentScheme.Chaps)]
        [InlineData(AccountPaymentScheme.FasterPayments)]
        public void Withdraw_RequestedPaymentSchemesAllowed_DeductsRequestedAmmount(AccountPaymentScheme requested)
        {
            var expectedBalance = 4;
            var sut = new Account("any", 5, AccountPaymentScheme.Bacs | AccountPaymentScheme.Chaps | AccountPaymentScheme.FasterPayments, AccountStatus.Live);

            var result = sut.Withdraw(1, requested);

            Assert.Equal(expectedBalance, sut.Balance);
            Assert.True(result);
        }

        [Theory]
        [InlineData(AccountPaymentScheme.Bacs, AccountPaymentScheme.FasterPayments)]
        [InlineData(AccountPaymentScheme.Chaps, AccountPaymentScheme.FasterPayments)]
        [InlineData(AccountPaymentScheme.FasterPayments, AccountPaymentScheme.Bacs)]
        public void Withdraw_RequestedPaymentSchemesNotAllowed_DoesNotChangeBalance(AccountPaymentScheme requested, AccountPaymentScheme allowed)
        {
            var expectedBalance = 4;
            var sut = new Account("any", expectedBalance, allowed, AccountStatus.Live);

            var result = sut.Withdraw(1, requested);

            Assert.Equal(expectedBalance, sut.Balance);
            Assert.False(result);
        }
    }
}
