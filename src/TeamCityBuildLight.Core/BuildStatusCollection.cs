using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NLog;
using TeamCityBuildLight.Core.CodeContracts;
using System.Linq;

namespace TeamCityBuildLight.Core
{
    public class BuildStatusCollection : List<BuildStatus>, IBuildStatusSource
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public BuildStatusCollection(){}

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
                logger.ErrorException("Error parsing xml from TeamCity", err);
            }
        }

        public IndicatorStatus Status
        {
            get
            {
                if (Count == 0) return IndicatorStatus.Unknown;
                if (this.Any(s => s.Activity == "Building")) return IndicatorStatus.Building;
                if (this.Any(s => s.LastBuildStatus == "Failure")) return IndicatorStatus.Failure;
                return IndicatorStatus.Success;
            }
        }

    }
}