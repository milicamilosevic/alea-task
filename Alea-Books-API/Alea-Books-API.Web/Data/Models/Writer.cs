using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.Data.Models
{
    public class Writer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        public string Image { get; set; }

        public DateTime DateOfBirth { get; set; }

        //public virtual List<Publication> Publications { get; set; }
    }
}
