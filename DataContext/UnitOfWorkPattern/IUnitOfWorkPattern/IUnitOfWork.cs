using System;
using System.Threading.Tasks;
using DbAccess.Data;
using static DataContext.Repository.IRepository.IGenericRepository;

namespace DataContext.UnitOfWorkPattern.IUnitOfWorkPattern
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Immo> ImmoRepository { get; }
        Task Save();
    }
}