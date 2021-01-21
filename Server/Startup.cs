using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Server.Models;

namespace Server
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
            services.AddCors(options => // omogucavamo da pristupamo nasim metodama iz domena naseg sajta
            {
                options.AddPolicy("CORS", builder => 
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithOrigins(new string[]
                           {
                               "http://127.0.0.1:5500" // live server link 
                           });
                });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server", Version = "v1" });
            });
            services.AddDbContext<BankContext>(options => // sada je BankContext dostupan kroz konstruktor iz bilo koje klase koje koristimo u aplikaciji
            {   // sada da povezemo connection string sa nasim db-om
                options.UseSqlServer(Configuration.GetConnectionString("BankaCS")); // dodali smo referencu Microsoft.EntityFrameworkCore.SqlServer
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CORS"); // potrebno da mozemo da pristupimo sajtu sa naseg domena

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
