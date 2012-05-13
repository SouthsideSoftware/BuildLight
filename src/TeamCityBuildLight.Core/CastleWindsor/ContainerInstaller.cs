using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DelcomSupport;

namespace TeamCityBuildLight.Core.CastleWindsor
{
    public class ContainerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDelcomLight>().ImplementedBy<DelcomLight>(),
                Component.For<IBuildIndicator>().ImplementedBy<DelcomUsbLightBuildIndicator>(),
                Component.For<IBuildStatusChecker>().ImplementedBy<BuildStatusChecker>());
        }
    }
}
