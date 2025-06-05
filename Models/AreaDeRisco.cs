namespace FloodAlertAPI.Models;

public class AreaDeRisco
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string NivelDeRisco { get; set; }
    public string Descricao { get; set; }
}