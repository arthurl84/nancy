using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace PS.NancyDemo
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Func<Request,bool> _isNotApiClient = request =>
                !request.Headers.UserAgent.ToLower().StartsWith("curl");

            Get["/", ctx=> _isNotApiClient.Invoke(ctx.Request)] = p => View["index.html"];

            Get["/"] = p => "Welcome to the Pluralsight API";

            /*
             * cUrl is a command-line tool for transferring data
             * with URL Syntax. Download it from:
             * http://curl.haxx.se/download
             */
        }
    }
}