using Microsoft.ML.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloML.Model
{
    public class TransformedInput
    {
        [ColumnName("Features")]
        public VBuffer<float> Features { get; set; }

        [ColumnName("Label")]
        public bool Label { get; set; }
    }
}
