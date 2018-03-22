using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicProiectMVC.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int? AttendanceStudentCourse { get; set; }
        public int? AttendanceStudentLab { get; set; }
        public int? AttendanceStudentSeminar { get; set; }
        public DateTime? Validate { get; set; }
        public int? RequestEnrollment { get; set; }
        public int? AccessCourse { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}