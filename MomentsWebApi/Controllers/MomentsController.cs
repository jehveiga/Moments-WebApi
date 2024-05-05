using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MomentsWebApi.Data;
using MomentsWebApi.Mappings;
using MomentsWebApi.Models;
using MomentsWebApi.Models.Enums;
using MomentsWebApi.Services;
using MomentsWebApi.ViewModels;

namespace MomentsWebApi.Controllers
{
    [Route("api/moments")]
    [ApiController]
    public class MomentsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MomentViewModel>>> GetAsync([FromServices] AppDbContext context)
        {
            var moments = await context.Moments.ToListAsync();

            var momentsViewModel = moments.ConverterMomentsParaViewModel();

            return Ok(momentsViewModel);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MomentViewModel>> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var moment = await context.Moments.FindAsync(id);

            if (moment is null)
                return NotFound();

            var momentViewModel = moment.ConverterMomentParaViewModel();

            var response = new Response<MomentViewModel>
            {
                Message = "",
                Data = momentViewModel
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Response<ResponseMomentViewModel>>> Post([FromServices] AppDbContext context,
                                              [FromServices] IUploadService uploadService,
                                              [FromForm] EditMomentViewModel createMoment)
        {
            string nameImage = string.Empty;

            if (createMoment.Image is not null && createMoment.Image.Length > 0)
            {
                (RetornoStatusUploadArquivo statusRetorno, string statusMessage) =
                    await uploadService.ArquivoUploadAsync(createMoment.Image);

                if (statusRetorno == RetornoStatusUploadArquivo.Success)
                {
                    nameImage = statusMessage ?? string.Empty;
                }
                else
                {
                    return BadRequest(statusMessage);
                }
            }


            var moment = createMoment.ConverterViewModelParaMoment();
            moment.Image = nameImage;

            await context.Moments.AddAsync(moment);
            await context.SaveChangesAsync();

            var responseMoment = moment.ConverterMomentParaResponseViewModel();

            var response = new Response<ResponseMomentViewModel>
            {
                Message = "Momento criado com sucesso",
                Data = responseMoment
            };

            var location = Url.Action(nameof(GetByIdAsync), new { id = responseMoment.Id }) ?? $"/{responseMoment.Id}";
            return Created(location, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Response<MomentViewModel>>> Put([FromServices] AppDbContext context,
                                                                        [FromServices] IUploadService uploadService,
                                                                        [FromForm] EditMomentViewModel editmoment,
                                                                        int id)
        {


            var momentDb = await context.Moments.FindAsync(id);

            if (momentDb is null)
                return NotFound();

            string nameImage = string.Empty;

            if (editmoment.Image is not null && editmoment.Image.Length > 0)
            {
                (RetornoStatusUploadArquivo statusRetorno, string statusMessage) =
                    await uploadService.ArquivoUploadAsync(editmoment.Image);

                if (statusRetorno == RetornoStatusUploadArquivo.Success)
                {
                    nameImage = statusMessage ?? string.Empty;
                }
                else
                {
                    return BadRequest(statusMessage);
                }
            }

            momentDb.Title = editmoment.Title;
            momentDb.Description = editmoment.Description;
            momentDb.Image = nameImage;

            momentDb.AddDateUpdate();

            context.Entry(momentDb).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var responseMoment = momentDb.ConverterMomentParaViewModel();

            var response = new Response<MomentViewModel>
            {
                Message = "Momento criado com sucesso",
                Data = responseMoment
            };

            return Ok(response);
        }
    }
}
