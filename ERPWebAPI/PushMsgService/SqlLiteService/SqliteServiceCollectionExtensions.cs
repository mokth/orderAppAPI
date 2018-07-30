using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using galaEatAPI.Services.Abstractions;
using System;

namespace galaEatAPI.Services.Sqlite
{
    public static class SqliteServiceCollectionExtensions
    {
        private const string SQLITE_CONNECTION_STRING_NAME = "PushSubscriptionSqliteDatabase";

        public static IServiceCollection AddSqlitePushSubscriptionStore(this IServiceCollection services,IConfiguration configuration)
        {
            var sqlConnectionString = configuration.GetConnectionString("DataAccessMsSqlServerProvider");
            Console.WriteLine(sqlConnectionString);
            services.AddDbContext<PushSubscriptionContext>(options =>
              options.UseSqlServer(sqlConnectionString)
            );

            services.AddTransient<IPushSubscriptionStore, SqlitePushSubscriptionStore>();

            return services;
        }
    }
}
