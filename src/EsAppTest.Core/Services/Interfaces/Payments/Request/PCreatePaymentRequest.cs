namespace EsAppTest.Core.Services.Interfaces.Payments.Request
{
    public sealed class PCreatePaymentRequest
    {
        public Guid CustomerId { get; set; }
        public string ServiceProvider { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "BOB";
    }
}
