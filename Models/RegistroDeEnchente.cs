namespace FloodAlertAPI.Models;

public class RegistroDeEnchente
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public double NivelDaAgua { get; set; }
    public int AreaDeRiscoId { get; set; }
    public AreaDeRisco? AreaDeRisco { get; set; }
}
