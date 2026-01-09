using EsAppTest.Core.Entities;
using EsAppTest.Core.Enums;
using EsAppTest.Core.Services.Interfaces.Payments.Request;
using EsAppTest.Core.Services.Interfaces.Payments.Response;

namespace EsAppTest.Core.Services.Interfaces.Payments.Services
{
    public sealed class PaymentsService
    {
        private readonly IPaymentsRepository _repo;

        public PaymentsService(IPaymentsRepository repo)
        {
            _repo = repo;
        }

        public async Task<PCreatePaymentResponse> CreateAsync(PCreatePaymentRequest request, CancellationToken ct)
        {
            if (request.CustomerId == Guid.Empty)
                throw new ArgumentException("CustomerId inválido.");

            if (string.IsNullOrWhiteSpace(request.ServiceProvider))
                throw new ArgumentException("ServiceProvider es requerido.");

            if (request.Amount <= 0)
                throw new ArgumentException("El monto debe ser mayor a 0.");

            if (request.Amount > 1500m)
                throw new ArgumentException("No se permiten montos mayores a 1500 Bs.");

            if (!string.Equals(request.Currency?.Trim(), "BOB", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Solo se permiten pagos en Bolivianos (BOB). No se aceptan dólares.");

            var payment = new Payment(
                request.CustomerId,
                request.ServiceProvider,
                request.Amount,
                request.Currency
            );

            await _repo.AddAsync(payment, ct);

            return new PCreatePaymentResponse
            {
                PaymentId = payment.PaymentId,
                ServiceProvider = payment.ServiceProvider,
                Amount = payment.Amount,
                Status = "pendiente",
                CreatedAt = payment.CreatedAt
            };
        }

        public async Task<IReadOnlyList<PGetPaymentsResponse>> GetByCustomerIdAsync(PGetPaymentsRequest request, CancellationToken ct)
        {
            if (request.CustomerId == Guid.Empty)
                throw new ArgumentException("CustomerId inválido.");

            var payments = await _repo.GetByCustomerIdAsync(request.CustomerId, ct);

            return payments
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new PGetPaymentsResponse
                {
                    PaymentId = x.PaymentId,
                    ServiceProvider = x.ServiceProvider,
                    Amount = x.Amount,
                    Status = x.Status == PaymentStatus.Pending
                        ? "pendiente"
                        : x.Status.ToString().ToLowerInvariant(),
                    CreatedAt = x.CreatedAt
                })
                .ToList();
        }
    }
}
