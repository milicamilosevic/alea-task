using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.RequestModels
{
    public class RatePublication_RequestModel
    {
        public int PublicationId { get; set; }
        public int Rating { get; set; }
    }
}
