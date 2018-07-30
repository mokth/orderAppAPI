namespace galaEatAPI.Services.Abstractions
{
    public class PushNotificationServiceOptions
    {
        public string Subject { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }
    }
}
