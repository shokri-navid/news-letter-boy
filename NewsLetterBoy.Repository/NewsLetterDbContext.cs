using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NewsLetterBoy.Model;
using NewsLetterBoy.Model.NewsLetter;
using NewsLetterBoy.Model.Subscription;

namespace NewsLetterBoy.Repository
{
    public class NewsLetterDbContext : DbContext
    {
        public NewsLetterDbContext()
        {
        }

        public NewsLetterDbContext(DbContextOptions<NewsLetterDbContext> options): base(options)
        {
            
        }

        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsLetter>()
                .HasData(new List<NewsLetter>{
                    new NewsLetter("Tech news", "About information technologies an data science"){Id = 1},
                    new NewsLetter("Science news", "About Science and academic research"){Id = 2},
                    new NewsLetter("Financial news", "About cryptocurrencies and economy"){Id = 3},
                    new NewsLetter("Sport news", "About Sport and fitness around the world"){Id = 4},
                    new NewsLetter("Technology news", "About industrial and engineering"){Id = 5},
                    new NewsLetter("Political news", "About politics "){Id = 6}
                });
            
        }
        
        
        
      
    }
}