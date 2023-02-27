using Microsoft.EntityFrameworkCore;

namespace BirdClone.DbContext;

public class AccountDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AccountDbContext (DbContextOptions<AccountDbContext> options)
        : base(options)
    {
    }

    public DbSet<RazorPagesContacts.Models.Customer> Customer => Set<RazorPagesContacts.Models.Customer>();
}
//TODO https://learn.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-7.0&tabs=visual-studio