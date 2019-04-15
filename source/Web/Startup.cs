using DotNetCore.AspNetCore;
using DotNetCore.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            Configuration = environment.Configuration();
            Environment = environment;
        }

        private IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

        public void Configure(IApplicationBuilder application)
        {
            application.UseExceptionMiddleware(Environment);
            application.UseCorsAllowAny();
            application.UseHttps();
            application.UseAuthentication();
            application.UseResponseCompression();
            application.UseResponseCaching();
            application.UseStaticFiles();
            application.UseMvcWithDefaultRoute();
            application.UseHealthChecks();
            application.UseSwaggerDefault();
            //application.UseSpa(Environment);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogger(Configuration);
            services.AddCors();
            services.AddJsonWebToken();
            services.AddHash();
            services.AddAuthentication().AddJwtBearer(options => options.Events.OnTokenValidated = ctx =>
            {
                //Gerekirse burada gelen token içerisindeki çeşitli bilgilere göre doğrulam yapılabilir.
                return Task.CompletedTask;
            });
            //services.AddAuthenticationJwtBearer();
            services.AddResponseCompression();
            services.AddResponseCaching();
            services.AddMvcDefault();
            services.AddHealthChecks();
            services.AddSwaggerDefault();
            //services.AddSpa();
            services.AddFileService();
            services.AddApplicationServices();
            services.AddDomainServices();
            services.AddDatabaseServices();
            services.AddDatabaseContext(Configuration);
        }
    }
}
