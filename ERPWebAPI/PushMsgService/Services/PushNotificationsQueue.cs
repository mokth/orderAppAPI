using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Lib.Net.Http.WebPush;
using galaEatAPI.Services.Abstractions;
using galaCoreAPI.Entities;

namespace galaEatAPI.Services
{
    internal class PushNotificationsQueue : IPushNotificationsQueue
    {
        private readonly ConcurrentQueue<PushMessageEx> _messages = new ConcurrentQueue<PushMessageEx>();
        private readonly SemaphoreSlim _messageEnqueuedSignal = new SemaphoreSlim(0);

        public void Enqueue(PushMessageEx message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            _messages.Enqueue(message);

            _messageEnqueuedSignal.Release();
        }

        public async Task<PushMessageEx> DequeueAsync(CancellationToken cancellationToken)
        {
            await _messageEnqueuedSignal.WaitAsync(cancellationToken);

            _messages.TryDequeue(out PushMessageEx message);

            return message;
        }
    }
}
