using ParaEstudoApi.DTO.UploadFiles.Request;
using ParaEstudoApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParaEstudoApi.DAL.Interfaces
{
    public interface IUploadFilesRepository
    {
        /// <summary>
        /// Retorna o total de arquivos. Esta informação será retornada no response para o endpoint.
        /// </summary>
        /// <param name="filterUploadFilesRequestDto"></param>
        /// <returns></returns>
        Task<int> GetTotalUploadFilesRecordsAsync(FilterUploadFilesRequestDto filterUploadFilesRequestDto);

        /// <summary>
        /// Retorna uma lista de arquivos com base nos filtros do endpoint.
        /// </summary>
        /// <param name="filterUploadFilesRequestDto"></param>
        /// <returns></returns>
        Task<List<UploadFile>> GetListUploadFilesByFilterAsync(FilterUploadFilesRequestDto filterUploadFilesRequestDto);

        /// <summary>
        /// Salva uma lista de informações de arquivos enviados.
        /// </summary>
        /// <param name="saveAllFiles"></param>
        /// <returns></returns>
        Task SaveAllAsync(IList<UploadFile> saveAllFiles);

        /// <summary>
        /// Retorna os dados do arquivo para que seja realizado o upload.
        /// </summary>
        /// <param name="hashcode"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        Task<UploadFile> GetDataUploadFilesHashcodeDocument(string hashcode, string document);
    }
}
