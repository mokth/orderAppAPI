using Microsoft.AspNetCore.Builder;
using galaEatAPI.Services.Sqlite;

namespace galaEatAPI.Services
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePushSubscriptionStore(this IApplicationBuilder app)
        {
            app.UseSqlitePushSubscriptionStore();

            return app;
        }
    }
}
