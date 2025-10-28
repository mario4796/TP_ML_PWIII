using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TP_ML_PWIII.Data.Entidades;

public partial class Interaccione
{
    [Key]
    public int IdInteraccion { get; set; }

    public int IdUsuario { get; set; }

    public int IdCancion { get; set; }

    public float? Valor { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? TipoInteraccion { get; set; }

    public DateTime? FechaInteraccion { get; set; }

    [ForeignKey("IdCancion")]
    [InverseProperty("Interacciones")]
    public virtual Cancione IdCancionNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Interacciones")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
