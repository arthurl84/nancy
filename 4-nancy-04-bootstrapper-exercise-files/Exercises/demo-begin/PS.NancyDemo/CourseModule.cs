using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;

namespace PS.NancyDemo
{
    public class CourseModule : NancyModule
    {
        public CourseModule() : base("/courses")
        {
            Before += ctx =>
                {
                    ctx.Items.Add("start_time", DateTime.UtcNow);
                    if (!ctx.Request.Headers.UserAgent.ToLower().StartsWith("curl"))
                        return new NotFoundResponse();
                    return null;
                };

            After += ctx =>
                {
                    //How long did this take to process?
                    var processTime = (DateTime.UtcNow - (DateTime) ctx.Items["start_time"]).TotalMilliseconds;

                    System.Diagnostics.Debug.WriteLine("Processing Time: " + processTime);

                    ctx.Response.WithHeader("x-processing-time", processTime.ToString());
                };
            Get["/"] = p => new JsonResponse(Repository.Courses, new DefaultJsonSerializer());

            Get["/{id}"] = p => Response.AsJson((Course) Repository.GetCourse(p.id));

            Post["/", c => c.Request.Headers.ContentType != "application/x-www-urlencoded"] = p =>
                {
                    var course = this.Bind<Course>();
                    Repository.AddCourse(course);
                    return NewCourseResponse(course);
                };
            Post["/"] = p =>
                {
                    var name = this.Request.Form.Name;
                    var author = this.Request.Form.Author;
                    var course = Repository.AddCourse(name, author);

                    return NewCourseResponse(course);
                };
        }

        Response NewCourseResponse(dynamic course)
        {
            string url = string.Format("{0}/{1}", this.Context.Request.Url, course.Id);

            return new Response()
                {
                    StatusCode = HttpStatusCode.Accepted
                }
                .WithHeader("Location", url);
        }
    }
}