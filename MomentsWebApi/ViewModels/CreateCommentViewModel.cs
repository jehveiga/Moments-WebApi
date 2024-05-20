using System.ComponentModel.DataAnnotations;

namespace MomentsWebApi.ViewModels
{
    public class CreateCommentViewModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Text { get; set; } = string.Empty;
    }
}
