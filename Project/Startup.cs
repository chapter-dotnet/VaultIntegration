using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Data.SqlClient;
using VaultAPI.Model;
using VaultAPI.Models;

namespace VaultAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<VaultOptions>(Configuration.GetSection("Vault"));

            var dbBuilder = new SqlConnectionStringBuilder(
              Configuration.GetConnectionString("Database")
            );

            if (Configuration["database:userID"] != null)
            {
                dbBuilder.UserID = Configuration["database:userID"];
                dbBuilder.Password = Configuration["database:password"];

                Configuration.GetSection("ConnectionStrings")["Database"] = dbBuilder.ConnectionString;
            }

            services.AddDbContext<ProjectContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("Database")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project v1"));


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
