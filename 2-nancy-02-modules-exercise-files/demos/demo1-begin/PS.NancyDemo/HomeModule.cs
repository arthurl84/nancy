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
            Get["/"] = p => View["index.html"];

            /*
             * cUrl is a command-line tool for transferring data
             * with URL Syntax. Download it from:
             * http://curl.haxx.se/download
             */
        }
    }
}