using LicentaProiect.Models;
using LicentaProiect.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicentaProiect.Repositoryes
{
    public class ClassRoomCourseRepository
    {

        private SchoolDBContext schoolDBFirstContext =
            SchoolDBContext.Instance;

        public string getCourseMessageDisplayByLocalTime()
        {
            var dateNow = DateTime.Now;
            var course = schoolDBFirstContext.ClassRoomCourse.Where(
                crc => crc.DayOfWeek.Equals(dateNow.DayOfWeek.ToString())
            && crc.StartHour <= dateNow.TimeOfDay &&
            crc.EndHour >= dateNow.TimeOfDay).FirstOrDefault();

            string stringCourseDisplay;
            if (course != null)
            {
                CourseRepository courseRepository = new CourseRepository();
                course.Course = courseRepository.GetCourseById(course.CourseId);
                ClassRoomRepository classRoomRepository = new ClassRoomRepository();
                course.ClassRoom = classRoomRepository.GetClassRoomById(course.ClassRoomId);



                stringCourseDisplay = "CourseID:" + course.Course.CourseId +
                    "CourseClass:" + course.ClassRoom.ClassRoomName +
                    "CourseName:" + course.Course.Name +
                            //"TeacherCourse:" + course.Course. +
                            "BeginEndCourse:" + course.StartHour.Value.ToString(@"hh\:mm") +
                            "-" + course.EndHour.Value.ToString(@"hh\:mm");


                return stringCourseDisplay;
            }
            return null;
        }

        public ServerInformation getCourseServerInformationByLocalTime()
        {
            var dateNow = DateTime.Now;
            var course = schoolDBFirstContext.ClassRoomCourse.Where(
                crc => crc.DayOfWeek.Equals(dateNow.DayOfWeek.ToString())
            && crc.StartHour <= dateNow.TimeOfDay &&
            crc.EndHour >= dateNow.TimeOfDay).FirstOrDefault();
            ServerInformation serverInformation = new ServerInformation();
            if (course != null)
            {
                CourseRepository courseRepository = new CourseRepository();
                course.Course = courseRepository.GetCourseById(course.CourseId);
                ClassRoomRepository classRoomRepository = new ClassRoomRepository();
                course.ClassRoom = classRoomRepository.GetClassRoomById(course.ClassRoomId);


                //put on ServerInformation
                serverInformation.course = course.Course;
                serverInformation.classRoom = course.ClassRoom;


                return serverInformation;
            }
            else
            {
                return null;
            }
        }
    }
}
