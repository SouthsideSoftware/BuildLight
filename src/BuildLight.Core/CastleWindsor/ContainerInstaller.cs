using BuildLight.Core.Configuration;
using BuildLight.Core.Server;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DelcomSupport;

namespace BuildLight.Core.CastleWindsor
{
    public class ContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDelcomLight>().ImplementedBy<DelcomLight>(),
                Component.For<IBuildIndicator>().ImplementedBy<DelcomUsbLightBuildIndicator>(),
                Component.For<IBuildStatusChecker>().ImplementedBy<BuildStatusChecker>(),
                Component.For<IBuildStatusServer>().ImplementedBy<BuildStatusServer>(),
                Component.For<IApplicationConfiguration>().ImplementedBy<ApplicationConfiguration>());
        }
    }
}
