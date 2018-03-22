using LicentaProiect.Models;
using LicentaProiect.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicentaProiect.Repositoryes
{
    public class StudentCourseRepository
    {
        private SchoolDBContext schoolDBFirstContext =
           SchoolDBContext.Instance;
        public StudentCourse UpdateStudentCourse(StudentCourse studentCourse,ServerInformation serverInformation)
        {

            var studentCourseUpdate = GetStudentCourseByStudentIDCourseID(studentCourse.StudentId, studentCourse.CourseId);

            if (studentCourseUpdate != null)
            {
                if (studentCourseUpdate.Validate == null || (studentCourse.Validate.Value.Day!=DateTime.Now.Day))
                {
                    //daca nu a validat deloc sau nu a validat azi
                    studentCourse.Validate = DateTime.Now;
                    switch(serverInformation.classRoom.TypeOfStudy)
                    {
                        case "Course":studentCourse.AttendanceStudentCourse++; break;
                        case "Seminar": studentCourse.AttendanceStudentSeminar++; break;
                        case "Lab": studentCourse.AttendanceStudentLab++; break;
                    }
                    schoolDBFirstContext.StudentCourse.Update(studentCourseUpdate);
                    schoolDBFirstContext.SaveChanges();
                    return studentCourseUpdate;
                }
                return studentCourseUpdate;
            }

            return null;

        }
        public StudentCourse DeleteStudentCourse(StudentCourse studentCourse)
        {
            schoolDBFirstContext.StudentCourse.Remove(studentCourse);
            schoolDBFirstContext.SaveChanges();
            return studentCourse;
        }

        public StudentCourse GetStudentCourseByStudentIDCourseID(int studentID, int courseID)
        {
            var studentCourse = schoolDBFirstContext.StudentCourse.Where(sc => sc.CourseId == courseID && sc.StudentId == studentID).FirstOrDefault();
            return studentCourse;
        }

        public StudentCourse GetStudentCourseByCourseID(int courseID)
        {
            var studentCourse = schoolDBFirstContext.StudentCourse.Where(sc => sc.CourseId == courseID).FirstOrDefault();
            return studentCourse;
        }

        public StudentCourse GetStudentCourseByStudentID(int studentID)
        {
            var studentCourse = schoolDBFirstContext.StudentCourse.Where(sc => sc.StudentId == studentID).FirstOrDefault();
            return studentCourse;
        }



        public void displayStudentCourse(StudentCourse studentCourse)
        {
            Console.WriteLine("Student ID: {0}", studentCourse.StudentId);
            Console.WriteLine("Course ID: {0}", studentCourse.CourseId);
            Console.WriteLine("Minimum de prezente curs: {0}", studentCourse.AttendanceStudentCourse);
            Console.WriteLine("Minimum de prezente lab: {0}", studentCourse.AttendanceStudentLab);
            Console.WriteLine("Minimum de prezente seminar: {0}", studentCourse.AttendanceStudentSeminar);
        }


    }
}
