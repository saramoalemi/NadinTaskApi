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
    public class UserLoginDto
    {
         [Required(ErrorMessage = nameof(NadinTask.Domain.Resources.Shared.RequiredError), ErrorMessageResourceType = typeof(NadinTask.Domain.Resources.Shared))] 
        public string UserName { get; set; }

         [Required(ErrorMessage = nameof(NadinTask.Domain.Resources.Shared.RequiredError), ErrorMessageResourceType = typeof(NadinTask.Domain.Resources.Shared))]
        public string Password { get; set; }
    }
}
