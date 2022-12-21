using System;

namespace ParaEstudoApi.DTO.DownloadFile.Request
{
    /// <summary>
    /// Classe DTO de entrada criada para passar os filtros (parâmentros) dos endpoints de Download.
    /// </summary>
    public class FilterDownloadFilesRequestDto
    {
        /// <summary>
        /// Data inicial utilizada no filtro da litagem de arquivos para download (referente a data de geração do arquivo).
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Data final utilizada no filtro da litagem de arquivos para download (referente a data de geração do arquivo).
        /// </summary>
        public DateTime? DateTo { get; set; }
    }
}
