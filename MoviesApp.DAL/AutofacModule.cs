using Autofac;
using MoviesApp.Domain;

namespace MoviesApp.DAL
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<MovieSqlRepository>().As<ICrudRepository<Movie, int>>().SingleInstance();
            builder.RegisterType<MovieDapSqlRepository>().As<IMovieRepository>().SingleInstance();
            //builder.RegisterType<MemberSqlRepository>().As<ICrudRepository<Member, int>>().SingleInstance();
            builder.RegisterType<MemberDapSqlRepository>().As<IMemberRepository>().SingleInstance();
            //builder.RegisterType<VoteSqlRepository>().As<ICrudRepository<Vote, (int, int)>>().SingleInstance();
            builder.RegisterType<VoteDapSqlRepository>().As<IVoteRepository>().SingleInstance();
        }
    }
}
