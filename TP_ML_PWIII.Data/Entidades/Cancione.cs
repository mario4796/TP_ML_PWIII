using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TP_ML_PWIII.Data.Entidades;

[Index("SpotifyTrackId", Name = "UQ__Cancione__3C41F0C7DB760B26", IsUnique = true)]
public partial class Cancione
{
    [Key]
    public int IdCancion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SpotifyTrackId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Titulo { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string? Artista { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? UrlPreview { get; set; }

    public int? Popularidad { get; set; }

    public float? Danceability { get; set; }

    public float? Energy { get; set; }

    public float? Valence { get; set; }

    public float? Acousticness { get; set; }

    public float? Tempo { get; set; }

    public float? Instrumentalness { get; set; }

    public string? Letras { get; set; }

    [InverseProperty("IdCancionNavigation")]
    public virtual ICollection<Interaccione> Interacciones { get; set; } = new List<Interaccione>();
}
