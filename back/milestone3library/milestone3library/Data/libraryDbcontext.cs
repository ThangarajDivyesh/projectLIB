using Microsoft.EntityFrameworkCore;
using milestone3library.Entity;

namespace milestone3library.Data
{
    public class libraryDbcontext : DbContext
    {
        public libraryDbcontext(DbContextOptions options) : base(options)
        { }

        public DbSet<Member>Members { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Genre { get; set; }    

        public DbSet<Author>Authors { get; set; }

        public DbSet<BookTransaction> BookTransactions { get; set; }



        public DbSet<BookRequest> BookRequests { get; set; }
        public DbSet<BookResponse> BookResponses { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
