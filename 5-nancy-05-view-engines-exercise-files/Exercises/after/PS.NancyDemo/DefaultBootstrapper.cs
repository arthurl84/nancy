using Nancy;

namespace PS.NancyDemo
{
    public class DefaultBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(Nancy.Conventions.NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.ViewLocationConventions.Add((viewName, model, context)=>
                string.Concat(context.ModuleName, "/views/", viewName.ToLower()));
        }   
    }
}