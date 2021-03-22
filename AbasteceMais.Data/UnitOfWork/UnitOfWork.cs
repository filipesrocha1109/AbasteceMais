using AbasteceMais.Data.Context;
using AbasteceMais.Data.Repositories;
using AbasteceMais.Domain.Entities;
using AbasteceMais.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbasteceMais.Domain.Interfaces.UnitOfWork;

namespace AbasteceMais.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly DatabaseContextAbasteceMais _contextAbasteceMais;

        private IGenericRepository<Registration> _registrationRepository;

        private bool disposed = false;

        public UnitOfWork(DatabaseContextAbasteceMais contextAbasteceMais)
        {
            _contextAbasteceMais = contextAbasteceMais;
        }

        public IGenericRepository<Registration> RegistrationRepository
        {
            get { return _registrationRepository ?? (_registrationRepository = new GenericRepository<Registration>(_contextAbasteceMais)); }
        }


        public void PersistChanges()
        {
            _contextAbasteceMais.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _contextAbasteceMais.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}
