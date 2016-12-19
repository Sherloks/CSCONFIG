using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data.Models
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }
        //public string Username { get; set; }
        //public string Email { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime LastLogin { get; set; }


        //public string PasswordHash { get; set; }
        //public string SecurityStamp { get; set; }

        public int SettingID { get; set; }

        public Setting Setting { get; set; }
        public ICollection<Configuration> Configurations { get; set; }
    }
}
