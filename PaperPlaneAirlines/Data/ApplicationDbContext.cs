using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaperPlaneAirlines.ViewModels;

namespace PaperPlaneAirlines.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<PaperPlaneAirlines.ViewModels.MenuVM> MenuVM { get; set; } = default!;
}