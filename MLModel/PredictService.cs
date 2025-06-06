using Microsoft.ML;

public class PredictService
{
    private readonly PredictionEngine<ModelInput, ModelOutput> _predictionEngine;

    public PredictService()
    {
        var mlContext = new MLContext();
        var pipeline = mlContext.Transforms.Concatenate("Features", "NivelAgua", "Frequencia")
            .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

        var data = mlContext.Data.LoadFromTextFile<ModelInput>("MLModel/dados.csv", hasHeader: true, separatorChar: ',');
        var model = pipeline.Fit(data);

        _predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
    }

    public ModelOutput Predict(float nivelAgua, float frequencia)
    {
        var input = new ModelInput { NivelAgua = nivelAgua, Frequencia = frequencia };
        return _predictionEngine.Predict(input);
    }

    internal object PreverEnchente(double nivelDaAgua)
    {
        throw new NotImplementedException();
    }
}
