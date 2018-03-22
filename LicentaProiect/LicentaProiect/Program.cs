using LicentaProiect.Server;
using System;

namespace LicentaProiect
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerClass serverClass = new ServerClass();
            serverClass.launchServer();
            Console.ReadLine();
        }
    }
}
