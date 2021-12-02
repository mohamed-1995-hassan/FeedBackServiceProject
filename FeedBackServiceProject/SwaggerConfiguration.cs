using Microsoft.OpenApi.Models;

namespace FeedBackServiceProject.Api
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection configreSwager(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
           // string serviceDescription = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "ServiceDescription.md"));
            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FeedbackService", Version = "v1",/* Description = serviceDescription*/ });
                //string xmlFile = $"{typeof(SwaggerConfiguration).Assembly.GetName().Name}.xml";
                //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
                //c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["controller"]}_{e.ActionDescriptor.RouteValues["action"]}");
                }
            );
            return services;
        }
        public static IApplicationBuilder configreSwager(this IApplicationBuilder app)
        {
            if(app==null)
                throw new ArgumentNullException(nameof(app));
            //string serviceDescription = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "ServiceDescription.md"));
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","DocumentService.Api v1"));
            return app;
        }
    }
}
