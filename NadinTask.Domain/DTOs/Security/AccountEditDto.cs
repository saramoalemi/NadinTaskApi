
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.DTOs.Security
{
    public class AccountEditDto
    {
        public int id { get; set; }

        [MaxLength(length: 150, ErrorMessageResourceName = nameof(Resources.Shared.MaxLengthError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string? Name_User { get; set; }

        [MaxLength(length: 150, ErrorMessageResourceName = nameof(Resources.Shared.MaxLengthError), ErrorMessageResourceType = typeof(Resources.Shared))]
        [Required(ErrorMessage = nameof(Resources.Shared.RequiredError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string UserName { get; set; }
      
        [MaxLength(length: 150, ErrorMessageResourceName = nameof(Resources.Shared.MaxLengthError), ErrorMessageResourceType = typeof(Resources.Shared))]
        [EmailAddress(ErrorMessage =nameof(Resources.Shared.FormatError), ErrorMessageResourceType =typeof(Resources.Shared))]
        public string? Email { get; set; }


    }
}
