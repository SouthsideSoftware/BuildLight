using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using NLog;
using TeamCityBuildLight.Core.CodeContracts;

namespace TeamCityBuildLight.Core
{
    public class BuildStatus
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public string LastBuildStatus { get; private set; }
        public string Project { get; private set; }
        public string Configuration { get; private set; }

        public BuildStatus(XElement projectElement)
        {
            ParameterCheck.ParameterRequired(projectElement, "projectElement");

            try
            {
                LastBuildStatus = GetAttributeValue(projectElement, "lastBuildStatus");
                var name = GetAttributeValue(projectElement, "name");
                if (name != null)
                {
                    var nameParts = Regex.Split(name, "::");
                    if (nameParts.Length == 2)
                    {
                        Project = nameParts[0].Trim();
                        Configuration = nameParts[1].Trim();
                    }
                }

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

        private string GetAttributeValue(XElement element, string attrName)
        {
            var attr = element.Attribute(attrName);
            return attr != null ? attr.Value : null;
        }
    }
}
