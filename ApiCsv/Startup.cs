using ApiCsv.Services;
using ApiCsv.Utils;
using AutoMapper;
using Contracts;
using Entities;
using Entities.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Repository;

namespace ApiCsv
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
            // -------------------------------------------------------------------------------
            services.AddCors(); // va de primero

            // connection in memory
            services.AddDbContext<MemoryContext>
                (
                    opt =>
                    {
                        opt.UseInMemoryDatabase(databaseName: "DB_memory");
                    }
                );


            //---
            // service manager
            //services.AddScoped<IMemoryContext, MemoryContext>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddSingleton<IUploads, Uploads>();
            services.AddSingleton<IConvertFile, ConvertFile>();
            //---




            //---
            // Mapper: to map between two objects
            var mapperConfig = new MapperConfiguration(m => {
                m.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            //---

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiCsv", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ---
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:3000");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });
            // ---


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCsv v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //---
            // seed data for db memory
            var scope = app.ApplicationServices.CreateScope();
            var context1 = scope.ServiceProvider.GetService<MemoryContext>();
            DbMemoryConfiguration.SeedData(context1);
            //---
        }
    }
}
