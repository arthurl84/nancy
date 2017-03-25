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
           

        }

    }
}