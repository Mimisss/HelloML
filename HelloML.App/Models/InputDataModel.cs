using System.ComponentModel.DataAnnotations;

namespace HelloML.App.Models
{
    public class InputDataModel
    {
        [Display(Name = "Καιρός")]
        public string Outlook { get; set; }

        [Display(Name = "Θερμοκρασία")]
        public string Temperature { get; set; }

        [Display(Name = "Υγρασία")]
        public string Humidity { get; set; }

        [Display(Name = "Άνεμος")]
        public string Windy { get; set; }
    }
}
