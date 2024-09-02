using Microsoft.EntityFrameworkCore;
using Registration.Application.Abstraction;
using Registration.Domain.Entities.Users;

namespace Registration.Inftastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            Database.Migrate();
        } 
        public DbSet<User> Users { get; set; }

        async ValueTask<int> IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}