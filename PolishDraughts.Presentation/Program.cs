using System;
using System.Reflection;
using Autofac;
using PolishDraughts.Core.Entities.Games;
using PolishDraughts.Core.Entities.Players;
using PolishDraughts.Core.Interfaces;

namespace PolishDraughts.Presentation
{
    internal class Program
    {
        private static void Main()
        {
            var container = ConfigureContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<Menu>().MainMenu();
            }
        }

        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Menu>().AsSelf();
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Player)))
                .Where(t => t.IsSubclassOf(typeof(Player))).AsSelf().InstancePerDependency();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<IDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(IDependency)))
                .AssignableTo<IDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();
            return builder.Build();

        }
    }
}
