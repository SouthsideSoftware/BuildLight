using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NLog;
using TeamCityBuildLight.Core.CodeContracts;

namespace TeamCityBuildLight.Core
{
    public class BuildStatus
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public BuildStatus(XElement projectElement)
        {
            ParameterCheck.ParameterRequired(projectElement, "projectElement");

            try
            {
                foreach (var xAttribute in projectElement.Attributes())
                {
                    logger.Debug("{0} = {1}", xAttribute.Name, xAttribute.Value);
                }
            } 
            catch (Exception err)
            {
                logger.ErrorException(string.Format("Error parsing TeamCity project node {0}", projectElement), err);
            }
        }
    }
}
