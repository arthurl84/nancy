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

            Get["/{id}"] = p => Response.AsJson((Course) repository.GetCourse(p.id));

            Post["/"] = p =>
                {
                    var course = this.Bind<Course>();
                    repository.AddCourse(course);
                    return Response.AsNewCourse(course);
                };
        }
    }
}