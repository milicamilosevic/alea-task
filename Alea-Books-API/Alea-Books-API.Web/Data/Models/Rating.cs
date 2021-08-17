using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.Data.Models
{
    public class Rating
    {
        
        [Required]  
        public int Stars { get; set; }

        [Required]
        public virtual IdentityUser User { get; set; }

        [Required]
        public virtual Publication Publication{ get; set; }
    }
}
