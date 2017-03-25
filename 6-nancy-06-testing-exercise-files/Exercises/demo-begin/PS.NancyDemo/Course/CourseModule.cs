using Nancy;
using Nancy.ModelBinding;

namespace PS.NancyDemo
{
    public class CourseModule : NancyModule
    {
        const string CoursesPath = "/courses";

        public CourseModule(Repository repository)
            : base(CoursesPath)
        {
            Get["/"] = p => View["courses.html", repository.Courses];
            Get["/{id}"] = p => View[repository.GetCourse(p.Id)];

        }

    }
}