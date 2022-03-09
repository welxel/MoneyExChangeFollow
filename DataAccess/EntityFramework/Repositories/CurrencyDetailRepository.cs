using DataAccess.EntityFramework.Repositories.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityFramework.Repositories
{
    public class CurrencyDetailRepository:ICurrencyDetailRepository
    {
        public CurrencyDetailRepository(DbContext db) : base(db)
        {

        }
    }
}
