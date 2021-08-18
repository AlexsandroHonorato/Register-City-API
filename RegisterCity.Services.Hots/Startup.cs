using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using RegisteCity.Data.Repository.Base;
using RegisteCity.Data.Repository.Interfaces;
using RegisterCity.Application.Interfaces;
using RegisterCity.Application;
using RegisterCity.Services.Hots.Command;
using RegisterCity.Application.ViewModel;
using RegisterCity.Domain.Entities;
using RegisteCity.Data.Repository;

namespace RegisterCity.Services.Hots {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            ConfigureDI(services);
            ConfigureAutoMapper(services);
            services.AddCors(options => options.AddPolicy("Cors", builder => {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));
        }

        private void ConfigureDI(IServiceCollection services) {
            services.AddSingleton<IRepositoryBase>(ctx => new RepositoryBase(connectionString: Configuration.GetConnectionString("ConnBD")));

            services.AddScoped<ICityServices, CityServices>();
            services.AddScoped<ICityRepository, CityRepository>();
        }

        private void ConfigureAutoMapper(IServiceCollection services) {
            MapperConfiguration AutoMapperConfig = new AutoMapper.MapperConfiguration(cfg => {
               cfg.CreateMap<CityCommand, CityViewModel>().ReverseMap();
               cfg.CreateMap<City, CityViewModel>().ReverseMap();
            });

            IMapper mapper = AutoMapperConfig.CreateMapper();

            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();  

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("Cors"); // CORS

            app.UseAuthentication();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });       
        }
    }
}
