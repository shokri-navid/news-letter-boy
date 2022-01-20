using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NewsLetterBoy.Model;
using NewsLetterBoy.Model.NewsLetter;

namespace NewsLetterBoy.Service
{
    public class NewsLetterService: INewsLetterService
    {
        private readonly INewsLetterRepository _newsLetterRepository;
        private readonly ILogger<NewsLetterService> _logger;

        public NewsLetterService(INewsLetterRepository newsLetterRepository, ILogger<NewsLetterService> logger)
        {
            _newsLetterRepository = newsLetterRepository;
            _logger = logger;
        }
        public async Task<int> CreateAsync(string title, string description)
        {
            var newsLetter = new NewsLetter(title, description);
            return await _newsLetterRepository.InsertAsync(newsLetter);
        }

        public async Task UpdateAsync(int id, string title, string description)
        {
            var newsLetter = await _newsLetterRepository.GetByIdAsync(id);
            if (newsLetter == null)
            {
                throw new ApplicationException("Invalid newsletter id!!!"); 
            }
            newsLetter.Modify(title,description);
            await _newsLetterRepository.UpdateAsync(newsLetter);
        }

        public async Task<NewsLetter> GetByIdAsync(int id)
        {
            return await _newsLetterRepository.GetByIdAsync(id); 
        }

        public async Task<IEnumerable<NewsLetter>> GetAllAsync(string search)
        {
            Expression<Func<NewsLetter, bool>> expr = letter => true;
            if (!string.IsNullOrWhiteSpace(search))
            {
                expr = letter =>  letter.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                       || letter.Description.Contains(search, StringComparison.InvariantCultureIgnoreCase);
            }

            return await _newsLetterRepository.GetAllAsync(expr); 
        }
    }
}