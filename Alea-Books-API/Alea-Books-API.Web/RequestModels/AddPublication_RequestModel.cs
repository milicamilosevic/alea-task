using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.RequestModels
{
    public class AddPublication_RequestModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int TypeId { get; set; }
        public int WriterId { get; set; }
        public DateTime PublishedOn { get; set; }
    }
}
