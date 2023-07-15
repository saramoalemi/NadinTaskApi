using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.ViewModels.Products
{
    public class ProductViewModel: Base.BaseViewModel<long>
    {
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(11)]
        public string ManufacturePhone { get; set; }

        [EmailAddress]
        [Required]
        public string ManufactureEmail { get; set; }

        [Required]
        public DateTime ProductDate { get; set; }

        public bool IsAvailable { get; set; }
    }
}
