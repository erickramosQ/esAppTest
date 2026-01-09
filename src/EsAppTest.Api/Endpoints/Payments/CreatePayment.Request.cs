namespace EsAppTest.Api.Endpoints.Payments
{
    public class CreatePaymentRequest
    {
        public Guid CustomerId { get; set; }
        public string ServiceProvider { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "BOB";

    }
}
