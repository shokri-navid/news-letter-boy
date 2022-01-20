namespace NewsLetterBoy.Model.Subscription
{
    public interface ISubscriptionDomainService
    {
        bool IsUserRegisteredBefore(int userId, int newsLetterId);
    }
}