using System;
using System.Collections.Generic;

namespace BussinessLogic.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Course = new HashSet<Course>();
        }

        public int TeacherId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Fct { get; set; }
        public string Email { get; set; }
        public string Studies { get; set; }
        public string ProfessionalExperience { get; set; }
        public string PublishedWorks { get; set; }
        public string More { get; set; }

        public User User { get; set; }
        public ICollection<Course> Course { get; set; }
    }
}
