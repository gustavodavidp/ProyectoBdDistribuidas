using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetrasBlog.Infraestructure.Data
{
    public class SqlConfigurationContextDev
    {
        public SqlConfigurationContextDev(string connectionString) => ConnectionString = connectionString;
        public string ConnectionString { get; }
    }
}
