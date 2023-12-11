using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application
{
    public static  class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureGlossaryApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ApplicationServicesRegistration)));

            return services;
        }
    }
}
