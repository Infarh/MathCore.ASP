using System.Text.Json;

using MathCore.ASP.Filters.Results;
using MathCore.ASP.Middleware;
using MathCore.ASP.WEB.Tests.Models;

using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

using Newtonsoft.Json;

namespace MathCore.ASP.WEB.Tests
{
    public record Startup(IConfiguration Configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services
               .AddControllersWithViews(opt => opt.Filters.Add(new ProcessingTimeHeaderAttribute("P-Processing")))
               .AddNewtonsoftJson(opt => opt.SerializerSettings.Formatting = Formatting.Indented)
               .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.WriteIndented = true;
                    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; 
                })
               .AddRazorRuntimeCompilation();

            //services.AddOData();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            

            app.UseAuthentication();

            app.UseRouting();

            app.UseMiddleware<ProcessingTimeHeaderMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.EnableDependencyInjection();
                //endpoints.Expand().Select().OrderBy().MaxTop(null).Count().Filter();
                //endpoints.MapODataRoute("OData", "odata", CreateODataModel());
                
                
                endpoints.MapDefaultControllerRoute();
            });
        }

        private IEdmModel CreateODataModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Student>("students").EntityType
               .Select().OrderBy().Expand().Count().Filter();

            return builder.GetEdmModel();
        }
    }
}
