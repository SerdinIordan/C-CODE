using System;
using System.Collections.Generic;

namespace BussinessLogic.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentCourse = new HashSet<StudentCourse>();
        }

        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string CodCard { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherInitial { get; set; }
        public int? Age { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Locality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public double AverageAdmission { get; set; }
        public string Profile { get; set; }
        public string Series { get; set; }
        public int Grup { get; set; }
        public byte[] Image { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }

        public User User { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
