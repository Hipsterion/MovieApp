using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoviesApp.Domain
{
    public class Member : Entity<int>
    {
        public string RealName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
