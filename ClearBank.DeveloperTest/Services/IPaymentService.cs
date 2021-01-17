using ClearBank.DeveloperTest.Dto;

namespace ClearBank.DeveloperTest.Services
{
    public interface IPaymentService
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
