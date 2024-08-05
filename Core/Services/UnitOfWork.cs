using Domain.Entities;
using Core.Interfaces;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TwitterDbContext _context;

        public UnitOfWork(TwitterDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
