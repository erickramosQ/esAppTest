using EsAppTest.Core.Enums;

namespace EsAppTest.Core.Entities
{
    public sealed class Payment
    {
        public Guid PaymentId { get; private set; } = Guid.NewGuid();
        public Guid CustomerId { get; private set; }
        public string ServiceProvider { get; private set; } = default!;
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = "BOB";
        public PaymentStatus Status { get; private set; } = PaymentStatus.Pending;
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;

        private Payment() { }

        public Payment(Guid customerId, string serviceProvider, decimal amount, string currency)
        {
            CustomerId = customerId;
            ServiceProvider = serviceProvider.Trim();
            Amount = amount;
            Currency = currency.ToUpperInvariant();
            Status = PaymentStatus.Pending;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
