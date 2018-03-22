using BussinessLogic.Models;
using BussinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Repositoryes
{
    public class ClassRoomRepository: IClassRoomService
    {
        private SchoolDBContext schoolDBFirstContext =
            SchoolDBContext.Instance;
        public ClassRoom Save(ClassRoom classRoom)
        {
            classRoom.ClassRoomCourse = null;
            schoolDBFirstContext.ClassRoom.Add(classRoom);
            schoolDBFirstContext.SaveChanges();
            return classRoom;
        }
        public ClassRoom Update(ClassRoom classRoom)
        {

            classRoom.ClassRoomCourse = null;
            schoolDBFirstContext.ClassRoom.Update(classRoom);
            schoolDBFirstContext.SaveChanges();
            return classRoom;
        }


        public ClassRoom Delete(ClassRoom classRoom)
        {
            schoolDBFirstContext.ClassRoom.Remove(classRoom);
            schoolDBFirstContext.SaveChanges();
            return classRoom;
        }

        public ClassRoom GetByID(int id)
        {

            var classRoom = schoolDBFirstContext.ClassRoom.Where(c => c.ClassRoomId == id).FirstOrDefault();
            return classRoom;
        }

        public List<ClassRoom> GetAll()
        {
            var listClassRoomes = schoolDBFirstContext.ClassRoom.ToList();
            return listClassRoomes;
        }
    }
}
