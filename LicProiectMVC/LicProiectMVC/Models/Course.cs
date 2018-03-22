using BussinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicProiectMVC.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinimumAttendanceCourse { get; set; }
        public int MinimumAttendanceLab { get; set; }
        public int MinimumAttendanceSeminar { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
        public int RequestEnrollmentButton { get; set; }

        public Teacher Teacher { get; set; }
        public ICollection<ClassRoomCourse> ClassRoomCourse { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}