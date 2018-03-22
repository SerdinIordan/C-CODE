using System;
using System.Collections.Generic;

namespace LicentaProiect.Models
{
    public partial class ClassRoomCourse
    {
        public int ClassRoomId { get; set; }
        public int CourseId { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan? StartHour { get; set; }
        public TimeSpan? EndHour { get; set; }

        public ClassRoom ClassRoom { get; set; }
        public Course Course { get; set; }
    }
}
