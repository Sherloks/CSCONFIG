using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Data
{
    //public class DatabaseContextFactory : IDbContextFactory<DatabaseContext>
    //{
    //    private const string DATABASE_NAME = "csconfig";


    //    public DatabaseContext Create(DbContextFactoryOptions options)
    //    {
    //        var builder = new DbContextOptionsBuilder<DatabaseContext>();
    //        builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=" + DATABASE_NAME + ";Trusted_Connection=True;MultipleActiveResultSets=true");
    //        return new DatabaseContext(builder.Options);
    //    }
    //}
}
