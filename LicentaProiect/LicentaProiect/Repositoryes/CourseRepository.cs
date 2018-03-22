using LicentaProiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicentaProiect.Repositoryes
{
    public class CourseRepository
    {

        private SchoolDBContext schoolDBFirstContext =
            SchoolDBContext.Instance;
        public Course SaveCourse(Course course)
        {
            course.StudentCourse = null;
            schoolDBFirstContext.Course.Add(course);
            schoolDBFirstContext.SaveChanges();
            return course;
        }
        public Course UpdateCourse(Course course)
        {

            course.StudentCourse = null;
            schoolDBFirstContext.Course.Update(course);
            schoolDBFirstContext.SaveChanges();
            return course;
        }


        public Course RemoveCourse(Course course)
        {

            schoolDBFirstContext.Course.Remove(course);
            schoolDBFirstContext.SaveChanges();
            return course;
        }

        public Course GetCourseById(int id)
        {

            var course = schoolDBFirstContext.Course.Where(c => c.CourseId == id).FirstOrDefault();
            return course;
        }
        public void displayCourse(Course course)
        {
            Console.WriteLine("Course ID: {0}", course.CourseId);
            Console.WriteLine("Course Name: {0}", course.Name);
            Console.WriteLine("Miminimum de prezente cours: {0}", course.MinimumAttendanceCourse);
            Console.WriteLine("Miminimum de prezente lab: {0}", course.MinimumAttendanceLab);
            Console.WriteLine("Miminimum de prezente seminar: {0}", course.MinimumAttendanceSeminar);
        }
    }
}
