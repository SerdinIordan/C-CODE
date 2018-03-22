using LicentaProiect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicentaProiect.Repositoryes
{
    public class StudentRepository
    {
        private SchoolDBContext schoolDBFirstContext =
              SchoolDBContext.Instance;

        public Student SaveStudent(Student student)
        {
            // student.StudentCourses = null;
            schoolDBFirstContext.Student.Add(student);
            schoolDBFirstContext.SaveChanges();
            return student;
        }
        public Student UpdateStudent(Student student)
        {
            student.StudentCourse = null;
            schoolDBFirstContext.Student.Update(student);
            schoolDBFirstContext.SaveChanges();
            return student;
        }
        public Student RemoveStudent(Student student)
        {
            schoolDBFirstContext.Student.Remove(student);
            schoolDBFirstContext.SaveChanges();
            return student;
        }
        public Student GetStudentByID(int id)
        {
            var student = schoolDBFirstContext.Student.Where(s => s.StudentId == id).FirstOrDefault();
            return student;
        }
        public Student GetStudentByCOD(string cod)
        {
            var student = schoolDBFirstContext.Student.Where(s => s.CodCard.Equals(cod)).FirstOrDefault();
            return student;
        }
        public void displayStudent(Student student)
        {
            Console.WriteLine("Student ID: {0}", student.StudentId);
            Console.WriteLine("Student FirstName: {0}", student.FirstName);
            Console.WriteLine("Student LastName: {0}", student.LastName);
            Console.WriteLine("Student FatherInitial: {0}", student.FatherInitial);
            Console.WriteLine("Student Country: {0}", student.Country);
            Console.WriteLine("Student County: {0}", student.County);
            Console.WriteLine("Student Locality: {0}", student.Locality);
            Console.WriteLine("Student DateOfBirth: {0}", student.DateOfBirth);
            Console.WriteLine("Student Address: {0}", student.Address);
            Console.WriteLine("Student Profile: {0}", student.Profile);
            Console.WriteLine("Student AverageAdmission: {0}", student.AverageAdmission);

        }



    }
}
