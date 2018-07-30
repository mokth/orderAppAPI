using System.Threading;
using System.Threading.Tasks;
using galaCoreAPI.Entities;
using Lib.Net.Http.WebPush;

namespace galaEatAPI.Services.Abstractions
{
    public interface IPushNotificationsQueue
    {
        void Enqueue(PushMessageEx message);

        Task<PushMessageEx> DequeueAsync(CancellationToken cancellationToken);
    }
}
