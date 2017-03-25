using Nancy;
using Nancy.Testing;
using Xunit;

namespace PS.NancyDemo.Test
{
    public class CourseApiModuleTest
    {
         [Fact]
         public void TestPostNewCourse()
         {
             var bootstrapper = new ConfigurableBootstrapper(with =>
                 {
                     with.Module<CourseApiModule>();
                 });
             var browser = new Browser(bootstrapper);

             var response = browser.Post("/api/courses", with =>
                 {
                     with.HttpRequest();
                     with.Header("User-Agent","curl");
                     with.FormValue("name", "Testing with Nancy");
                     with.FormValue("author", "Richard Cirerol");
                     with.Header("Content-Type", "application/x-www-form-urlencoded");
                 });

             Assert.Equal(HttpStatusCode.Created, response.StatusCode);
             Assert.Equal("application/x-www-form-urlencoded", response.Context.Request.Headers.ContentType);
             Assert.Contains("/api/courses/2", response.Headers["Location"]);

             var getresponse = response.Then.Get("/api/courses/2", with =>
                 {
                     with.Header("User-Agent", "curl");
                 });

             var course = getresponse.Body.DeserializeJson<Course>();

             Assert.Equal(2, course.Id);
             Assert.Equal("Testing with Nancy", course.Name);
             Assert.Equal("Richard Cirerol", course.Author);
             Assert.Empty(course.Modules);
         }
    }
}