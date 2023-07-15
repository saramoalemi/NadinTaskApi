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
    public class RoleDto
    {
        public int Id { get; set; }

        [MaxLength(length: 250, ErrorMessageResourceName = nameof(Resources.Shared.MaxLengthError), ErrorMessageResourceType = typeof(Resources.Shared))]
        [Required(ErrorMessage = nameof(Resources.Shared.RequiredError), ErrorMessageResourceType = typeof(Resources.Shared))] 
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
