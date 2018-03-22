using BussinessLogic.Models;
using BussinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Repositoryes
{
   public class StudentRepository:IStudentService
    {
        private SchoolDBContext schoolDBFirstContext =
              SchoolDBContext.Instance;

        public Student Save(Student student)
        {
            // student.StudentCourses = null;
            schoolDBFirstContext.Student.Add(student);
            schoolDBFirstContext.SaveChanges();
            return student;
        }
        public Student Update(Student student)
        {
            student.StudentCourse = null;
            schoolDBFirstContext.Student.Update(student);
            schoolDBFirstContext.SaveChanges();
            return student;
        }
        public Student Delete(Student student)
        {
            schoolDBFirstContext.Student.Remove(student);
            schoolDBFirstContext.SaveChanges();
            return student;
        }
        public Student GetByID(int id)
        {
            var student = schoolDBFirstContext.Student.Where(s => s.StudentId == id).FirstOrDefault();
            remadeNullInformations(student);
            return student;
        }
        public Student GetStudentByCOD(string cod)
        {
            var student = schoolDBFirstContext.Student.Where(s => s.CodCard.Equals(cod)).FirstOrDefault();
            return student;
        }
        //
        public Student GetStudentByUserID(int id)
        {
            var student = schoolDBFirstContext.Student.Where(s => s.UserId==id).FirstOrDefault();
            remadeNullInformations(student);
            return student;
        }
        public List<Student> GetAll()
        {
            var listStudents = schoolDBFirstContext.Student.ToList();
            foreach (Student student in listStudents)
            {
                remadeNullInformations(student);
            }
            return listStudents;
        }

        public Student remadeNullInformations(Student student)
        {
            //la restul vin null
            student.StudentCourse = schoolDBFirstContext.StudentCourse.Where(sc => sc.StudentId == student.StudentId).ToList();
            foreach (var studentCourse in student.StudentCourse)
            {
                studentCourse.Course = schoolDBFirstContext.Course.Where(c => c.CourseId == studentCourse.CourseId).FirstOrDefault();
            }
            student.User = schoolDBFirstContext.User.Where(u => u.UserId == student.UserId).FirstOrDefault();
            return student;
        }
        
    }
}
