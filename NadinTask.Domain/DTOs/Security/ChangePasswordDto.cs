using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.DTOs.Security
{
    public class ChangePasswordDto
    { [Required(ErrorMessage = nameof(Resources.Shared.RequiredError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string UserName { get; set; }

        [Required(ErrorMessage = nameof(Resources.Shared.RequiredError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = nameof(Resources.Shared.RequiredError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessageResourceName =nameof(Resources.Shared.ComparePasswordError), ErrorMessageResourceType =typeof(Resources.Shared))]
        [Required(ErrorMessage = nameof(Resources.Shared.RequiredError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string ConfirmPassword { get; set; }
    }
}
