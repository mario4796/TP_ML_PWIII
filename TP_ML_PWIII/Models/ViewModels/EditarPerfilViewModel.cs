using System.ComponentModel.DataAnnotations;

namespace TP_ML_PWIII.Web.Models.ViewModels
{
 public class EditarPerfilViewModel
 {
 public int IdUsuario { get; set; }

 [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
 [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 40 caracteres.")]
 public string NombreUsuario { get; set; }


 [DataType(DataType.Password)]
 [StringLength(100, MinimumLength =6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
 public string? Password { get; set; }

 [DataType(DataType.Password)]
 [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
 public string? ConfirmarPassword { get; set; }
 }
}
