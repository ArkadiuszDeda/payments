namespace ClearBank.DeveloperTest.Dto
{
    public class MakePaymentResult
    {
        public bool Success { get; set; }

        // Improvement idea: add string Error or reason to be able to bubble up the reason for failure
        // It needs to be added as well in Domain.Account Withdraw method
        // Then we can create static Factory Method i.e. CreateSuccess() and CreateFailure(string error)
    }
}
