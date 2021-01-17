﻿using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Dto;

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
            if (account == null)
            {
                result.Success = false;
                return result;
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

            result.Success = account.Withdraw(request.Amount, request.PaymentScheme);

            if (result.Success)
            {
                accountDataStore.UpdateAccount(account);
            }

            return result;
        }
    }
}
