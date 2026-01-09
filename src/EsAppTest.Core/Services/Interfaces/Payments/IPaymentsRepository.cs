using EsAppTest.Core.Entities;

namespace EsAppTest.Core.Services.Interfaces.Payments
{
    public interface IPaymentsRepository
    {
        Task AddAsync(Payment payment, CancellationToken ct);
        Task<IReadOnlyList<Payment>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct);
    }
}
