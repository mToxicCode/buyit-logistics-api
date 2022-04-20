using Microsoft.OpenApi.Models;

namespace ToxiCode.BuyIt.Logistics.Api.Infrustructure.Extensions
{
    public static class SwaggerExtension
    {
        /// <summary>
        /// Add swagger service to DI
        /// </summary>
        /// <param name="services">Services collection</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MailAPI",
                });
            });
        }
    }
}