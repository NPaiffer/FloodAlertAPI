using Microsoft.ML.Data;

namespace FloodAlertAPI.ML;

public class FloodPrediction
{
    [ColumnName("PredictedLabel")]
    public bool Enchente { get; set; }

    public float Probability { get; set; }
    public float Score { get; set; }
}
