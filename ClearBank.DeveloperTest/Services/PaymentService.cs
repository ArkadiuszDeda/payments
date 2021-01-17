using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Dto;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore accountDataStore;
        private readonly IRequestedToAccountPaymentSchemeMapper mapper;

        public PaymentService(IAccountDataStore accountDataStore, IRequestedToAccountPaymentSchemeMapper mapper)
        {
            this.accountDataStore = accountDataStore;
            this.mapper = mapper;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = accountDataStore.GetAccount(request.DebtorAccountNumber);

            if (account == null)
            {
                return new MakePaymentResult { Success = false };
            }

            var result = account.Withdraw(request.Amount, mapper.MapFrom(request.PaymentScheme));

            if (result)
            {
                accountDataStore.UpdateAccount(account);
            }

            return new MakePaymentResult { Success = result };
        }
    }
}
