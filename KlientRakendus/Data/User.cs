using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlientRakendus.Data
{
    public class User : IdentityUser
    {
        public string Token { get; set; }
    }
}
