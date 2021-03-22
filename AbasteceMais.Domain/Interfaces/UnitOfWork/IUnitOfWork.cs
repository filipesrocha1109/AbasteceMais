﻿using System;
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

        void PersistChanges();

        void Dispose();
    }
}