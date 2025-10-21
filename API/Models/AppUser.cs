
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AppUser
    {

        public long? Id { get; set; } = null;

        public required string Firstname { get; set; }

        public required string Lastname { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        
    }
}