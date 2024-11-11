using IBKS.DataAccess.Entities;
using IBKS.DataAccess.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IBKS.DataAccess;

public class ApplicationContext : IdentityDbContext<User, Role, Guid>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Reply> Replies { get; set; }
}
