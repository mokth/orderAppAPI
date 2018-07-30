using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using galaEatAPI.Services.Abstractions;
using galaEatAPI.Services.Sqlite;
using galaEatAPI.Services.PushService;

namespace galaEatAPI.Services
{
    public static class ServiceCollectionExtensions
    {
        private const string PUSH_NOTIFICATION_SERVICE_CONFIGURATION_SECTION = "PushNotificationService";

        public static IServiceCollection AddPushSubscriptionStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlitePushSubscriptionStore(configuration);
            
            return services;
        }

        public static IServiceCollection AddPushNotificationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<PushNotificationServiceOptions>(configuration.GetSection(PUSH_NOTIFICATION_SERVICE_CONFIGURATION_SECTION));

            services.AddPushServicePushNotificationService();

            return services;
        }

        public static IServiceCollection AddPushNotificationsQueue(this IServiceCollection services)
        {
            services.AddSingleton<IPushNotificationsQueue, PushNotificationsQueue>();
            services.AddSingleton<IHostedService, PushNotificationsDequeuer>();

            return services;
        }
    }
}
