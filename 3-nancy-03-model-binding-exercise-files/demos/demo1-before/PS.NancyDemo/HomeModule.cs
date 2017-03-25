using System;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Responses;

namespace PS.NancyDemo
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            /****** Root routes ******/
            Func<Request, bool> _isNotApiClient = request =>
                !request.Headers.UserAgent.ToLower().StartsWith("curl");

            Get["/", ctx=> _isNotApiClient.Invoke(ctx.Request)] = p => View["index.html"];
            Get["/"] = p => "Welcome to the Pluralsight API";
            
            /****** Course routes ******
             * 
             * GET list of courses
             * GET single course
             * POST single course
             * Return responses as JSON 
             * 
             ***************************/
            
            Get["/courses"] = p => new JsonResponse(Repository.Courses, new DefaultJsonSerializer());

            Get["/courses/{id}"] = p => Response.AsJson((Course)Repository.GetCourse(p.id));

            Post["/courses"] = p =>
                {
                    var name = this.Request.Form.Name;
                    var author = this.Request.Form.Author;
                    var course = Repository.AddCourse(name, author);

                    string url = string.Format("{0}/{1}", this.Context.Request.Url, course.Id);

                    return new Response()
                        {
                            StatusCode = HttpStatusCode.Accepted
                        }
                        .WithHeader("Location", url);
                };
        }
    }
}