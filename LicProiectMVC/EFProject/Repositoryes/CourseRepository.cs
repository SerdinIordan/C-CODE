using BussinessLogic.Models;
using BussinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Repositoryes
{
    public class CourseRepository:ICourseService
    {
        private SchoolDBContext schoolDBFirstContext =
            SchoolDBContext.Instance;
        public Course Save(Course course)
        {
            course.StudentCourse = null;
            schoolDBFirstContext.Course.Add(course);
            schoolDBFirstContext.SaveChanges();
            return course;
        }
        public Course Update(Course course)
        {

            course.StudentCourse = null;
            schoolDBFirstContext.Course.Update(course);
            schoolDBFirstContext.SaveChanges();
            return course;
        }


        public Course Delete(Course course)
        {

            schoolDBFirstContext.Course.Remove(course);
            schoolDBFirstContext.SaveChanges();
            return course;
        }

        public Course GetByID(int id)
        {

            var course = schoolDBFirstContext.Course.Where(c => c.CourseId == id).FirstOrDefault();
            remadeNullInformations(course);
            return course;
        }

        public List<Course> GetAll()
        {
            var listCourses=schoolDBFirstContext.Course.ToList();
            foreach(var course in listCourses)
            {
                remadeNullInformations(course);
            }
            return listCourses;
        }
        public Course remadeNullInformations(Course course)
        {
            course.StudentCourse = schoolDBFirstContext.StudentCourse.Where(sc => sc.CourseId == course.CourseId).ToList();
            course.Teacher = schoolDBFirstContext.Teacher.Where(t => t.TeacherId == course.TeacherId).FirstOrDefault();
            return course;
        }
    }
}
