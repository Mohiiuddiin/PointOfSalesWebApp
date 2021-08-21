using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSalesWebApp.Models;

namespace PointOfSalesWebApp.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {

        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);

    }
}
