using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace JieDDDFramework.Module.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
