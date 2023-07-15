
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  NadinTask.Domain.DTOs.Security
{
    public class UserRegisterDto
    {
        public string? Role_User { get; set; }

         [DataType(DataType.Password)]
        public string? Password { get; set; }

         [Compare("Password", ErrorMessageResourceName = nameof(NadinTask.Domain.Resources.Shared.ComparePasswordError), ErrorMessageResourceType = typeof(Resources.Shared))]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [MaxLength(length: 150, ErrorMessageResourceName = nameof(Resources.Shared.MaxLengthError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string? Name_User { get; set; }

        [MaxLength(length: 150, ErrorMessageResourceName = nameof(Resources.Shared.MaxLengthError), ErrorMessageResourceType = typeof(Resources.Shared))]
        [Required(ErrorMessage = nameof(Resources.Shared.RequiredError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string UserName { get; set; }

        [StringLength(maximumLength: 15, MinimumLength = 10, ErrorMessageResourceName = nameof(Resources.Shared.StringLengthError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string? NationalCode_User { get; set; }


         [MaxLength(length: 150, ErrorMessageResourceName = nameof(Resources.Shared.MaxLengthError), ErrorMessageResourceType = typeof(Resources.Shared))]
        [EmailAddress(ErrorMessage = nameof(Resources.Shared.FormatError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string? Email { get; set; }

    }
        
}
