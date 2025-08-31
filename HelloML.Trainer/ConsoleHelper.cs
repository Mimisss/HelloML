using HelloML.Model;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;

namespace HelloML.Trainer
{
    public static class ConsoleHelper
    {
        public static void PrintTransformedData(MLContext context, IDataView dataView)
        {
            Console.WriteLine("\n\nSparce vectors of transformed data:");
            foreach (var row in context.Data.CreateEnumerable<TransformedInput>(dataView, reuseRowObject: false))
            {
                var featuresString = "";
                var features = row.Features;

                // Get the indices and values from the VBuffer
                var indices = features.GetIndices().ToArray();
                var values = features.GetValues().ToArray();

                // Reconstruct a more readable string for the sparse vector
                for (int i = 0; i < indices.Length; i++)
                {
                    featuresString += $"[{indices[i]}:{values[i]}] ";
                }

                Console.WriteLine($"Label: {row.Label} | Features Vector (Sparse): {featuresString}");
            }
            Console.WriteLine("\n\n");
        }

        public static void PrintBinaryClassificationMetrics(string name, CalibratedBinaryClassificationMetrics metrics)
        {
            // Print binary classification evaluation metrics
            Console.WriteLine($"Model Evaluation Metrics for {name}:");
            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"Area Under Curve (AUC): {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"F1 Score: {metrics.F1Score:P2}");
        }
    }
}
