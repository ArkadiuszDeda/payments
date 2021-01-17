﻿using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore accountDataStore;

        public PaymentService(IAccountDataStore accountDataStore)
        {
            this.accountDataStore = accountDataStore;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var account = accountDataStore.GetAccount(request.DebtorAccountNumber);

            var result = new MakePaymentResult();
            result.Success = true;
            if (account == null)
            {
                result.Success = false;
            }

            //switch (request.PaymentScheme)
            //{
            //    case PaymentScheme.Bacs:
            //        if (account == null)
            //        {
            //            result.Success = false;
            //        }
            //        else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            //        {
            //            result.Success = false;
            //        }
            //        break;

            //    case PaymentScheme.FasterPayments:
            //        if (account == null)
            //        {
            //            result.Success = false;
            //        }
            //        else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            //        {
            //            result.Success = false;
            //        }
            //        else if (account.Balance < request.Amount)
            //        {
            //            result.Success = false;
            //        }
            //        break;

            //    case PaymentScheme.Chaps:
            //        if (account == null)
            //        {
            //            result.Success = false;
            //        }
            //        else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            //        {
            //            result.Success = false;
            //        }
            //        else if (account.Status != AccountStatus.Live)
            //        {
            //            result.Success = false;
            //        }
            //        break;
            //}

            if (result.Success)
            {
                account.Withdraw(request.Amount);

                accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
