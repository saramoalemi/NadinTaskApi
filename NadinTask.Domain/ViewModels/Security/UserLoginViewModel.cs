using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinTask.Domain.ViewModels.Security
{
    public class UserLoginViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Name { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
     
    }
}
