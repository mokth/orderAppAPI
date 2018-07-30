using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lib.Net.Http.WebPush;
using System;
using System.Linq;
using galaEatAPI.Services.Abstractions;
using galaCoreAPI.Model;
using galaCoreAPI.Entities;
using galaCoreAPI.Authentication;
using galaEatAPI.Services.Sqlite;
using Microsoft.AspNetCore.Http;

namespace galaCoreAPI.Controllers.Controllers
{
    [Route("push-notifications-api")]
    public class PushNotificationsApiController : Controller
    {
        private readonly IPushSubscriptionStore _subscriptionStore;
        private readonly IPushNotificationService _notificationService;
        private readonly IPushNotificationsQueue _pushNotificationsQueue;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IJwtFactory _jwtFactory;
        private readonly DataDbContect _context;
        private readonly IHttpContextAccessor _acceessor;


        public PushNotificationsApiController(
              IPushSubscriptionStore subscriptionStore, IPushNotificationService notificationService, IPushNotificationService pushNotificationService,
              IPushNotificationsQueue pushNotificationsQueue, IJwtFactory jwtFactory, DataDbContect context, IHttpContextAccessor acceessor)
        {
            _subscriptionStore = subscriptionStore;
            _notificationService = notificationService;
            _pushNotificationsQueue = pushNotificationsQueue;
            _pushNotificationService = pushNotificationService;
            _jwtFactory = jwtFactory;
            _context = context;
            _acceessor = acceessor;
        }

        // GET push-notifications-api/public-key
        [HttpGet("public-key")]
        public ContentResult GetPublicKey()
        {
            return Content(_notificationService.PublicKey, "text/plain");
        }

        //// POST push-notifications-api/subscriptions
        //[HttpPost("subscriptions")]
        //public async Task<IActionResult> StoreSubscription([FromBody]PushSubscription subscription)
        //{
        //    Console.WriteLine(subscription);

        //    await _subscriptionStore.StoreSubscriptionAsync(subscription);

        //    return NoContent();
        //}

        //// POST push-notifications-api/subscriptions
        //[HttpPost("subscriptions")]
        //public async Task<IActionResult> StoreSubscription([FromBody] PushSubscription subscription)
        //{
        //    UserInfo user = _jwtFactory.DecodedRequestAuth(Request);
        //    Console.WriteLine(subscription);
        //    var profile = _context.Merchants
        //              .Where(x => x.country == user.country &&
        //                          x.state == user.state &&
        //                          x.area == user.area &&
        //                          x.city == user.city &&
        //                          x.branchCode == user.branchCode &&
        //                          x.companyCode == user.companyCode &&
        //                          x.location == user.location)
        //              .FirstOrDefault();
        //   //string clientip= _acceessor.HttpContext.Connection.RemoteIpAddress.ToString()
        //   // Console.WriteLine(Request.Headers["User-Agent"]);
        //   SubscriptionPayLoad payload = new SubscriptionPayLoad();
        //    payload.subscription = subscription;
        //    payload.memberId = profile.id;
        //    payload.cleintIP = _acceessor.HttpContext.Connection.RemoteIpAddress.ToString();
           
        //    if (profile != null)
        //    {
        //        //userAgent = Request.Headers["User-Agent"]; 
        //        try
        //        {
        //            await _subscriptionStore.StoreSubscriptionAsync(payload);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //        }
        //    }

        //    return NoContent();
        //}

        

        // DELETE push-notifications-api/subscriptions?endpoint={endpoint}
        [HttpDelete("subscriptions")]
        public async Task<IActionResult> DiscardSubscription(string endpoint)
        {
            await _subscriptionStore.DiscardSubscriptionAsync(endpoint);

            return NoContent();
        }

        // POST push-notifications-api/notifications
        [HttpPost("notifications")]
        public IActionResult SendNotification([FromBody]PushMessageViewModel message)
        {
            PushMessageEx msg = new PushMessageEx();
            msg.pushmessage = new PushMessage(message.Notification)
            {
                Topic = message.Topic,
                Urgency = message.Urgency
            };
            msg.MemberID = 1;// test

            _pushNotificationsQueue.Enqueue(msg);

            return NoContent();
        }
    }
}
