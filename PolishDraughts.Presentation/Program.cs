using System;
using System.Reflection;
using Autofac;
using PolishDraughts.Core.Entities.Games;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Presentation
{
    internal class Program
    {
        private static void Main()
        {
            var container = ConfigureContainer();
            container.Resolve<Game>().Run();
        }

        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Random>().AsSelf();
            builder.RegisterType<View>().As<IView>().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<IDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
            return builder.Build();

        }
    }
}
