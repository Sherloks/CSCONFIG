using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class Configuration
    {
        public int ConfigurationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<ConfigurationAttribute> ConfigurationAttributes { get; set; }
    }
}
