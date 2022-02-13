using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerivoxTask.Application.Services;
using VerivoxTask.Domain.Extensions;
using VerivoxTask.Domain.Interfaces;
using VerivoxTask.Extensions;
using VerivoxTask.Infrastructure.Persistence;
using VerivoxTask.Infrastructure.Persistence.Extensions;

namespace VerivoxTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VerivoxApi", Version = "v1" });
            });

            services.AddRepository(Configuration.GetConnectionString("DefaultConnection"));
            services.AddScoped<ITarrifCalculationService, TarrifComparisonService>();
            services.RegisterAllTypes<ITarrifModel>(new[] { typeof(Startup).Assembly });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



           
            if (!db.Database.EnsureCreated())
            {
                db.Database.Migrate();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VerivoxApi v1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }



   
}
