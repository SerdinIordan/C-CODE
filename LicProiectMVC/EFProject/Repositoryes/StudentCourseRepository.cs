using BussinessLogic.Models;
using BussinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Repositoryes
{
    public class StudentCourseRepository: IStudentCourseService
    {
        private SchoolDBContext schoolDBFirstContext =
            SchoolDBContext.Instance;
        public StudentCourse RequestEnrollment(StudentCourse studentCourse)
        {
            schoolDBFirstContext.StudentCourse.Add(studentCourse);
            schoolDBFirstContext.SaveChanges();
            return studentCourse;
        }


       /* public StudentCourse UpdateStudentCourse(StudentCourse studentCourse)
        {

            var studentCourseUpdate = GetStudentCourseByStudentIDCourseID(studentCourse.StudentId, studentCourse.CourseId);

            if (studentCourseUpdate != null)
            {
                if (studentCourseUpdate.Validate == null || (studentCourse.Validate.Value.Day != DateTime.Now.Day))
                {
                    //daca nu a validat deloc sau nu a validat azi
                    studentCourse.Validate = DateTime.Now;
                    switch (serverInformation.classRoom.TypeOfStudy)
                    {
                        case "Course": studentCourse.AttendanceStudentCourse++; break;
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

        }*/
        public StudentCourse Delete(StudentCourse studentCourse)
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

        public List<StudentCourse> GetStudentCoursesByCourseID(int courseID)
        {
            var studentCourses = schoolDBFirstContext.StudentCourse.Where(sc => sc.CourseId == courseID).ToList();
            return remadeNullInformations(studentCourses);
        }

        public List<StudentCourse> GetStudentCoursesByStudentID(int studentID)
        {
            var studentCourses = schoolDBFirstContext.StudentCourse.Where(sc => sc.StudentId == studentID).ToList();
            return remadeNullInformations(studentCourses);
        }

        public List<StudentCourse> GetAll()
        {
            var studentCourses = schoolDBFirstContext.StudentCourse.ToList();
            return remadeNullInformations(studentCourses);
        }
        public List<StudentCourse> remadeNullInformations(List<StudentCourse> studentCourses)
        {
            foreach (var studentCourse in studentCourses)
            {
                studentCourse.Student = schoolDBFirstContext.Student.Where(s => s.StudentId == studentCourse.StudentId).FirstOrDefault();
                studentCourse.Course = schoolDBFirstContext.Course.Where(c => c.CourseId == studentCourse.CourseId).FirstOrDefault();
            }
            return studentCourses;
        }

        public StudentCourse UpdateStudentCourse(StudentCourse studentCourse)
        {
            throw new NotImplementedException();
        }

        
    }
}
