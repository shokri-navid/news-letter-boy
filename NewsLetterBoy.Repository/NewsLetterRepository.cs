using Microsoft.Extensions.Logging;
using NewsLetterBoy.Model.NewsLetter;

namespace NewsLetterBoy.Repository
{
    public class NewsLetterRepository : GenericRepository<NewsLetter> , INewsLetterRepository
    {
        public NewsLetterRepository(NewsLetterDbContext context, ILogger<NewsLetterRepository> logger) : base(context,
            logger)
        {
        }
    }
}