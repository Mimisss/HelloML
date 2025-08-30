using HelloML.Model;
using Microsoft.Extensions.ML;

namespace HelloML.App.Application
{
    public class PlayGolfPredictionService
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> EnginePool;

        public PlayGolfPredictionService(PredictionEnginePool<ModelInput, ModelOutput> enginePool)
        {
            EnginePool = enginePool;
        }

        public ModelOutput Predict(ModelInput input)
        {
            return EnginePool.Predict(input);
        }
    }
}
