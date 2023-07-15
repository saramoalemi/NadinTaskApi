using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace NadinTask.Domain.Models.Products
{
    public class Product : ObjectModel<long>
    {

        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(11)]
        public string ManufacturePhone { get; set; }

        public string ManufactureEmail { get; set; }


        public DateTime ProductDate { get; set; }

        public bool IsAvailable { get; set; }
        
    }
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "Products");

            builder.HasIndex(i => new { i.ManufactureEmail, i.ProductDate })
             .HasFilter("[IsDeleted_]=0")
             .HasDatabaseName("UK_Product_ManufactureEmail_ProductDate")
             .IsUnique();


        }
    }
}
