using System;
using System.Timers;
using BuildLight.Core.CodeContracts;
using NLog;

namespace BuildLight.Core.Server
{
    public class BuildStatusServer : IBuildStatusServer
    {
        readonly IBuildIndicator buildIndicator;
        readonly IBuildStatusChecker buildStatusChecker;
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Timer timer;

        public BuildStatusServer(IBuildIndicator buildIndicator, IBuildStatusChecker buildStatusChecker)
        {
            ParameterCheck.ParameterRequired(buildIndicator, "buildIndicator");
            ParameterCheck.ParameterRequired(buildStatusChecker, "buildStatusChecker");

            this.buildIndicator = buildIndicator;
            this.buildStatusChecker = buildStatusChecker;
            timer = new Timer(Properties.Settings.Default.PollingIntervalSeconds * 1000);
            timer.Elapsed += PollBuildServer;
        }

        void PollBuildServer(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            timer.Stop();
            try
            {
                logger.Debug("Polling build server...");
                var status = buildStatusChecker.Check();
                buildIndicator.ShowIndicator(status);
                logger.Debug("Poll of build server completed");
            }
            catch(Exception err)
            {
                logger.ErrorException("Build status server poll failed with error.  Will retry at next interval", err);
            }
            timer.Start();
        }

        public void Start()
        {
            logger.Info("Starting service...");
            PollBuildServer(this, null);
            
        }

        public void Stop()
        {
            logger.Info("Stopping service...");
            timer.Stop();
            buildIndicator.Clear();
            logger.Info("Server stopped.");
        }
    }
}
