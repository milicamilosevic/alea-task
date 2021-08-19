using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alea_Books_API.Web.Data.Models
{
    public class Rating
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int PublicationId { get; set; }

        [Required]
        public int Stars { get; set; }
    }
}
