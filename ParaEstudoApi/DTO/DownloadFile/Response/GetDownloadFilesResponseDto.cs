using ParaEstudoApi;
using ParaEstudoApi.DTO;
using ParaEstudoApi.DTO.DownloadFile;
using ParaEstudoApi.DTO.DownloadFile.Request;
using ParaEstudoApi.DTO.DownloadFile.Response;
using ParaEstudoApi.DTO.Pagination;
using System.Collections.Generic;

namespace ParaEstudoApi.DTO.DownloadFile.Response
{
    /// <summary>
    /// Classe DTO de resposta para os endpoints de Download, que retorna a listagem dos arquivos para download.
    /// </summary>
    public class GetDownloadFilesResponseDto
    {
        /// <summary>
        /// Lista dos arquivos para download.
        /// </summary>
        public List<DataDownloadFilesResponseDto> DownloadFiles { get; set; }

        /// <summary>
        /// Dados da paginação para o endpoint (total de páginas, total de registros e quantidade de registros por página).
        /// </summary>
        public PaginationResponseDto Pagination { get; set; }
    }
}
