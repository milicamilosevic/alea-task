using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.Data
{
    public class BooksDbContext: IdentityDbContext
    {
      
        public BooksDbContext(DbContextOptions<BooksDbContext> options)
            : base(options)
        {
           
        }

        
    }
}
