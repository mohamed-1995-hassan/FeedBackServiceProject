using FeedBackServiceProject.Core.Interfaces.Repositories;
using FeedBackServiceProject.Core.Interfaces.Services;
using FeedBackServiceProject.Core.Services;
using FeedBackServiceProject.Infrastructure.Context;
using FeedBackServiceProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FeedBackServiceProject.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            if(services==null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddDbContext<FeedBackServiceDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();

            return services;
        }
    }
}
