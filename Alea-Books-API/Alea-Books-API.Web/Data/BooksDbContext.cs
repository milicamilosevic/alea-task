using Alea_Books_API.Web.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alea_Books_API.Web.Data
{
    public class BooksDbContext : IdentityDbContext
    {
        public virtual DbSet<Publication> Publications { get; set; }
        public virtual DbSet<PublicationType> PublicationTypes { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Writer> Writers { get; set; }

        public BooksDbContext(DbContextOptions<BooksDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Rating>().HasKey(rating => new { rating.UserId, rating.PublicationId });
        }
    }
}
