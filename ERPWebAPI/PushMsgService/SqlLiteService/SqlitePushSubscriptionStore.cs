using System;
using System.Data;
using System.Data.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lib.Net.Http.WebPush;
using galaEatAPI.Services.Abstractions;
using System.Collections.Generic;
using galaCoreAPI.Entities;
using System.Linq;

namespace galaEatAPI.Services.Sqlite
{
    internal class SqlitePushSubscriptionStore : IPushSubscriptionStore
    {
        private readonly PushSubscriptionContext _context;
        private readonly DataDbContect _dbcontext;

        public SqlitePushSubscriptionStore(PushSubscriptionContext context, DataDbContect dbcontext)
        {
            _context = context;
            _dbcontext = dbcontext;
        }

        public List<SWSubscription> GetSubScriptions(int memberid)
        {
           return _dbcontext.SWSubscriptions.Where(x => x.MemberID == memberid).ToList();
        }
        private string getBrowserType(string endpoint)
        {
            string type = "google";
            if (endpoint.Contains("google"))
            {
                type = "google";
            }
            else if (endpoint.Contains("mozilla"))
            {
                type = "mozilla";
            }

            return type;
        }

        public Task StoreSubscriptionAsync(SubscriptionPayLoad payload)
        {
           // var list =  _context.Subscriptions.ToListAsync();
            var list = _dbcontext.SWSubscriptions.Where(x => x.MemberID==payload.memberId );
            var found = list.Where(x => x.Endpoint.ToString().ToLower() == payload.subscription.Endpoint.ToString().ToLower()).FirstOrDefault();
            var found2 = _context.Subscriptions.Where(x => x.Endpoint.ToString().ToLower() == payload.subscription.Endpoint.ToString().ToLower()).FirstOrDefault();
            ;
            if (found == null && found2 == null)
            {
                Console.WriteLine("found =null && found2=null");
                _context.Subscriptions.Add(new PushSubscriptionContext.PushSubscription(payload.subscription, payload.memberId, payload.cleintIP));
                string brwtype = getBrowserType(payload.subscription.Endpoint);
                foreach (var item in list)
                {
                    if (getBrowserType(item.Endpoint) == brwtype)
                    {
                        _dbcontext.Remove(item);
                    }
                }
                _dbcontext.SaveChangesAsync();
            }
            else if (found2 != null)
            {
                found2.clientIP = payload.cleintIP;
                found2.MemberID = payload.memberId;
               
            }

            return _context.SaveChangesAsync();
        }

        public async Task DiscardSubscriptionAsync(string endpoint)
        {
            PushSubscriptionContext.PushSubscription subscription = await _context.Subscriptions.FindAsync(endpoint);

            _context.Subscriptions.Remove(subscription);

            await _context.SaveChangesAsync();
        }

        public Task ForEachSubscriptionAsync(Action<PushSubscription> action)
        {
            return ForEachSubscriptionAsync(action, CancellationToken.None);
        }

        public Task ForEachSubscriptionAsync(Action<PushSubscription> action, CancellationToken cancellationToken)
        {
            return _context.Subscriptions.AsNoTracking().ForEachAsync(action, cancellationToken);
        }

        public Task ForEachSubscriptionAsyncEx(Action<SWSubscription> action, CancellationToken cancellationToken)
        {
            return _dbcontext.SWSubscriptions.AsNoTracking().ForEachAsync(action, cancellationToken);
        }
    }
}
