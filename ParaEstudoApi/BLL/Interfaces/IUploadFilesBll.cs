using ParaEstudoApi.DTO.UploadFiles.Response;
using ParaEstudoApi.DTO.UploadFiles.Request;
using ParaEstudoApi.Util.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ParaEstudoApi.BLL.Interfaces
{
    public interface IUploadFilesBll
    {
        /// <summary>
        /// Retorna uma lista de arquivos existentes na pasta com base nos filtros do endpoint.
        /// </summary>
        /// <param name="filterUploadFilesRequestDto"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetUploadFilesResponseDto>> GetAllUploadFilesAsync(FilterUploadFilesRequestDto filterUploadFilesRequestDto);

        /// <summary>
        /// Recebe uma lista de arquivos para serem salvas na pasta.
        /// </summary>
        /// <param name="listFiles"></param>
        /// <returns></returns>
        Task<ServiceResponse<IList<UploadFilesResponseDto>>> PostUploadFilesAsync(IList<IFormFile> listFiles);
    }
}
