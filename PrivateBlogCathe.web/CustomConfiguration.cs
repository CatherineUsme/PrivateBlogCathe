using Microsoft.EntityFrameworkCore;
using PrivateBlogCathe.web.Data;

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
            return builder;
        
        
        }

    }
}
