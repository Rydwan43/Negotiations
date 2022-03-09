using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Negotiations.Application.Settings
{
    public class JwtSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}