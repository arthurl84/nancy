using Nancy.Testing;
using Nancy.Testing.Fakes;
using Nancy.ViewEngines.Razor;
using Xunit;

namespace PS.NancyDemo.Test
{
    public class CourseModuleTest
    {
        [Fact]
        public void TestCoursesRoute()
        {
            FakeRootPathProvider.RootPath = "../../../PS.NancyDemo/Course/Views";

            //Arrange
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.RootPathProvider(new FakeRootPathProvider());
                with.Module<CourseModule>();
            });
            var browser = new Browser(bootstrapper);

            //Act
            var response = browser.Get("/courses");

            //Assert
            response.Body["body"].ShouldExist();
        }


        [Fact]
        public void TestCoursesByIdRoute()
        {
            FakeRootPathProvider.RootPath = "../../../PS.NancyDemo/Course/Views";

            //Arrange
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.RootPathProvider(new FakeRootPathProvider());
                with.Module<CourseModule>();
            });
            var browser = new Browser(bootstrapper);

            //Act
            var response = browser.Get("/courses/0");

            //Assert
            response.Body["body"].ShouldExist();
        }


    }
}