using Autofac;

namespace MoviesApp.BLL
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //services.AddSingleton<MovieService>();
            builder.RegisterType<MovieService>().SingleInstance();
            builder.RegisterType<MemberService>().SingleInstance();
            builder.RegisterType<VoteService>().SingleInstance();
            builder.RegisterType<RatingService>().SingleInstance();
            //per request
            //builder.RegisterType<MovieService>().InstancePerLifetimeScope();

            //per resolve
            //builder.RegisterType<MovieService>().InstancePerDependency();

            //interface
            //builder.RegisterType<MovieService>().As<IInterface>();

            builder.RegisterModule<DAL.AutofacModule>();
        }
    }
}
