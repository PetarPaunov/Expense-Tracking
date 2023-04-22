namespace ExpenseTracking.Infrastructure
{
    using ExpenseTracking.Infrastructure.Models.Account;
    using ExpenseTracking.Infrastructure.Models.MappingTables;
    using ExpenseTracking.Infrastructure.Models.Wallet;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        // Db Sets here
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<ApplicationUserWallet> ApplicationUsersWallets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserWallet>()
                .HasKey(x => new { x.WalletId, x.ApplicationUserId });

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}