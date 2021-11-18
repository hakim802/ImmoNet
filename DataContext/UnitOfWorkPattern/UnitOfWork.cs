using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContext.Repository;
using DataContext.Repository.IRepository;
using DataContext.UnitOfWorkPattern.IUnitOfWorkPattern;
using DbAccess.Data;
using static DataContext.Repository.IRepository.IGenericRepository;

namespace DataContext.UnitOfWorkPattern
{
    public class UnitOfWork : IUnitOfWork
    {
        // First we have to introduce a local 'DbContext' using dependency injection
        // from our 'Startup.cs'-file into the constructor here. We will also need 
        // a local object from the two 'IGenericRepository'-classes.
        private readonly ImmoDbContext _context;

        private IGenericRepository<Immo> _immos;

        private IGenericRepository<Contact> _contacts;

        private IGenericRepository<Image> _images;

        public UnitOfWork(ImmoDbContext context)
        {
            _context = context;
        }

        //******************************************************************************
        // Here we will provide a body to the function-headers that were implemented 
        // by inheriting from the interface.

        public IGenericRepository.IGenericRepository<Immo> ImmoRepository =>
                                                    _immos ??= new GenericRepository<Immo>(_context);

        public IGenericRepository.IGenericRepository<Contact> ContactRepository =>
                                                    _contacts ??= new GenericRepository<Contact>(_context);

        public IGenericRepository.IGenericRepository<Image> ImageRepository =>
                                                    _images ??= new GenericRepository<Image>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

