namespace FloodAlertAPI.Services;

public class PredictService
{
    public string PreverEnchente(double nivelAgua)
    {
        return nivelAgua > 5.0 ? "Risco de Enchente Alto" : "Risco de Enchente Baixo";
    }
}
