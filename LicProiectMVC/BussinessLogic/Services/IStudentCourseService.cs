using BussinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public interface IStudentCourseService
    {
        StudentCourse UpdateStudentCourse(StudentCourse studentCourse);
        StudentCourse GetStudentCourseByStudentIDCourseID(int studentID, int courseID);
        List<StudentCourse> GetStudentCoursesByCourseID(int courseID);
        List<StudentCourse> GetStudentCoursesByStudentID(int studentID);
    }
}
