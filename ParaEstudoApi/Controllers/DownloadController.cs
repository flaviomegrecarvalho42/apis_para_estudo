using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ParaEstudoApi.BLL.Interfaces;
using ParaEstudoApi.DTO.DownloadFile.Request;
using ParaEstudoApi.DTO.DownloadFile.Response;
using ParaEstudoApi.DTO.Error;
using ParaEstudoApi.Util.Exceptions;
using ParaEstudoApi.Util.Constants;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ParaEstudoApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly IDownloadFilesBll _downloadFilesBll;

        public DownloadController(IDownloadFilesBll downloadFilesBll)
        {
            _downloadFilesBll = downloadFilesBll ?? throw new ArgumentNullException(nameof(downloadFilesBll));
        }

        #region Exemplos Acessando a Base de Dados (realizando consulta no SQL Server)

        /// <summary>
        /// Retornar a lista de arquivos para download de acordo com os filtros informados.
        /// As informações destes arquivos estarão salva na Base de Dados.
        /// Neste endpoint será utilizado o PredicateBuilder (classe para realização de consultas do Repository).
        /// </summary>
        /// <param name="filterDownloadFileRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetDownloadFilesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDownloadFiles([FromQuery] FilterDownloadFilesRequestDto filterDownloadFileRequestDto)
        {
            var response = await _downloadFilesBll.GetAllDownloadFilesAsync(filterDownloadFileRequestDto).ConfigureAwait(false);

            return StatusCode(response.StatusCode, response.Content);
        }

        /// <summary>
        /// Realiza o download do arquivo de acordo com o id informado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ById/{id}")]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status412PreconditionFailed)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDownloadFilesById([FromRoute] int id)
        {
            var response = await _downloadFilesBll.GetDownloadFilesByIdAsync(id).ConfigureAwait(false);

            return File(response.Content.Memory, response.Content.ExtensionType, response.Content.FileName);
        }

        #endregion

        #region Exemplo Simples sem acesso a Base de dados

        /// <summary>
        /// Realizar o Download de arquivo de acordo com o nome do arquivo.
        /// Exemplos simples, sem consulta na base de dados.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByName/{fileName}")]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDownloadFileByName([FromRoute] string fileName)
        {
            if (fileName == null)
                throw new ParaEstudoApiException(StatusCodes.Status400BadRequest, "Nome do arquivo não informado.");

            var path = Path.Combine(Constants.PathDownloadArquivos, fileName);

            if (!System.IO.File.Exists(path))
                throw new ParaEstudoApiException(StatusCodes.Status404NotFound, "Arquivo não encontrado.");

            var memory = new MemoryStream();
            using var stream = new FileStream(path, FileMode.Open);
            await stream.CopyToAsync(memory);

            memory.Position = 0;

            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        #endregion

        /// <summary>
        /// Retorna uma string com o tipo de extensão do arquivos.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetContentType(string fileName)
        {
            string strcontentType = "application/octetstream";
            string ext = Path.GetExtension(fileName).ToLower();
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(ext);

            if (registryKey != null && registryKey.GetValue("Content Type") != null)
                strcontentType = registryKey.GetValue("Content Type").ToString();

            return strcontentType;
        }
    }
}
