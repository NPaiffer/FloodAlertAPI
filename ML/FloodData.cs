using Microsoft.ML.Data;

namespace FloodAlertAPI.ML;

public class FloodData
{
    [LoadColumn(0)]
    public float NivelAgua { get; set; }

    [LoadColumn(1)]
    public float Precipitacao { get; set; }

    [LoadColumn(2)]
    public float Umidade { get; set; }

    [LoadColumn(3)]
    public bool Enchente { get; set; }
}
