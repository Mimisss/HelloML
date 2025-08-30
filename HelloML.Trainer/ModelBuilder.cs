using HelloML.Model;
using Microsoft.ML;
using System;
using System.Linq;

namespace HelloML.Trainer
{
    internal class ModelBuilder
    {
        private static readonly MLContext Context = new MLContext();        
        private static string _baseModelsRelativePath = @"Output\";

        private static readonly string _trainDataRelativePath = @"Data\weather.nominal.txt";
        private static readonly string _trainDataPath = DebugHelper.GetAbsolutePath(_trainDataRelativePath);

        private static string _modelRelativePath = @$"{_baseModelsRelativePath}\weather.nominal.zip";
        private static readonly string _modelPath = DebugHelper.GetAbsolutePath(_modelRelativePath);
        
        public static void Execute()
        {
            // 1. Load the data            
            var dataView = Context.Data.LoadFromTextFile<ModelInput>(
                _trainDataPath, separatorChar: ',', hasHeader: true);

            // Split data for training and testing
            var dataSplit = Context.Data.TrainTestSplit(dataView, testFraction: 0.2);
            var trainingData = dataSplit.TrainSet;
            var testData = dataSplit.TestSet;

            // 2. Prepare the data (featurize)
            // ML.NET algorithms require numerical input.            
            var dataPrepPipeline = ComposeDataProcessingPipeline();

            // 3. Train, evaluate and save the model
            Console.WriteLine("Training the model...");
            TrainEvaluateSaveModel(trainingData, testData, dataPrepPipeline, _modelPath, true);
        }

        #region PRIVATE
        private static IEstimator<ITransformer> ComposeDataProcessingPipeline()
        {            
            // We use One-Hot Encoding to convert nominal features to numerical vectors.
            var pipeline = Context.Transforms.Conversion.MapValueToKey(inputColumnName: "Outlook", outputColumnName: "OutlookKey")
                .Append(Context.Transforms.Conversion.MapValueToKey(inputColumnName: "Temperature", outputColumnName: "TemperatureKey"))
                .Append(Context.Transforms.Conversion.MapValueToKey(inputColumnName: "Humidity", outputColumnName: "HumidityKey"))
                .Append(Context.Transforms.Conversion.MapValueToKey(inputColumnName: "Windy", outputColumnName: "WindyKey"))                
                .Append(Context.Transforms.Categorical.OneHotEncoding(inputColumnName: "OutlookKey", outputColumnName: "OutlookEncoded"))
                .Append(Context.Transforms.Categorical.OneHotEncoding(inputColumnName: "TemperatureKey", outputColumnName: "TemperatureEncoded"))
                .Append(Context.Transforms.Categorical.OneHotEncoding(inputColumnName: "HumidityKey", outputColumnName: "HumidityEncoded"))
                .Append(Context.Transforms.Categorical.OneHotEncoding(inputColumnName: "WindyKey", outputColumnName: "WindyEncoded"))                
                // Concatenate all feature columns into a single "Features" column
                .Append(Context.Transforms.Concatenate("Features", "OutlookEncoded", "TemperatureEncoded", "HumidityEncoded", "WindyEncoded"));

            return pipeline;
        }

        private static void TrainEvaluateSaveModel(IDataView trainingDataView, IDataView testDataView, IEstimator<ITransformer> dataProcessPipeline, string modelPath, bool inspectTransformation = false)
        {
            // Set the training algorithm
            var trainer = Context
                .BinaryClassification
                .Trainers
                //.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features");
                .LbfgsLogisticRegression();            

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            // Train the model            
            var trainedModel = trainingPipeline.Fit(trainingDataView);
                                    
            if (inspectTransformation)
            {
                // Inspect the Feature sparce vectors for debugging
                var transformedData = trainedModel.Transform(trainingDataView);
                ConsoleHelper.PrintTransformedData(Context, transformedData);
            }

            // Evaluate the model and show accuracy stats
            Console.WriteLine("Evaluating the model...");
            var predictions = trainedModel.Transform(testDataView);

            var metrics = Context.BinaryClassification.Evaluate(predictions, "Label");
            ConsoleHelper.PrintBinaryClassificationMetrics(trainer.ToString(), metrics);

            // Save the trained model to a .ZIP file
            Context.Model.Save(trainedModel, trainingDataView.Schema, modelPath);
            
            Console.WriteLine("The model is saved to {0}\n\n\n", modelPath);
        }
        #endregion
    }
}
