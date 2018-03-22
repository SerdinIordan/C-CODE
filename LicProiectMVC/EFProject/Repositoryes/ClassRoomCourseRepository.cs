using BussinessLogic.Models;
using BussinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Repositoryes
{
   public class ClassRoomCourseRepository:IClassRoomCourseService
    {
        private SchoolDBContext schoolDBFirstContext =
            SchoolDBContext.Instance;

        public ClassRoomCourse Delete(ClassRoomCourse entity)
        {
            schoolDBFirstContext.ClassRoomCourse.Remove(entity);
            schoolDBFirstContext.SaveChanges();
            return entity;
        }

        public List<ClassRoomCourse> GetAll()
        {
            var classRoomCourses = schoolDBFirstContext.ClassRoomCourse.ToList();
            return remadeNullInformations(classRoomCourses);
        }

        public List<ClassRoomCourse> GetClassRoomCoursesByCourseID(int courseID)
        {
            var classRoomCourses = schoolDBFirstContext.ClassRoomCourse.Where(crc => crc.CourseId== courseID).ToList();
            return remadeNullInformations(classRoomCourses);
        }
        public List<ClassRoomCourse> GetClassRoomCoursesByClassRoomID(int classRoomID)
        {
            var classRoomCourses = schoolDBFirstContext.ClassRoomCourse.Where(crc => crc.ClassRoomId == classRoomID).ToList();
            return remadeNullInformations(classRoomCourses);
        }

        public ClassRoomCourse Save(ClassRoomCourse entity)
        {
            schoolDBFirstContext.ClassRoomCourse.Add(entity);
            schoolDBFirstContext.SaveChanges();
            return entity;
        }

        public ClassRoomCourse Update(ClassRoomCourse entity)
        {
            schoolDBFirstContext.ClassRoomCourse.Update(entity);
            schoolDBFirstContext.SaveChanges();
            return entity;
        }

        public List<ClassRoomCourse> remadeNullInformations(List<ClassRoomCourse> classRoomCourses)
        {
            foreach(var classRoomCourse in classRoomCourses){
                classRoomCourse.ClassRoom = schoolDBFirstContext.ClassRoom.Where(cr => cr.ClassRoomId == classRoomCourse.ClassRoomId).FirstOrDefault();
                classRoomCourse.Course = schoolDBFirstContext.Course.Where(c => c.CourseId == classRoomCourse.CourseId).FirstOrDefault();
            }
            return classRoomCourses;

        }


        public ClassRoomCourse GetByID(int id)
        {
            throw new NotImplementedException();
        }

        /*  public string getCourseMessageDisplayByLocalTime()
          {
              var dateNow = DateTime.Now;
              var course = schoolDBFirstContext.ClassRoomCourse.Where(
                  crc => crc.DayOfWeek.Equals(dateNow.DayOfWeek.ToString())
              && crc.StartHour <= dateNow.TimeOfDay &&
              crc.EndHour >= dateNow.TimeOfDay).FirstOrDefault();

              string stringCourseDisplay;
              if (course != null)
              {
                  CourseRepository courseRepository = new CourseRepository();
                  course.Course = courseRepository.GetCourseById(course.CourseId);
                  ClassRoomRepository classRoomRepository = new ClassRoomRepository();
                  course.ClassRoom = classRoomRepository.GetClassRoomById(course.ClassRoomId);



                  stringCourseDisplay = "CourseID:" + course.Course.CourseId +
                      "CourseClass:" + course.ClassRoom.ClassRoomName +
                      "CourseName:" + course.Course.Name +
                              //"TeacherCourse:" + course.Course. +
                              "BeginEndCourse:" + course.StartHour.Value.ToString(@"hh\:mm") +
                              "-" + course.EndHour.Value.ToString(@"hh\:mm");


                  return stringCourseDisplay;
              }
              return null;
          }
          */




    }
}
