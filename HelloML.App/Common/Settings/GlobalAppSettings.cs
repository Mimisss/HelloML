using System.Globalization;

namespace HelloML.App.Common.Settings
{
    public class GlobalAppSettings
    {
        public static GlobalAppSettings Instance = new GlobalAppSettings();

        public GlobalAppSettings()
        {
            Languages = new List<string>();
            General = new GeneralSettings();
            Run = new RunSettings();
            Instance = this;
        }

        public List<string> Languages { get; }
        public GeneralSettings General { get; set; }
        public RunSettings Run { get; set; }

        public IList<CultureInfo> SupportedCultures()
        {
            var list = new List<CultureInfo>();
            foreach (var language in Languages)
            {
                if (!string.IsNullOrWhiteSpace(language))
                {
                    list.Add(new CultureInfo(language));
                }
            }
            return list;
        }

        public static IList<CultureInfo> GetSupportedCultures(string[] languages)
        {
            if (languages == null)
            {
                return new List<CultureInfo>();
            }

            var list = new List<CultureInfo>();
            foreach (var language in languages)
            {
                if (!string.IsNullOrWhiteSpace(language))
                {
                    list.Add(new CultureInfo(language));
                }
            }

            return list;
        }
    }
}