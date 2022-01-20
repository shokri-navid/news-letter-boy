using System.Threading.Tasks;

namespace NewsLetterBoy.Model.Subscription
{
    public interface ISubscriptionRepository: IGenericRepository<Subscription>
    {
        Task<Subscription> GetSubscriptionAsync(int userId, int newsLetterId);
    }
}