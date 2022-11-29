using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetrasBlog.Infraestructure
{
    public class SqlConfigurationContext : DbContext
    {

        public SqlConfigurationContext(DbContextOptions<SqlConfigurationContext> options)
                : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _ = new ConfigurationBuilder();

            }
        }
        
    }
}
