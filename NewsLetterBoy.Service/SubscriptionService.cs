using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NewsLetterBoy.Model.NewsLetter;
using NewsLetterBoy.Model.Subscription;

namespace NewsLetterBoy.Service
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly INewsLetterRepository _newsLetterRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriptionDomainService _subscriptionDomainService;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(
            INewsLetterRepository newsLetterRepository,
            ISubscriptionRepository subscriptionRepository,
            ISubscriptionDomainService subscriptionDomainService,
            ILogger<SubscriptionService> logger)
        {
            _newsLetterRepository = newsLetterRepository;
            _subscriptionRepository = subscriptionRepository;
            _subscriptionDomainService = subscriptionDomainService;
            _logger = logger;
        }
        public async Task SubscribeAsync(int userId, int newsLetterId, DateTime? expireDate)
        {
            var newsLetter = await _newsLetterRepository.GetByIdAsync(newsLetterId);
            if (newsLetter == null)
            {
                throw new ApplicationException("wrong newsletter Id!!!"); 
            }

            var subscription = new Subscription(newsLetterId, userId, _subscriptionDomainService, expireDate);
            await _subscriptionRepository.InsertAsync(subscription);
        }

        public async Task UnsubscribeAsync(int userId, int newsLetterId)
        {
            var subscription = await _subscriptionRepository.GetSubscriptionAsync(userId, newsLetterId);
            if (subscription == null)
            {
                throw new ApplicationException("Invalid subscription information provided!!!");
            }

            subscription.Unsubscribe();
            await _subscriptionRepository.UpdateAsync(subscription);

        }

        public async Task<bool> GetSubscriptionStatusAsync(int userId, int newsLetterId)
        {
             var subscription = await _subscriptionRepository.GetSubscriptionAsync(userId, newsLetterId);
             if (subscription == null)
                 return false;
             return true;
        }
    }
}