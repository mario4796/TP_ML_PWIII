using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TP_ML_PWIII.Data.Entidades;

public partial class Recomendacione
{
    [Key]
    public int IdRecomendacion { get; set; }

    public int IdUsuarioFuente { get; set; }

    public int IdUsuarioRecomendado { get; set; }

    public float? PuntajeSimilitud { get; set; }

    public DateTime? FechaCalculo { get; set; }

    [ForeignKey("IdUsuarioFuente")]
    [InverseProperty("RecomendacioneIdUsuarioFuenteNavigations")]
    public virtual Usuario IdUsuarioFuenteNavigation { get; set; } = null!;

    [ForeignKey("IdUsuarioRecomendado")]
    [InverseProperty("RecomendacioneIdUsuarioRecomendadoNavigations")]
    public virtual Usuario IdUsuarioRecomendadoNavigation { get; set; } = null!;
}
