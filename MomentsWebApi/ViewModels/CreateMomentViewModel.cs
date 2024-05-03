using System.ComponentModel.DataAnnotations;

namespace MomentsWebApi.ViewModels
{
    public record CreateMomentViewModel
    {
        [Required(ErrorMessage = "Campo é requerido")]
        [MinLength(3, ErrorMessage = "O campo deve conter mais de 3 caracteres")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Campo é requerido")]
        [MaxLength(200, ErrorMessage = "O campo deve conter menos de 200 caracteres")]
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
