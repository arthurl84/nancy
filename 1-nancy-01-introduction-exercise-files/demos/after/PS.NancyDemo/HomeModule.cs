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
        }
    }
}