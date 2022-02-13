
using System.Threading.Tasks;
using VerivoxTask.Domain.Entities;

namespace VerivoxTask.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Repository<Product> productRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IRepository<Product> ProductRepository => productRepository ?? new Repository<Product>(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
