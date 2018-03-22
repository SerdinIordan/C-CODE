using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public interface IEntityService<T>
    {
        T GetByID(int id);
        T Save(T entity);
        T Update(T entity);
        T Delete(T entity);
        List<T> GetAll();
    }
}
