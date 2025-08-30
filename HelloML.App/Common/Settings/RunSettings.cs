using System.Collections.Generic;

namespace HelloML.App.Common.Settings
{
    public class RunSettings
    {
        public RunSettings()
        {
            DevMode = true;
            EnableLogging = false;
            EnableDevExceptions = false;
        }

        public bool DevMode { get; set; }
        public bool EnableLogging { get; set; }
        public bool EnableDevExceptions { get; set; }
    }
}