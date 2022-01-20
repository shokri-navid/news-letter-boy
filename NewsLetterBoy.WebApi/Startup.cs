using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NewsLetterBoy.Model.NewsLetter;
using NewsLetterBoy.Model.Subscription;
using NewsLetterBoy.Repository;
using NewsLetterBoy.Service;

namespace NewsLetterBoy.WebApi
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
            services.AddDbContext<NewsLetterDbContext>(builder => 
                builder.UseInMemoryDatabase("newsLetterBoy_Db"));
            
            services.AddScoped<INewsLetterService, NewsLetterService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ISubscriptionDomainService, SubscriptionDomainService>();
            
            services.AddScoped<INewsLetterRepository, NewsLetterRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "NewsLetterBoy.WebApi", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, NewsLetterDbContext context)
        {
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NewsLetterBoy.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlerMiddleware>();    
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
        }
    }
}