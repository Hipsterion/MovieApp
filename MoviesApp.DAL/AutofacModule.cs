using Autofac;
using MoviesApp.Domain;

namespace MoviesApp.DAL
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlRepository>().As<ICrudRepository<Movie, int>>().SingleInstance();
        }
    }
}
