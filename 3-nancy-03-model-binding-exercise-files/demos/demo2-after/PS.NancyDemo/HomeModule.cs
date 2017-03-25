using System;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;

namespace PS.NancyDemo
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            /****** Root routes ******/
            Func<Request, bool> _isNotApiClient = request =>
                                                  !request.Headers.UserAgent.ToLower().StartsWith("curl");

            Get["/", ctx => _isNotApiClient.Invoke(ctx.Request)] = p => View["index.html"];
            Get["/"] = p => "Welcome to the Pluralsight API";
        }
    }
}