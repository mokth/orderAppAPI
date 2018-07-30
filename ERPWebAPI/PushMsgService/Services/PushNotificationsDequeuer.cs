using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Lib.Net.Http.WebPush;
using galaEatAPI.Services.Abstractions;
using galaCoreAPI.Entities;

namespace galaEatAPI.Services
{
    internal class PushNotificationsDequeuer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPushNotificationsQueue _messagesQueue;
        private readonly IPushNotificationService _notificationService;
        private readonly CancellationTokenSource _stopTokenSource = new CancellationTokenSource();

        private Task _dequeueMessagesTask;

        public PushNotificationsDequeuer(IServiceProvider serviceProvider, IPushNotificationsQueue messagesQueue, IPushNotificationService notificationService)
        {
            _serviceProvider = serviceProvider;
            _messagesQueue = messagesQueue;
            _notificationService = notificationService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _dequeueMessagesTask = Task.Run(DequeueMessagesAsync);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _stopTokenSource.Cancel();

            return Task.WhenAny(_dequeueMessagesTask, Task.Delay(Timeout.Infinite, cancellationToken));
        }

        private async Task DequeueMessagesAsync()
        {
            while (!_stopTokenSource.IsCancellationRequested)
            {
                PushMessageEx pushmessage = await _messagesQueue.DequeueAsync(_stopTokenSource.Token);

                if (!_stopTokenSource.IsCancellationRequested)
                {
                    using (IServiceScope serviceScope = _serviceProvider.CreateScope())
                    {
                        IPushSubscriptionStore subscriptionStore = serviceScope.ServiceProvider.GetRequiredService<IPushSubscriptionStore>();

                        //await subscriptionStore.ForEachSubscriptionAsync((PushSubscription subscription) =>
                        //{
                        //    // Fire-and-forget 
                        //    _notificationService.SendNotificationAsync(subscription, message, _stopTokenSource.Token);
                        //}, _stopTokenSource.Token);

                        await subscriptionStore.ForEachSubscriptionAsyncEx((SWSubscription sub) =>
                        {
                            PushSubscription subscription = new PushSubscription();
                            subscription.Endpoint = sub.Endpoint;
                            subscription.SetKey(PushEncryptionKeyName.Auth, sub.Auth);
                            subscription.SetKey(PushEncryptionKeyName.P256DH, sub.P256DH);
                            // subscription.Keys
                            // Fire-and-forget 
                            _notificationService.SendNotificationAsync(subscription, pushmessage.pushmessage, _stopTokenSource.Token);
                        }, _stopTokenSource.Token);
                    }

                }
            }

        }
    }
}
