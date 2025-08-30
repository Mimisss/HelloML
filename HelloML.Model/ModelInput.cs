using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloML.Model
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public string Outlook {  get; set; }

        [LoadColumn(1)]
        public string Temperature { get; set; }

        [LoadColumn(2)]
        public string Humidity { get; set; }

        [LoadColumn(3)]
        public string Windy { get; set; }

        [LoadColumn(4), ColumnName("Label")]
        public bool Play { get; set; }
    }
}
