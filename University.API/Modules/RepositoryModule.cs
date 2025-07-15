using Autofac;
using University.Core.Services;
using University.Data.Contexts;
using University.Data.Repositories;

namespace University.API.Modules;

public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UniversityDbContext>().AsSelf().InstancePerLifetimeScope();
    }
}