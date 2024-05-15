using MomentsWebApi.Models.Enums;

namespace MomentsWebApi.Services
{
    public class UploadService : IUploadService
    {
        private readonly ILogger<UploadService> _logger;
        private readonly IWebHostEnvironment _appEnvironment;

        public UploadService(ILogger<UploadService> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        public async Task<(RetornoStatusUploadArquivo, string)> ArquivoUploadAsync(IFormFile arquivo)
        {

            var extensoesPermitidas = new string[] { ".png", ".jpg", ".jpeg" };
            int tamanhoMaximoPermitido = 1024 * 1024 * 2; // representa 2MB 

            if (arquivo.Length > tamanhoMaximoPermitido)
            {
                var mensagem = $"Arquivo: {arquivo.Name} excede o tamanho máximo permitido.";
                _logger.LogInformation(mensagem);
                return (RetornoStatusUploadArquivo.Failed, mensagem);
            }

            // Combina o caminho relativo da pasta 'wwwroot' com a pasta criada alvo que será salvo a imagem enviada
            var diretorioUpload = Path.Combine(_appEnvironment.WebRootPath, "uploads");

            VerificaDiretorioArquivo(diretorioUpload);

            var arquivoExtensao = Path.GetExtension(arquivo.FileName);

            if (!extensoesPermitidas.Contains(arquivoExtensao))
            {
                var mensagem = $"Arquivo: {arquivo.Name}, tipo de Arquivo não permitido";
                _logger.LogInformation(mensagem);
                return (RetornoStatusUploadArquivo.Failed, mensagem);
            }

            // Altera o nome do arquivo por segurança de não aceitar nomes pelo client
            var nomeArquivoSeguro = $"{Guid.NewGuid()}{arquivoExtensao}";
            // Obtém o caminho do arquivo em wwwwroot
            var path = Path.Combine(diretorioUpload, nomeArquivoSeguro);
            // Cria o arquivo
            await using var fileStream = new FileStream(path, FileMode.Create);
            // Lê e copia para memória
            await arquivo.OpenReadStream().CopyToAsync(fileStream);
            return (RetornoStatusUploadArquivo.Success, nomeArquivoSeguro);
        }

        /// <summary>
        /// Método que será reponposável por verificar se o diretório existe passado
        /// se não existir irá criar
        /// </summary>
        /// <param name="diretorioUpload"></param>
        private void VerificaDiretorioArquivo(string diretorioUpload)
        {
            if (!Directory.Exists(diretorioUpload))
                Directory.CreateDirectory(diretorioUpload);
        }
    }
}
