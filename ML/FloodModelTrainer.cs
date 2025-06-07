using Microsoft.ML;
using Microsoft.ML.Trainers;

namespace FloodAlertAPI.ML;

public class FloodModelTrainer
{
    private static readonly string _modelPath = "ML/Model.zip";

    public static void TreinarModelo()
    {
        var mlContext = new MLContext();

        var dataPath = "ML/flood-data.csv";

        IDataView data = mlContext.Data.LoadFromTextFile<FloodData>(dataPath, hasHeader: true, separatorChar: ',');

        var pipeline = mlContext.Transforms.Concatenate("Features", nameof(FloodData.NivelAgua), nameof(FloodData.Precipitacao), nameof(FloodData.Umidade))
            .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Enchente", featureColumnName: "Features"));

        var model = pipeline.Fit(data);

        mlContext.Model.Save(model, data.Schema, _modelPath);
    }
}
