using HelloML.App.Application;
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

        public IActionResult Predict(ModelInput input)
        {
            var response = Service.Predict(input);
            return Json(response);
        }
    }
}
