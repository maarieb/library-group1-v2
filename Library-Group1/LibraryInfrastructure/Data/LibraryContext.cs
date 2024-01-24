using Library.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Domain> Domains { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().UseTpcMappingStrategy(); 
            modelBuilder.Entity<Reader>().ToTable("Readers"); 
            modelBuilder.Entity<Writer>().ToTable("Writers"); 
            modelBuilder.Entity<Admin>().ToTable("Admins");


            modelBuilder.Entity<Person>().Property(b => b.LastName).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<Person>().Property(b => b.FirstName).IsRequired().HasMaxLength(32);
            modelBuilder.Entity<Reader>().Property(b => b.Mail).IsRequired();
            modelBuilder.Entity<Admin>().Property(b => b.Mail).IsRequired();

            modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired();

            modelBuilder.Entity<Loan>().Property(b => b.StartDate).IsRequired();

        }
    }
}
