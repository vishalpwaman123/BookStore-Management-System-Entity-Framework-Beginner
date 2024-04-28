using BookStoreManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreManagementSystem.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(p => p.UserId).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(p => p.InsertionDate).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<User>().Property(p => p.IsActive).HasDefaultValue(true);

            modelBuilder.Entity<Book>().Property(p => p.BookID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Book>().Property(p => p.InsertionDate).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Book>().Property(p => p.IsArchive).HasDefaultValue(false);
            modelBuilder.Entity<Book>().Property(p => p.IsActive).HasDefaultValue(true);
        }
    }
}
