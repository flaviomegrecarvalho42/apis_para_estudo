using ParaEstudoApi.DTO.Pagination;
using System.Collections.Generic;

namespace ParaEstudoApi.DTO.UploadFiles.Response
{
    /// <summary>
    /// Classe DTO de response (retorno) criada para enviar os arquivos de upload para o endpoint.
    /// </summary>
    public class GetUploadFilesResponseDto
    {
        /// <summary>
        /// Lista dos arquivos do upload.
        /// </summary>
        public List<UploadFilesResponseDto> UploadFiles { get; set; }

        /// <summary>
        /// Dados da paginação para o endpoint (total de páginas, total de registros e quantidade de registros por página).
        /// </summary>
        public PaginationResponseDto Pagination { get; set; }
    }
}
