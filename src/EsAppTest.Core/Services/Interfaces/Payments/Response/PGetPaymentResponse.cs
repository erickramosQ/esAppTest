namespace EsAppTest.Core.Services.Interfaces.Payments.Response
{
    public sealed class PGetPaymentsResponse
    {
        public Guid PaymentId { get; set; }
        public string ServiceProvider { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Status { get; set; } = default!;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
