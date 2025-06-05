using Microsoft.EntityFrameworkCore;
using FloodAlertAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AreaDeRisco> AreasDeRisco { get; set; }
    public DbSet<RegistroDeEnchente> RegistrosDeEnchente { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
}
