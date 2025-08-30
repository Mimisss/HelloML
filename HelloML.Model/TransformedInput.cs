using Microsoft.ML.Data;

namespace HelloML.Model
{
    // Used to inspect the transformed nominal input
    // data to binary sparce vectors
    public class TransformedInput
    {
        [ColumnName("Features")]
        public VBuffer<float> Features { get; set; }

        [ColumnName("Label")]
        public bool Label { get; set; }
    }
}
