using Alea_Books_API.Web.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.Data
{
    public interface IPublicationData
    {
         List<Publication> GetPublications();

         Publication GetPublication(Guid Id);

         Publication EditPublication(Publication publication);
    }
}
