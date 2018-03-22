using System;
using System.Collections.Generic;

namespace LicentaProiect.Models
{
    public partial class Course
    {
        public Course()
        {
            ClassRoomCourse = new HashSet<ClassRoomCourse>();
            StudentCourse = new HashSet<StudentCourse>();
        }

        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinimumAttendanceCourse { get; set; }
        public int MinimumAttendanceLab { get; set; }
        public int MinimumAttendanceSeminar { get; set; }

        public Teacher Teacher { get; set; }
        public ICollection<ClassRoomCourse> ClassRoomCourse { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
