using LicentaProiect.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LicentaProiect.Server
{
    public class ServerInformation
    {
        public Student student { get; set; }
        public Course course { get; set; }
        public StudentCourse studentCourse { get; set; }
        public ClassRoom classRoom { get; set; }

    }
}
