using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Bootstrapper : Nancy.DefaultNancyBootstrapper
    {
        private byte[] _favicon;
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            ApiKey.Initialize(pipelines);

            //pipelines.AfterRequest += ctx =>
            //{
            //    foreach (var key in ctx.Response.Headers.Keys)
            //    {
            //        Console.WriteLine($"{key} => {ctx.Response.Headers[key]}");
            //    }
            //};

            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("docs"));
        }

        //protected override byte[] FavIcon
        //{
        //    get { return _favicon ?? (_favicon = LoadFavIcon()); }
        //}

        private byte[] LoadFavIcon()
        {
            using (var resourceStream = GetType().Assembly.GetManifestResourceStream("compute.geometry.favicon.ico"))
            {
                var memoryStream = new System.IO.MemoryStream();
                resourceStream.CopyTo(memoryStream);
                return memoryStream.GetBuffer();
            }
        }
    }
}
