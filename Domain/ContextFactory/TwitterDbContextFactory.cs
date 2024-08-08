using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Domain.ContextFactory
{
    public class TwitterDbContextFactory : IDesignTimeDbContextFactory<TwitterDbContext>
    {
        public TwitterDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TwitterDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-236OCC7\\SQLEXPRESS;Database=Twitterlydb;TrustServerCertificate=True;Trusted_Connection=True;Connection Timeout=120;"
);

            return new TwitterDbContext(optionsBuilder.Options);
        }
    }

}
