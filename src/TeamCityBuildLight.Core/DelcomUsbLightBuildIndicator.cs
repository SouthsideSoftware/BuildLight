using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamCityBuildLight.Core
{
    public class DelcomUsbLightBuildIndicator : IDisposable
    {
        public void ShowIndicator(BuildStatusCollection buildStatusCollection){
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
        }


    }
}
