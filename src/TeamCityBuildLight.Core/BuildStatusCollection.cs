using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NLog;
using TeamCityBuildLight.Core.CodeContracts;

namespace TeamCityBuildLight.Core
{
    public class BuildStatusCollection : List<BuildStatus>
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public BuildStatusCollection(string projectXml)
        {
            ParameterCheck.StringRequiredAndNotWhitespace(projectXml, "projectXml");

            try
            {
                var doc = XDocument.Parse(projectXml);
                foreach (var projectElement in doc.Descendants("Project"))
                {
                    Add(new BuildStatus(projectElement));
                }
            }
            catch (Exception err)
            {
                logger.ErrorException("Error parsing xml from TeamCity.", err);
            }
        }
    }
}