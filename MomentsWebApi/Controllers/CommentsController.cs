using Microsoft.AspNetCore.Mvc;
using MomentsWebApi.Data;
using MomentsWebApi.Mappings;
using MomentsWebApi.Models;
using MomentsWebApi.ViewModels;

namespace MomentsWebApi.Controllers
{
    [Route("api/moments/")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        [HttpPost("{id:int}/comments")]
        public async Task<ActionResult<Response<CommentViewModel>>> Post([FromServices] AppDbContext context,
                                        [FromBody] CreateCommentViewModel commentViewModel,
                                        [FromRoute] int id)
        {
            var moment = await context.Moments.FindAsync(id);

            if (moment is null)
                return NotFound();

            var comment = commentViewModel.ConverterViewModelParaComment();

            comment.MomentId = moment.Id;

            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();

            var commentResultViewModel = comment.ConverterCommentParaViewModel();

            var response = new Response<CommentViewModel>
            {
                Message = "Comentário adicionado com sucesso",
                Data = commentResultViewModel
            };

            return Ok(response);
        }
    }
}
