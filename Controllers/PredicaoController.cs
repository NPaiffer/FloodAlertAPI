using FloodAlertAPI.ML;
using Microsoft.AspNetCore.Mvc;

namespace FloodAlertAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PredicaoController : ControllerBase
{
    private readonly FloodPredictionService _predictionService;

    public PredicaoController()
    {
        _predictionService = new FloodPredictionService();
    }

    [HttpPost]
    public ActionResult<FloodPrediction> Prever(FloodData data)
    {
        var resultado = _predictionService.Predict(data);
        return Ok(resultado);
    }
}
