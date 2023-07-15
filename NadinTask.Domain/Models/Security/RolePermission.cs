using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NadinTask.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.Models.Security
{
    public class RolePermission : ObjectModel<long>
    {
        
        public string PermissionName { get; set; }

        public int RoleID { get; set; }
        public virtual Role Permission_Role { get; set; }

    }

    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {

        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions", "Security");
           
        }
    }
}
