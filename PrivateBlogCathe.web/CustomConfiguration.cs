using Microsoft.EntityFrameworkCore;
using PrivateBlogCathe.web.Data;
using PrivateBlogCathe.web.Services;

namespace PrivateBlogCathe.web
{
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder AddCustomBuilderConfiguration(this WebApplicationBuilder builder)
        {
            //Data context
            builder.Services.AddDbContext<DataContext>(configuration =>
            {
                configuration.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });

            //Services
            AddServices(builder);

            return builder;
                
        }

        public static void AddServices( WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISectionsService, SectionsService>();
        }

    }
}
