using ClearBank.DeveloperTest.Domain;
using System;

namespace ClearBank.DeveloperTest.Dto
{
    public class MakePaymentRequest
    {
        public string CreditorAccountNumber { get; set; }

        public string DebtorAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public RequestedPaymentScheme PaymentScheme { get; set; }
    }
}
