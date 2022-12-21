using ParaEstudoApi.DTO.DownloadFile.Request;
using ParaEstudoApi.DTO.DownloadFile.Response;
using ParaEstudoApi.Util.Helpers;
using System.Threading.Tasks;

namespace ParaEstudoApi.BLL.Interfaces
{
    public interface IDownloadFilesBll
    {
        /// <summary>
        /// Retorna uma lista de arquivos para download com base nos filtros.
        /// </summary>
        /// <param name="filterDownloadFileRequestDto"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetDownloadFilesResponseDto>> GetAllDownloadFilesAsync(FilterDownloadFilesRequestDto filterDownloadFileRequestDto);

        /// <summary>
        /// Realiza do download do arquivo gravado na pasta.
        /// </summary>
        /// <param name="downloadFileId"></param>
        /// <returns></returns>
        Task<ServiceResponse<DownloadFilesResponseDto>> GetDownloadFilesByIdAsync(int downloadFileId);

        /// <summary>
        /// Retorna os dados do arquivo para que seja realizado o download.
        /// </summary>
        /// <param name="downloadFileId"></param>
        /// <returns></returns>
        Task<ServiceResponse<DataDownloadFilesResponseDto>> GetDataDownloadFilesIdAsync(int downloadFileId);
    }
}
