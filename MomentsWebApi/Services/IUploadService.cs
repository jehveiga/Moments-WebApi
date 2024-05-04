using MomentsWebApi.Models.Enums;

namespace MomentsWebApi.Services
{
    public interface IUploadService
    {
        Task<(RetornoStatusUploadArquivo, string)> ArquivoUploadAsync(IFormFile arquivo);
    }

}
