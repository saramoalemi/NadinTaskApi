using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.Models.Security
{
    public class Role : IdentityRole<int>
    {
        public System.DateTime CreationDate { get; set; } = DateTime.Now;
        public string? Description { get; set; }
        public bool IsDeleted_ { get; set; } = false;
        public virtual ICollection<RolePermission> Role_Permission_List { get; set; }
        public virtual ICollection<UserRole> Role_User_List { get; set; }

    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            
                builder.ToTable("Roles", "Security");
                builder.HasMany(m => m.Role_Permission_List)
                    .WithOne(m => m.Permission_Role)
                    .HasForeignKey(m => m.RoleID);
                builder.HasMany(e => e.Role_User_List)
                   .WithOne(e => e.Role)
                   .HasForeignKey(ur => ur.RoleId)
                   .IsRequired();           
        }
    }
}
