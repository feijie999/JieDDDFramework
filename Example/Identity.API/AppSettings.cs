using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JieDDDFramework.Core.Configures;

namespace Identity.API
{
    public class AppSettings : BaseConfig
    {
        public Certificates Certificates { get; set; }
    }

    public class Certificates
    {
        public string CerPath { get; set; }
        public string Password { get; set; }
    }
}
