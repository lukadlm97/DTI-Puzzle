using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence
{
    public class GlossaryDbContextFactory : IDesignTimeDbContextFactory<GlossaryDbContext>
    {
        public GlossaryDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<GlossaryDbContext>();
            var connectionString = configuration.GetSection("Storage")["ConnectionStrings:DefaultConnection"];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("factory");
            }
            builder.UseSqlServer(connectionString);

            return new GlossaryDbContext(builder.Options);
        }
    }
}
