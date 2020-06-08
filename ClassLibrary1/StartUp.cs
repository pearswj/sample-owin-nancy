using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Nancy;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(ClassLibrary1.Startup))]
namespace ClassLibrary1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);
            //app.Use<LoggingMiddleware>();
            //app.Use<TokenMiddleware>();
            app.UseNancy();
        }
    }

    public class LoggingMiddleware : OwinMiddleware
    {
        public LoggingMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext ctx)
        {
            IOwinRequest req = ctx.Request;

            //foreach (var key in context.Request.Headers.Keys)
            //{
            //    Console.WriteLine($"{key} - {context.Request.Headers[key]}");
            //}

            // invoke the next middleware in the pipeline
            await Next.Invoke(ctx);

            IOwinResponse res = ctx.Response;
            string contentLength = res.ContentLength > -1 ? res.ContentLength.ToString() : "-";

            // log request in apache format
            string msg = $"{req.RemoteIpAddress} - [{DateTime.Now:o}] \"{req.Method} {req.Uri.AbsolutePath} {req.Protocol}\" {res.StatusCode} {contentLength}";
            Console.WriteLine(msg);
        }
    }

    public class TokenMiddleware : OwinMiddleware
    {
        public TokenMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override Task Invoke(IOwinContext ctx)
        {
            if (ctx.Request.Method == "GET" || ctx.Request.Method == "OPTIONS")
                return Next.Invoke(ctx);

            if (ctx.Request.Headers.TryGetValue("Rhino-Compute-Token", out string[] token))
            {
                Console.WriteLine(token[0]);
                if (token[0] != "secret")
                {
                    ctx.Response.StatusCode = 401;
                    return ctx.Response.WriteAsync("Invalid token");
                }
                return Next.Invoke(ctx);
            }

            ctx.Response.StatusCode = 401;
            return ctx.Response.WriteAsync("Token missing");
        }
    }
}
