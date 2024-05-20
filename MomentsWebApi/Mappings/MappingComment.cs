using MomentsWebApi.Models;
using MomentsWebApi.ViewModels;

namespace MomentsWebApi.Mappings
{
    public static class MappingComment
    {
        public static Comment ConverterViewModelParaComment(this CreateCommentViewModel createComment)
        {
            return new Comment
            {
                UserName = createComment.UserName,
                Text = createComment.Text,
            };
        }
    }
}
