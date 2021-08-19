using Alea_Books_API.Web.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.Dtos
{
    public class PublicationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime PublishedOn { get; set; }

        public virtual Writer Author { get; set; }

        public decimal Rating { get; set; }
    }
}
