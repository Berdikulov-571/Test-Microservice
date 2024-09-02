using Microsoft.EntityFrameworkCore;
using Registration.Domain.Entities.Users;

namespace Registration.Application.Abstraction
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}