using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;


namespace mysqlefcore{
    public class LibraryContext : DbContext{
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=library;user=root;password=yourdatabaserootpassword");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publisher>(entity=>{
                entity.HasKey(e=>e.ID);
                entity.Property(e=>e.Name).IsRequired();
            });

            modelBuilder.Entity<Book>(entity=>{
                entity.HasKey(e=>e.ISBN);
                entity.Property(e=>e.Title).IsRequired();
                entity.HasOne(e=>e.Publisher)
                    .WithMany(p=>p.Books);
            });
        }
    }
}