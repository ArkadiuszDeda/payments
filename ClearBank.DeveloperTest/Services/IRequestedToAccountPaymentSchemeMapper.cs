using ClearBank.DeveloperTest.Domain;
using ClearBank.DeveloperTest.Dto;

namespace ClearBank.DeveloperTest.Services
{
    public interface IRequestedToAccountPaymentSchemeMapper
    {
        AccountPaymentScheme MapFrom(RequestedPaymentScheme requested);
    }
}