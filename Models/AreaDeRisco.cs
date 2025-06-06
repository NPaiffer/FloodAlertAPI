namespace FloodAlertAPI.Models;

public class AreaDeRisco
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Localizacao { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}
