using NadinTask.Domain.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NadinTask.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Infrastructure
{
    public class NadinContext : IdentityDbContext<User, Role, int>
    {
        public NadinContext(DbContextOptions options) : base(options)
        {

        }

        #region Products
        public DbSet<Product> Product { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("NadinTask.Domain"));

            modelBuilder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable("RoleClaims", "Security"));
            modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("UserLogins", "Security"));
            modelBuilder.Entity<IdentityUserRole<int>>(entity => entity.ToTable("UserRoles", "Security"));
            modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("UserTokens", "Security"));
            modelBuilder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("UserClaims", "Security"));

        }
    }
}
