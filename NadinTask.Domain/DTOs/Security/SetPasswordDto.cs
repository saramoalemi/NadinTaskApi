using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NadinTask.Domain.DTOs.Security
{
    public class SetPasswordDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = nameof(Resources.Shared.RequiredError), ErrorMessageResourceType = typeof(Resources.Shared))]
        public string Password { get; set; }
    }
}
