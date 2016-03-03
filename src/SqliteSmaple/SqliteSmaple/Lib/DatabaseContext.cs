using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SqliteSmaple.Model;

namespace SqliteSmaple.Lib
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Todo> Todoes { get; set; }

    }  
}
