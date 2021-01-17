using ClearBank.DeveloperTest.Domain;
using ClearBank.DeveloperTest.Dto;
using System;

namespace ClearBank.DeveloperTest.Services
{
    public class RequestedToAccountPaymentSchemeMapper : IRequestedToAccountPaymentSchemeMapper
    {
        public AccountPaymentScheme MapFrom(RequestedPaymentScheme requested)
        {
            switch (requested)
            {
                case RequestedPaymentScheme.FasterPayments:
                    return AccountPaymentScheme.FasterPayments;
                case RequestedPaymentScheme.Bacs:
                    return AccountPaymentScheme.Bacs;
                case RequestedPaymentScheme.Chaps:
                    return AccountPaymentScheme.Chaps;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
