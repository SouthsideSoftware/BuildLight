using BuildLight.Core.CastleWindsor;
using BuildLight.Core.Server;
using Castle.Windsor;
using Topshelf;

namespace BuildLight.Service
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
                        s.ConstructUsing(name => container.Resolve<IBuildStatusServer>());
                        s.WhenStarted(cb => cb.Start());
                        s.WhenStopped(cb => cb.Stop());
                    });
                x.RunAsLocalSystem();
                x.SetDescription("Update build indicator based on status of build server");
                x.SetDisplayName("BuildLight");
                x.SetServiceName("BuildLight");
            });
            host.Run();
        }
    }
}
