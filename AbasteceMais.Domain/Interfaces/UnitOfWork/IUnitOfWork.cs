using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbasteceMais.Domain.Entities;
using AbasteceMais.Domain.Interfaces.Repositories;

namespace AbasteceMais.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Registration> RegistrationRepository { get; }

        IGenericRepository<State> StateRepository { get;  }

        IGenericRepository<City> CityRepository { get; }

        IGenericRepository<District> DistrictRepository { get; }

        IGenericRepository<GasStation> GasStationRepository { get; }

        IGenericRepository<Assessment> AssessmentRepository { get; }

        IGenericRepository<Comment> CommentRepository { get; }

        IGenericRepository<UpdatePricesGasStation> UpdatePricesGasStationRepository { get; }

        void PersistChanges();

        void Dispose();
    }
}
