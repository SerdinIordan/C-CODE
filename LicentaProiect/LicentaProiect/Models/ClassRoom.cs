using System;
using System.Collections.Generic;

namespace LicentaProiect.Models
{
    public partial class ClassRoom
    {
        public ClassRoom()
        {
            ClassRoomCourse = new HashSet<ClassRoomCourse>();
        }

        public int ClassRoomId { get; set; }
        public string ClassRoomName { get; set; }
        public string TypeOfStudy { get; set; }

        public ICollection<ClassRoomCourse> ClassRoomCourse { get; set; }
    }
}
