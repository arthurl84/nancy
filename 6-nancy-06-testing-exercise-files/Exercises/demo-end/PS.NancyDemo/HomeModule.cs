using System;
using Nancy;

namespace PS.NancyDemo
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            /****** Root routes ******/
            Func<Request, bool> _isNotApiClient = request =>
                                                  !request.Headers.UserAgent.ToLower().StartsWith("curl");

            Get["/", ctx => _isNotApiClient.Invoke(ctx.Request)] = p => Response.AsRedirect("/courses");
            Get["/"] = p => Response.AsRedirect("/api/courses");
        }
    }
}