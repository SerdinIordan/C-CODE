using EFProject.Repositoryes;
using LicProiectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LicProiectMVC.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult PersonalDataStudent()
        {
            try
            {
                var studentRepository = new StudentRepository();
                int userID = int.Parse(Session["userID"].ToString());
                var student = studentRepository.GetStudentByUserID(userID);
                var modelViewStudent = AutoMapper.Mapper.Map<BussinessLogic.Models.Student, Models.Student>(student);

                return View(modelViewStudent);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult MyStudentCourses()
        {
            try
            {
                var studentRepository = new StudentRepository();
                int userID = int.Parse(Session["userID"].ToString());
                var student = studentRepository.GetStudentByUserID(userID);
                var modelViewStudent = AutoMapper.Mapper.Map<BussinessLogic.Models.Student, Models.Student>(student);
                var listStudentCourses = modelViewStudent.StudentCourse.Where(sc => sc.AccessCourse == 1);
                return View(listStudentCourses);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AllCourses(string sortOrder)
        {
            try
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

                int userID = int.Parse(Session["userID"].ToString());
                var courseRepository = new CourseRepository();
                var allCourses = courseRepository.GetAll();
                switch (sortOrder)
                {
                    case "name_desc":allCourses=allCourses.OrderByDescending(c => c.Name).ToList();break;
                    default: allCourses = allCourses.OrderBy(c => c.Name).ToList(); break;  
                }
                IList<Course> modelViewAllCourses = new List<Course>();
                foreach (var course in allCourses)
                {
                    var modelViewCourse = AutoMapper.Mapper.Map<BussinessLogic.Models.Course, Models.Course>(course);
                    modelViewAllCourses.Add(modelViewCourse);
                }
                displayButtonRequestEnrollmentAllCourses(modelViewAllCourses, userID);
                
                return View(modelViewAllCourses);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ActionRequestEnrollment(int courseID)
        {
            try
            {

                int userID = int.Parse(Session["userID"].ToString());
                var studentRepository = new StudentRepository();
                var student = studentRepository.GetStudentByUserID(userID);


                var studentCourse = new BussinessLogic.Models.StudentCourse();
                studentCourse.StudentId = student.StudentId;
                studentCourse.CourseId = courseID;
                studentCourse.RequestEnrollment = 1;

                var studentCourseRepository = new StudentCourseRepository();
                studentCourseRepository.RequestEnrollment(studentCourse);

                return RedirectToAction("AllCourses");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CourseDetail(int courseID)
        {
            try
            {
                var courseRepository = new CourseRepository();
                var course = courseRepository.GetByID(courseID);
                var modelViewCourse = AutoMapper.Mapper.Map<BussinessLogic.Models.Course, Models.Course>(course);
                int userID = int.Parse(Session["userID"].ToString());
                displayButtonRequestEnrollmentCourse(modelViewCourse, userID);
                return View(modelViewCourse);
            }
            catch
            {
                return View();
            }
        }

        public void displayButtonRequestEnrollmentAllCourses(IList<Course> modelViewAllCourses, int userID)
        {
            var studentRepository = new StudentRepository();
            var student = studentRepository.GetStudentByUserID(userID);

            foreach (var course in modelViewAllCourses)
            {
                makeDisplayButtonRequestEnrollmentCourse(course, student);
            }
        }
        public void displayButtonRequestEnrollmentCourse(Course course, int userID)
        {
            var studentRepository = new StudentRepository();
            var student = studentRepository.GetStudentByUserID(userID);
            makeDisplayButtonRequestEnrollmentCourse(course, student);

        }
        public void makeDisplayButtonRequestEnrollmentCourse(Course course, BussinessLogic.Models.Student student)
        {
            course.RequestEnrollmentButton = 1;
            foreach (var studentCourse in course.StudentCourse)
            {
                if (studentCourse.StudentId == student.StudentId && studentCourse.RequestEnrollment == 1 && studentCourse.AccessCourse == 1)
                {
                    course.RequestEnrollmentButton = -1; //sa nu mai apara butonul
                }

                else if (studentCourse.StudentId == student.StudentId && studentCourse.RequestEnrollment == 1 && studentCourse.AccessCourse == null)
                {
                    course.RequestEnrollmentButton = 0;  //sa apara disable (sa nu mai poti da solicitare)
                }
            }
        }
    }
}