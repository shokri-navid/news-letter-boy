using System;
using System.Threading.Tasks;

namespace NewsLetterBoy.Service
{
  public interface ISubscriptionService
  {
    Task SubscribeAsync(int userId, int newsLetterId, DateTime? expireDate);
    Task UnsubscribeAsync(int id, int newsLetterId);
    Task<bool> GetSubscriptionStatusAsync(int userId, int newsLetterId);
  }
}