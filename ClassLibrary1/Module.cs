using Nancy;
//using Nancy.Authentication.Stateless;
using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Module : NancyModule
    {
        public Module()
        {
            Get("/", args => "Hello World");
            Get("/error", args =>
            {
                throw new Exception("Hello World!");
            });
            Post("/", args => "Hey!");
            //Get("/abc", args =>
            //{
            //    return new Response { }
            //});
        }
    }
}
