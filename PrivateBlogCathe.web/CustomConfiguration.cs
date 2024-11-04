using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
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

            //Toast Notificacion
            builder.Services.AddNotyf(config => 
            {
                config.DurationInSeconds = 10; 
                config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; 
            });

            return builder;
                
        }

        public static void AddServices( WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISectionsService, SectionsService>();
        }

        public static WebApplication AddCustomWebAppConfiguration( this WebApplication app)
        {
            app.UseNotyf();
            return app;
        }

    }
}
