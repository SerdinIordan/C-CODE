using LicentaProiect.Models;
using LicentaProiect.Repositoryes;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace LicentaProiect.Server
{
    public class ServerClass
    {
        static bool isConnected = false;
        static SerialPort port;
        ServerInformation serverInformation = new ServerInformation();
        //PrelucrateStrings  prelucrateStrings;

        private void connectToArduino(string selectedPort)
        {
            if (!isConnected)
            {
                isConnected = true;
                serverConnected = true;
                port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
                port.Open();
            }
        }
        private void disconnectFromArduino()
        {
            if (isConnected)
            {
                isConnected = false;
                serverConnected = false;
                port.Write("#STOP\n");
                port.Close();
            }
        }

        public bool serverConnected = false;
        public string modifiedOrar;
        public void functionality()
        {
            /*  getAvailableComPorts();
              Console.WriteLine("Choose the port:");
              foreach (string port in ports)
              {
                  Console.WriteLine(port);
              }
              string selectPort = Console.ReadLine();*/
            ClassRoomCourseRepository classRoomCourseRepository = new ClassRoomCourseRepository();
            string messageCourseDisplay = classRoomCourseRepository.getCourseMessageDisplayByLocalTime();
            if (serverConnected)
            {
                //orarul se va actualiza in functie de ora curenta
                if (modifiedOrar != messageCourseDisplay && isConnected && messageCourseDisplay != null)
                {
                    modifiedOrar = messageCourseDisplay;
                    port.Write("#TEXT" + messageCourseDisplay + "#\n");

                    serverInformation = classRoomCourseRepository.getCourseServerInformationByLocalTime();
                }
                else if (messageCourseDisplay == null && modifiedOrar == null && isConnected)
                {
                    errorMessage = "In acest moment nu se desfasoara niciun curs";
                    modifiedOrar = errorMessage;
                    port.Write("#EROR" + errorMessage + "#\n");
                }
                if (isConnected && displayPageDetail)
                {
                    port.Write("#TEXT" + messageCourseDisplay + "#\n");
                }
            }
            else
            {
                Console.WriteLine("Server connect...");
                connectToArduino("COM5");
                // port.ReadTimeout = 100;
            }

            Thread readThread = new Thread(Read);
            // _continue = true;
            readThread.Start();

            readThread.Join();
            // Read();

        }
        public ServerInformation getStudentDetail(string message)
        {
            var goodMessage = message.Substring(0, message.Length - 1);
            var pozCODStudent = goodMessage.IndexOf("CODStudent:");//11 caractere
            string CODEStudent = goodMessage.Substring(11);

            StudentRepository studentRepository = new StudentRepository();
            serverInformation.student = studentRepository.GetStudentByCOD(CODEStudent);
            if (serverInformation.student != null)
            {
                StudentCourse studentCourse = new StudentCourse();
                studentCourse.StudentId = serverInformation.student.StudentId;
                studentCourse.CourseId = serverInformation.course.CourseId;

                StudentCourseRepository studentCourseRepository = new StudentCourseRepository();
                serverInformation.studentCourse = studentCourseRepository.UpdateStudentCourse(studentCourse,serverInformation);
            }

            return serverInformation;
        }
        public string getMessageStudentDetail(ServerInformation serverInformation)
        {
            string messageStudentDetail;
            messageStudentDetail = "FirstName:" + serverInformation.student.FirstName
                + "LastName:" + serverInformation.student.LastName
                + "AttendanceStudentCourse:" + serverInformation.studentCourse.AttendanceStudentCourse
                + "AttendanceStudentLab:" + serverInformation.studentCourse.AttendanceStudentLab
                + "AttendanceStudentSeminar:" + serverInformation.studentCourse.AttendanceStudentSeminar;
            return messageStudentDetail;
        }

        static bool displayPageDetail = false;
        static string modifiedCard = null;
        static string errorMessage;
        static int firstConnection = 0;
        public void Read()
        {
            try
            {
                if (port.IsOpen)
                {
                    if (port.BytesToRead != 0)
                    {
                        string message = port.ReadLine();
                        // byte byte_buffer = (byte)port.ReadByte();

                        //  string message = "" + char.ConvertFromUtf32(byte_buffer).ToString();
                        if (message != null && message.Contains("CODStudent:"))
                        {
                            //prima data
                            if (modifiedCard != message)
                            {
                                modifiedCard = message;
                                serverInformation = getStudentDetail(message);
                                if (serverInformation.student != null && serverInformation.studentCourse != null)
                                {
                                    string messageStudentDetail = getMessageStudentDetail(serverInformation);
                                    port.Write("#DETA" + messageStudentDetail + "#\n");
                                    //serverInformation.student.Image
                                }
                                else if (serverInformation.student == null)
                                {
                                    errorMessage = "Studentul nu se afla in baza de date";
                                    port.Write("#EROR" + errorMessage + "#\n");
                                }
                                else if (serverInformation.studentCourse == null)
                                {
                                    errorMessage = "Studentul nu este inscris la acest curs";
                                    port.Write("#EROR" + errorMessage + "#\n");
                                }

                                displayPageDetail = true;
                                Thread.Sleep(5000);
                            }
                            else
                            {
                                displayPageDetail = false;
                                disconnectFromArduino();
                            }

                        }

                    }
                }
            }
            catch (TimeoutException e)
            {

            }
        }

        public void launchServer()
        {
            Thread t = new Thread(() => {
                while (true)
                {
                    functionality();
                }
            });
            t.Start();

        }


    }
}
