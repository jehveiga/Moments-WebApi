using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MomentsWebApi.Data;
using MomentsWebApi.Mappings;
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

            return Ok(momentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] AppDbContext context, [FromBody] CreateMomentViewModel createMoment)
        {
            var moment = createMoment.ConverterViewModelParaMoment();


            await context.Moments.AddAsync(moment);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = moment.Id }, moment);
        }

    }
}
