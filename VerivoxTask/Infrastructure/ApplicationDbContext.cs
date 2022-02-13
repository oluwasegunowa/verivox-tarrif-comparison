using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using VerivoxTask.Domain.Entities;

namespace VerivoxTask.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(IConfiguration config)
        {
            _config = config;
        }

        private readonly IConfiguration _config;

     
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config["ConnectionStrings:DefaultConnection"]);
        }

    }
}
