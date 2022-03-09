using AppCore.DataAccess.EntityFramework.Bases;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityFramework.Repositories.Bases {
    public abstract class ICurrencyRepository:RepositoryBase<Currency> {
        public ICurrencyRepository(DbContext db):base(db) {

        }
    }
}
