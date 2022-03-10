using AppCore.Business.Models.Results;
using AppCore.Records.Bases;
using System;
using System.Linq;

namespace AppCore.Business.Services.Bases
{
    public interface IService<TModel> : IDisposable where TModel : RecordBase, new()
    {
        Result<IQueryable<TModel>> GetQuery();
    }
}
