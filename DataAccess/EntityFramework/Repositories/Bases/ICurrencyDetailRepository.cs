using AppCore.DataAccess.EntityFramework.Bases;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityFramework.Repositories.Bases
{
    public abstract class ICurrencyDetailRepository:RepositoryBase<CurrencyDetail>
    {
        public ICurrencyDetailRepository(DbContext db):base(db)
        {

        }
    }
}
