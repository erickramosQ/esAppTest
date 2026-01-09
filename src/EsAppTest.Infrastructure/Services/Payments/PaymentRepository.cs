using EsAppTest.Core.Entities;
using EsAppTest.Core.Services.Interfaces.Payments;
using EsAppTest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EsAppTest.Infrastructure.Services.Payments
{
    public sealed class PaymentsRepository : IPaymentsRepository
    {
        private readonly EsAppTestDbContext _db;

        public PaymentsRepository(EsAppTestDbContext db) => _db = db;

        public async Task AddAsync(Payment payment, CancellationToken ct)
        {
            _db.Payments.Add(payment);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<IReadOnlyList<Payment>> GetByCustomerIdAsync(Guid customerId, CancellationToken ct)
        {
            return await _db.Payments
                .AsNoTracking()
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(ct);
        }
    }
}
