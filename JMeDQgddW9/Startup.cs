using JMeDQgddW9.Data;
using JMeDQgddW9.Data.Repositories;
using JMeDQgddW9.Data.Repositories.Interfaces;
using JMeDQgddW9.Security;
using JMeDQgddW9.Service.Services;
using JMeDQgddW9.Service.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JMeDQgddW9
{
    public class Startup
    {
        public Startup()//IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            //Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(ApiAuthorizeAttribute));
                config.Filters.Add(typeof(AllowAnonymousFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBConnectionString")));

            services.AddSingleton<IConfiguration>(Configuration);

            //User
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            //Authentication
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                Context context = serviceScope.ServiceProvider.GetRequiredService<Context>();
                context.Database.EnsureCreated();
            }
        }
    }
}
