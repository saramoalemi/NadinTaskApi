using NadinTask.Domain.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.Models.Security
{
    public class User : IdentityUser<int>
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [MaxLength(150)]
        public string? Name_User { get; set; }
       
        [MaxLength(150)]
        public override string UserName { get; set; }
        public override string? Email { get; set; }
        public string? Last1_Pass { get; set; }
       
        public bool IsActive_ { get; set; } = true;
        public bool IsDeleted_ { get; set; }
        public virtual ICollection<UserRole> User_Role_List { get; set; }


    }

    public class UserRole : IdentityUserRole<int>
    {

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "Security");
           // builder.HasQueryFilter(x => !x.IsDeleted_);

          
            builder.HasMany(e => e.User_Role_List)
               .WithOne(e => e.User)
               .HasForeignKey(ur => ur.UserId)
               .IsRequired();

            builder.HasData(new User()
            {
                Id = 1,
                UserName = "user1",
                CreationDate = DateTime.Parse("2023-07-12 12:39:28.1934914"),
                PasswordHash = "AQAAAAIAAYagAAAAEKzoY5dZ2/K4di52Ji29gSdsEFPfqaFngRFF+FEH3JZGKaR6FXxcEuOkPaV2bWZO2Q==",
                SecurityStamp = "6JFSAMEXX5AUGHKIFVOR2MFD532PNEB7",
                ConcurrencyStamp = "659dde82-6598-4c4d-a3f8-6a8784be078e"
            });
        }
    }
}
