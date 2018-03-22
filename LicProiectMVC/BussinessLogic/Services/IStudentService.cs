using BussinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public interface IStudentService:IEntityService<Student>
    {
        Student GetStudentByCOD(string cod);
        Student GetStudentByUserID(int id);
    }
}
