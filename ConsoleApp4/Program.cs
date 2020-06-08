using System;
using Microsoft.Owin.Hosting;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:8080";

            using (WebApp.Start<ClassLibrary1.Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.ReadLine();
            }
        }
    }
}
