using System.Reflection;
using Nancy.Bootstrapper;
using Nancy.Testing;
using Xunit;

namespace PS.NancyDemo.Test
{
    public class HomeModuleTest
    {
        [Fact]
        public void TestHomeRoot()
        {
            //Arrange
            var bootstrapper = new ConfigurableBootstrapper(with =>
                {
                    with.Module<HomeModule>();
                });
            var browser = new Browser(bootstrapper);

            //Act
            var response = browser.Get("/");

            //Assert

            response.ShouldHaveRedirectedTo("/courses");
        }

        [Fact]
        public void TestApiHomeRoot()
        {
            //Arrange
            var bootstrapper = new ConfigurableBootstrapper(with =>
                {
                    with.Module<HomeModule>();
                });
            var browser = new Browser(bootstrapper);

            //Act
            var response = browser.Get("/", with=>
                {
                    with.Header("User-Agent","curl");
                });

            //Assert

            response.ShouldHaveRedirectedTo("/api/courses");
        }
    }
}