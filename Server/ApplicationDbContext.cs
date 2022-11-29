using System;
using Microsoft.EntityFrameworkCore;
using LetrasBlog.Infraestructure.Entities;

namespace LetrasBlog.Server
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Articles> Articles { get; set; }
    }
}
