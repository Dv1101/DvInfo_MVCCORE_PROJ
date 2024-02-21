using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DvInfoWeb.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category --perform crud op on it
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null); //Passing linq as a para argument 
     


        void Add(T entity);
        //void Update(T entity);
        void Remove(T entity);
        
        void RemoveRange(IEnumerable<T> entity);


    }
}
