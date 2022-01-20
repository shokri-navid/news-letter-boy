using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsLetterBoy.Model;
using NewsLetterBoy.Model.Subscription;

namespace NewsLetterBoy.Repository
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(NewsLetterDbContext context, ILogger<SubscriptionRepository> logger) : base(context, logger)
        {
        }

        public async Task<Subscription> GetSubscriptionAsync(int userId, int newsLetterId)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(x => 
                x.NewsLetterId == newsLetterId 
                && x.UserId == userId 
                && !x.IsDeleted
                && (x.ExpirationDateTime == null || x.ExpirationDateTime.Value > DateTime.Now));
            
        }
    }
}