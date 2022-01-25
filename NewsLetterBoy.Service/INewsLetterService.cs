using System.Collections.Generic;
using System.Threading.Tasks;
using NewsLetterBoy.Model.NewsLetter;

namespace NewsLetterBoy.Service
{
    public interface INewsLetterService
    {
        Task<int> CreateAsync(string title, string description);
        Task UpdateAsync (int id, string title, string description);
        Task<NewsLetter> GetByIdAsync(int id);
        Task<IEnumerable<NewsLetter>> GetAllAsync(string search); 
    }
}