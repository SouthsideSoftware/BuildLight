using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TeamCityBuildLight.Core.CodeContracts;

namespace TeamCityBuildLight.Core
{
    public class BuildStatus
    {
    }

    public class BuildStatusCollection : List<BuildStatus>
    {
        public BuildStatusCollection(XDocument projectsXml)
        {
            ParameterCheck.ParameterRequired(projectsXml, "projectsXml");

            try
            {
                
            }
            catch (Exception err)
            {
                
            }
        }
    }
}
