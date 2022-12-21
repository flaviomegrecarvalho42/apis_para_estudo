using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParaEstudoApi.BLL;
using ParaEstudoApi.BLL.Interfaces;
using ParaEstudoApi.DTO.DownloadFile.Request;
using ParaEstudoApi.DTO.DownloadFile.Response;
using ParaEstudoApi.DTO.Error;
using ParaEstudoApi.DTO.UploadFiles;
using ParaEstudoApi.DTO.UploadFiles.Request;
using ParaEstudoApi.DTO.UploadFiles.Response;
using ParaEstudoApi.Util.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParaEstudoApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadFilesBll _uploadFilesBll;

        public UploadController(IUploadFilesBll uploadFilesBll)
        {
            _uploadFilesBll = uploadFilesBll ?? throw new ArgumentNullException(nameof(uploadFilesBll));
        }

        #region Exemplos Acessando a Base de Dados (realizando consulta no SQL Server)

        /// <summary>
        /// Retornar a lista de arquivos existentes na pasta de arquivos Upload de acordo com os filtros informados.
        /// As informações destes arquivos estarão salva na Base de Dados.
        /// Neste endpoint será utilizado o PredicateBuilder (classe para realização de consultas do Repository).
        /// </summary>
        /// <param name="GetUploadFilesResponseDto"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetUploadFilesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUploadFiles([FromQuery] FilterUploadFilesRequestDto filterUploadFilesRequestDto)
        {
            var response = await _uploadFilesBll.GetAllUploadFilesAsync(filterUploadFilesRequestDto).ConfigureAwait(false);

            return StatusCode(response.StatusCode, response.Content);
        }

        /// <summary>
        /// Realiza o upload do arquivo para a pasta.
        /// Neste endpoint o upload é o de arquivos de remessa CNAB400 - Santander.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(UploadFilesResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostUploadFile([FromForm] IFormFile file)
        {
            var response = await _uploadFilesBll.PostUploadFilesAsync(new List<IFormFile> { file }).ConfigureAwait(false);

            return Created("uploadFile", response.Content.FirstOrDefault());
        }

        #endregion

        #region Exemplo Simples sem acesso a Base de dados

        /// <summary>
        /// Upload de uma imagem, passando um DTO com uma string de imagem base64 e a extensão da imagem.
        /// </summary>
        /// <param name="uploadImageRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FileBase64")]
        public IActionResult PostUploadImage([FromBody] UploadImageRequestDto uploadImageRequestDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uploadImageRequestDto.Image) || string.IsNullOrWhiteSpace(uploadImageRequestDto.Type))
                    return NoContent();

                byte[] bytes = Convert.FromBase64String(uploadImageRequestDto.Image);
                
                Image image;
                var path = Path.Combine(Constants.PathUploadArquivos, string.Concat(Guid.NewGuid().ToString(), ".", uploadImageRequestDto.Type));

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (image = Image.FromStream(ms))
                    {
                        image.Save(path, ImageFormat.Jpeg);
                    }
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Upload de uma lista de imagens, utilizando o IFormFile.
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FileList")]
        public async Task<IActionResult> PostUploadListFiles(List<IFormFile> files)
        {
            try
            {
                if (!files.Any())
                    return NoContent();

                var result = new List<FileResponseDto>();

                foreach (var file in files)
                {
                    var fileName = string.Concat(file.FileName.TakeWhile((c) => c != '.'));
                    var fileExtension = file.FileName.Substring(file.FileName.IndexOf("."));
                    var fileNameComplete = string.Concat(fileName, DateTime.Now.ToString("_yyyyMMddHHmmss"), fileExtension);
                    var path = Path.Combine(Constants.PathUploadArquivos, fileNameComplete);
                    var stream = new FileStream(path, FileMode.Create);

                    await file.CopyToAsync(stream);

                    result.Add(new FileResponseDto() { Name = fileNameComplete, Length = file.Length });
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion
    }
}
