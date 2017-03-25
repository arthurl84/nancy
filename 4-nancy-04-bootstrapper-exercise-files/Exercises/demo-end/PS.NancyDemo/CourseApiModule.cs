using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;

namespace PS.NancyDemo
{
    public class CourseApiModule : NancyModule
    {
        public CourseApiModule(Repository repository) : base("/api/courses")
        {
            Before += ctx =>
                {
                    ctx.Items.Add("start_time", DateTime.UtcNow);
                    if (!ctx.Request.Headers.UserAgent.ToLower().StartsWith("curl"))
                        return new RedirectResponse("/courses");
                    return null;
                };

            After += ctx =>
                {
                    //How long did this take to process?
                    var processTime = (DateTime.UtcNow - (DateTime) ctx.Items["start_time"]).TotalMilliseconds;

                    System.Diagnostics.Debug.WriteLine("Processing Time: " + processTime);

                    ctx.Response.WithHeader("x-processing-time", processTime.ToString());
                };
            Get["/"] = p => new JsonResponse(repository.Courses, new DefaultJsonSerializer());

            Get["/{id}"] = p => Response.AsJson((Course) Repository.GetCourse(p.id));

            Post["/", c => c.Request.Headers.ContentType != "application/x-www-urlencoded"] = p =>
                {
                    var course = this.Bind<Course>();
                    repository.AddCourse(course);
                    return Response.AsNewCourse(course);
                };
            Post["/"] = p =>
                {
                    var name = this.Request.Form.Name;
                    var author = this.Request.Form.Author;
                    var course = repository.AddCourse(name, author);
                    return Response.AsNewCourse((Course) course);
                };
        }
    }
}