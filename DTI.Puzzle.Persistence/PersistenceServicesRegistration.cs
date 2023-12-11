using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Domain.Utilities;
using DTI.Puzzle.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var persistStorage = configuration
                .GetSection("Storage")["EnablePersistence"]
                .ConvertBooleanFromConfiguation();

            if (persistStorage)
            {
                services.AddDbContextPool<GlossaryDbContext>(builder =>
                {
                    var connectionString = configuration.GetSection("Storage")["ConnectionStrings:DefaultConnection"];
                    if (connectionString == null)
                    {
                        throw new ArgumentNullException("registration");
                    }
                    builder.UseSqlServer(connectionString);
                });


                services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                services.AddScoped<IUnitOfWork, UnitOfWork>();

                return services;
            }
            services.AddSingleton<GlossaryInMemoryDbContext>();
            services.AddScoped<IUnitOfWork, Repositories.InMemory.UnitOfWork>();
            services.AddScoped<IGlossaryItemRepository, Repositories.InMemory.GlossaryItemRepository>();
            services.AddScoped<IHistoryChangeRepository, Repositories.InMemory.HistoryChangesRepository>();

            return services;
        }

    }
}
