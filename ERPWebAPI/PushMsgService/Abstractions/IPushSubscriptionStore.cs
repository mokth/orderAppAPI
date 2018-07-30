using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using galaCoreAPI.Entities;
using Lib.Net.Http.WebPush;

namespace galaEatAPI.Services.Abstractions
{
    public interface IPushSubscriptionStore
    {
        //Task StoreSubscriptionAsync(PushSubscription subscription);
        Task StoreSubscriptionAsync(SubscriptionPayLoad payload);

        List<SWSubscription> GetSubScriptions(int memberid);

        Task DiscardSubscriptionAsync(string endpoint);

        Task ForEachSubscriptionAsync(Action<PushSubscription> action);

        Task ForEachSubscriptionAsync(Action<PushSubscription> action, CancellationToken cancellationToken);

        Task ForEachSubscriptionAsyncEx(Action<SWSubscription> action, CancellationToken cancellationToken);
    }
}
