
using System;
using System.Threading.Tasks;
using VerivoxTask.Domain.Entities;

namespace VerivoxTask.Infrastructure.Persistence.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> ProductRepository { get;}
        Task<bool> Complete();
    }
}
