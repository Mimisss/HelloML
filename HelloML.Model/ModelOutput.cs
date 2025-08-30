using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloML.Model
{
    public class ModelOutput
    {
        [ColumnName("PredictedLabel")]
        public bool PredictedPlay { get; set; }

        public float Score { get; set; }

        public float Probability { get; set; }
    }
}
