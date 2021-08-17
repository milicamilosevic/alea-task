using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.Data.Models
{
    public class Publication
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }  
        
        public string Image{ get; set; }

        public DateTime PublishedOn { get; set; }

        public virtual Writer Author{ get; set; }

        public virtual PublicationType Type { get; set; }
    }
}
