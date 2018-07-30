using galaCoreAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebPush = Lib.Net.Http.WebPush;

namespace galaEatAPI.Services.Sqlite
{
    public class PushSubscriptionContext : DbContext
    {
        public class PushSubscription : WebPush.PushSubscription
        {
            public int MemberID { get; set; }
            public string clientIP { get; set; }


            public string P256DH
            {
                get { return GetKey(WebPush.PushEncryptionKeyName.P256DH); }

                set { SetKey(WebPush.PushEncryptionKeyName.P256DH, value); }
            }

            public string Auth
            {
                get { return GetKey(WebPush.PushEncryptionKeyName.Auth); }

                set { SetKey(WebPush.PushEncryptionKeyName.Auth, value); }
            }

            public PushSubscription()
            { }

            public PushSubscription(WebPush.PushSubscription subscription,int id,string clientip)
            {
                Endpoint = subscription.Endpoint;
                Keys = subscription.Keys;
                MemberID = id;
                clientIP = clientip;
            }
        }

        public DbSet<PushSubscription> Subscriptions { get; set; }
       

        public PushSubscriptionContext(DbContextOptions<PushSubscriptionContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<PushSubscription> pushSubscriptionEntityTypeBuilder = modelBuilder.Entity<PushSubscription>();
            pushSubscriptionEntityTypeBuilder.HasKey(e => e.Endpoint);
            pushSubscriptionEntityTypeBuilder.Ignore(p => p.Keys);
           
        }
    }
}
