using LicentaProiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicentaProiect.Repositoryes
{
    public class ClassRoomRepository
    {
        private SchoolDBContext schoolDBFirstContext =
            SchoolDBContext.Instance;
        public ClassRoom SaveClassRoom(ClassRoom classRoom)
        {
            classRoom.ClassRoomCourse = null;
            schoolDBFirstContext.ClassRoom.Add(classRoom);
            schoolDBFirstContext.SaveChanges();
            return classRoom;
        }
        public ClassRoom UpdateCourse(ClassRoom classRoom)
        {

            classRoom.ClassRoomCourse = null;
            schoolDBFirstContext.ClassRoom.Update(classRoom);
            schoolDBFirstContext.SaveChanges();
            return classRoom;
        }


        public ClassRoom DeleteClassRoom(ClassRoom classRoom)
        {
            schoolDBFirstContext.ClassRoom.Remove(classRoom);
            schoolDBFirstContext.SaveChanges();
            return classRoom;
        }

        public ClassRoom GetClassRoomById(int id)
        {

            var classRoom = schoolDBFirstContext.ClassRoom.Where(c => c.ClassRoomId == id).FirstOrDefault();
            return classRoom;
        }
        public void displayCourse(ClassRoom classRoom)
        {
            Console.WriteLine("ClassRoomId: {0}", classRoom.ClassRoomId);
            Console.WriteLine("Class Room Name: {0}", classRoom.ClassRoomName);
            Console.WriteLine("Tip de studiu: {0}", classRoom.TypeOfStudy);

        }
    }
}
