using MomentsWebApi.Models;
using MomentsWebApi.ViewModels;

namespace MomentsWebApi.Mappings
{
    public static class MappingMoment
    {
        public static Moment ConverterViewModelParaMoment(this CreateMomentViewModel createMoment)
        {
            return new Moment
            {
                Title = createMoment.Title,
                Description = createMoment.Description,
                Image = createMoment.Image,
            };
        }
    }
}
