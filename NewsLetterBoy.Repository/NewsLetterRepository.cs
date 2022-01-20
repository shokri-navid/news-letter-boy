using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewsLetterBoy.Model;
using NewsLetterBoy.Model.NewsLetter;

namespace NewsLetterBoy.Repository
{
    public class NewsLetterRepository : GenericRepository<NewsLetter> , INewsLetterRepository
    {
        public NewsLetterRepository(NewsLetterDbContext context, ILogger<NewsLetterRepository> logger) : base(context, logger)
        {
        }

        public Task kirtoot()
        { 
            var t =_context.NewsLetters.Count();
            return Task.CompletedTask;
        }
    }
}