using System;
using Newtonsoft.Json;

namespace NewsLetterBoy.Model.Subscription
{
    public class Subscription : BaseModel
    {
        private readonly ISubscriptionDomainService _domainService;
        [JsonConstructor]
        protected Subscription()
        {
        }

        public Subscription(int newsLetterId, int userId,  ISubscriptionDomainService domainService, DateTime? expirationDateTime = null)
        {
            _domainService = domainService;
            if (domainService.IsUserRegisteredBefore(userId, newsLetterId))
            {
                throw new DomainException("Repetitive newsletter subscription!!!", 400);
            }

            if (expirationDateTime != null && expirationDateTime.Value < DateTime.Now)
            {
                throw new DomainException("Expiration date should be in future!!!", 400);
            }

            NewsLetterId = newsLetterId;
            UserId = userId;
            ExpirationDateTime = expirationDateTime;
            SubscriptionDateTime = DateTime.Now;
        }

        public void Unsubscribe()
        {
            SetAsDeleted();
        }

        public int NewsLetterId { get; private set; }
        public int UserId { get; private set; }
        public DateTime SubscriptionDateTime { get; private set; }
        public DateTime? ExpirationDateTime { get; private set; }

    }
}