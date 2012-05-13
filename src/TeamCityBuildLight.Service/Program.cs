using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Castle.Windsor;
using TeamCityBuildLight.Core.CastleWindsor;
using TeamCityBuildLight.Core.Server;
using Topshelf;

namespace TeamCityBuildLight.Service
{
    class Program
    {
        static IWindsorContainer container;

        static void Main(string[] args)
        {
            container = new WindsorContainer();
            container.Install(new ContainerInstaller());

            var host = HostFactory.New(x =>
            {
                x.Service<IBuildStatusServer>(s =>
                {
                    s.SetServiceName("TeamCityBuildLight");
                    s.ConstructUsing(name => container.Resolve<IBuildStatusServer>());
                    s.WhenStarted(cb => cb.Start());
                    s.WhenStopped(cb => cb.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("Update build light based on status of build server");
                x.SetDisplayName("TeamCityBuildLight");
                x.SetServiceName("TeamCityBuildLight");
            });
            host.Run();
        }
    }
}
