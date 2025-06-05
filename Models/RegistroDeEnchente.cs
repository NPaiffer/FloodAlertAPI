namespace FloodAlertAPI.Models;

public class RegistroDeEnchente
{
    public int Id { get; set; }
    public int AreaDeRiscoId { get; set; }
    public AreaDeRisco AreaDeRisco { get; set; }
    public DateTime DataOcorrencia { get; set; }
    public float NivelDeAgua { get; set; }
    public string Descricao { get; set; }
    public string DanosRegistrados { get; set; }
}
