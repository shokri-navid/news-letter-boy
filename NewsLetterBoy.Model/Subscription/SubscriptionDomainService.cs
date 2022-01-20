namespace NewsLetterBoy.Model.Subscription
{
    public class SubscriptionDomainService : ISubscriptionDomainService
    {
        private readonly ISubscriptionRepository _repository;

        public SubscriptionDomainService(ISubscriptionRepository repository)
        {
            _repository = repository;
        }
        public bool IsUserRegisteredBefore(int userId, int newsLetterId)
        {
            var value = _repository.GetSubscriptionAsync(userId, newsLetterId).GetAwaiter().GetResult();
            if (value == null)
            {
                return false;
            }

            return true;
        }
    }
}