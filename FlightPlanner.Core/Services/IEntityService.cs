using FlightPlanner.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        ServiceResult Create(T entity);
        ServiceResult Delete(T entity);
        ServiceResult Update(T entity);
        List<T> GetAll();
        T GetById(int id);
        IQueryable<T> Query();
    }
}
