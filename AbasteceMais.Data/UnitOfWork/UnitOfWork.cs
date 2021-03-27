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

        private IGenericRepository<City> _citynRepository;

        private IGenericRepository<District> _districtRepository;

        private IGenericRepository<State> _stateRepository;

        private bool disposed = false;

        public UnitOfWork(DatabaseContextAbasteceMais contextAbasteceMais)
        {
            _contextAbasteceMais = contextAbasteceMais;
        }

        public IGenericRepository<Registration> RegistrationRepository
        {
            get { return _registrationRepository ?? (_registrationRepository = new GenericRepository<Registration>(_contextAbasteceMais)); }
        }

        public IGenericRepository<City> CityRepository
        {
            get { return _citynRepository ?? (_citynRepository = new GenericRepository<City>(_contextAbasteceMais)); }
        }

        public IGenericRepository<District> DistrictRepository
        {
            get { return _districtRepository ?? (_districtRepository = new GenericRepository<District>(_contextAbasteceMais)); }
        }

        public IGenericRepository<State> StateRepository
        {
            get { return _stateRepository ?? (_stateRepository = new GenericRepository<State>(_contextAbasteceMais)); }
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
