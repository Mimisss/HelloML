using HelloML.App.Common.Settings;

namespace HelloML.App.Models
{
    public class ViewBaseModel
    {
        protected ViewBaseModel(string title = "")
        {
            Settings = GlobalAppSettings.Instance;
            if (string.IsNullOrWhiteSpace(title))
            {
                title = Settings.General.ApplicationTitle;
            }

            Title = title;
        }

        public static ViewBaseModel Default(string title = "")
        {
            var model = new ViewBaseModel(title);
            return model;
        }

        public string Title { get; set; }

        public GlobalAppSettings Settings { get; }

        public virtual bool IsValid()
        {
            return true;
        }
    }
}
