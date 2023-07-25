using comp1682_back_end.Data;
using comp1682_back_end.Repositories.Interfaces;
using comp1682_back_end.Repositories;

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
using AspNetCoreRateLimit;

namespace comp1682_back_end
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

      services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      // Add AutoMapper
      services.AddAutoMapper(typeof(Startup));

      // Add repositories and unit of work with dependency injection
      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Your API Name",
          Version = "v1",
          Description = "Your API description",
        });
      });

      // Configure rate limiting options
      services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
      services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
      services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
      // Add rate limiting middleware
      services.AddMemoryCache(); // Add the IMemoryCache service here
      services.AddInMemoryRateLimiting();

      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseIpRateLimiting();

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name V1");
      });

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
