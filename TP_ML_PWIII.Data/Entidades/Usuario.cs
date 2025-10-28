using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TP_ML_PWIII.Data.Entidades;

[Table("Usuario")]
[Index("SpotifyId", Name = "UQ__Usuario__958BCEFD53AEA439", IsUnique = true)]
[Index("Email", Name = "UQ__Usuario__A9D105341CC6F1E2", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SpotifyId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? NombreUsuario { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? Email { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? TokenAcceso { get; set; }

    public string? TokenRefresco { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Interaccione> Interacciones { get; set; } = new List<Interaccione>();

    [InverseProperty("IdUsuarioFuenteNavigation")]
    public virtual ICollection<Recomendacione> RecomendacioneIdUsuarioFuenteNavigations { get; set; } = new List<Recomendacione>();

    [InverseProperty("IdUsuarioRecomendadoNavigation")]
    public virtual ICollection<Recomendacione> RecomendacioneIdUsuarioRecomendadoNavigations { get; set; } = new List<Recomendacione>();
}
