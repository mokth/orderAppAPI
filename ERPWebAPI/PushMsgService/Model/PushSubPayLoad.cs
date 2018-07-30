using Lib.Net.Http.WebPush;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace galaCoreAPI.Entities
{
    public class SubscriptionPayLoad
    {
        public PushSubscription subscription { get; set; }
        public int memberId { get; set; }
        public string cleintIP { get; set; }
    }

    [Table("Subscriptions")]
    public class SWSubscription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string Endpoint { get; set; }
        public string P256DH { get; set; }
        public string Auth { get; set; }
        public int? MemberID { get; set; }
        public DateTime? created { get; set; }
        public string clientIP { get; set; }

    }

    public class PushMessageEx
    {
        public PushMessage pushmessage { get; set; }
        public int? MemberID { get; set; }
    }
}
