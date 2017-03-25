using Nancy;

namespace PS.NancyDemo
{
    public static class CourseResponseExtensions
    {

        public static Response AsNewCourse(this IResponseFormatter formatter, Course course)
        {
            string url = string.Format("{0}/{1}", formatter.Context.Request.Url, course.Id);

            return new Response()
                {
                    StatusCode = HttpStatusCode.Created
                }
                .WithHeader("Location", url);
        }
    }
}