using Microsoft.ML;

namespace FloodAlertAPI.ML;

public class FloodPredictionService
{
    private readonly PredictionEngine<FloodData, FloodPrediction> _predictionEngine;

    public FloodPredictionService()
    {
        var mlContext = new MLContext();
        var model = mlContext.Model.Load("ML/Model.zip", out var _);
        _predictionEngine = mlContext.Model.CreatePredictionEngine<FloodData, FloodPrediction>(model);
    }

    public FloodPrediction Predict(FloodData data)
    {
        return _predictionEngine.Predict(data);
    }
}
