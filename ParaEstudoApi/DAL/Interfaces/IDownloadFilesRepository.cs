using ParaEstudoApi.DTO.DownloadFile.Request;
using ParaEstudoApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParaEstudoApi.DAL.Interfaces
{
    public interface IDownloadFilesRepository
    {
        /// <summary>
        /// Retorna o total de arquivos de para download. Esta informação será retornada no response.
        /// </summary>
        /// <param name="filterDownloadFileRequestDto"></param>
        /// <returns></returns>
        Task<int> GetTotalDownloadFilesRecordsAsync(FilterDownloadFilesRequestDto filterDownloadFileRequestDto);

        /// <summary>
        /// Retorna uma lista de arquivos para download com base nos filtros.
        /// </summary>
        /// <param name="filterDownloadFileRequestDto"></param>
        /// <returns></returns>
        Task<List<DownloadFile>> GetListDownloadFilesByFilterAsync(FilterDownloadFilesRequestDto filterDownloadFileRequestDto);

        /// <summary>
        /// Retorna os dados do arquivo para que seja realizado o download.
        /// </summary>
        /// <param name="downloadFileId"></param>
        /// <returns></returns>
        Task<DownloadFile> GetDataDownloadFilesIdAsync(int downloadFileId);
    }
}
