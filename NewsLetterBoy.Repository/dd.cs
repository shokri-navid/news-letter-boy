using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NewsLetterBoy.Repository
{
    public class DbContextFactory : IDesignTimeDbContextFactory<NewsLetterDbContext>
    {
        public NewsLetterDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NewsLetterDbContext>();
            optionsBuilder.UseSqlServer("Data Source=blog.db");

            return new (optionsBuilder.Options);
        }
    }
}