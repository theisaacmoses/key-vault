using KeyVault.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyVault.Repository
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Vault> VaultSet { get; set; }
    }
}
