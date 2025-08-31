using HelloML.App.Application;
using HelloML.App.Models;
using HelloML.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

namespace HelloML.App.Controllers
{
    public class PlayGolfController : Controller
    {
        private readonly PlayGolfPredictionService Service;

        public PlayGolfController(PredictionEnginePool<ModelInput, ModelOutput> engine)
        {
            Service = new PlayGolfPredictionService(engine);
        }

        public IActionResult Predict(InputDataModel input)
        {
            var model = new ModelInput
            {
                Outlook = input.Outlook,
                Temperature = input.Temperature,
                Humidity = input.Humidity,
                Windy = input.Windy
            };

            var response = Service.Predict(model);
            return Json(response);
        }
    }
}
