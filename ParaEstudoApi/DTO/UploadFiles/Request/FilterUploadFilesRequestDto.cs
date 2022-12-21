using System;

namespace ParaEstudoApi.DTO.UploadFiles.Request
{
    /// <summary>
    /// Classe DTO de entrada criada para passar os filtros da consulta dos arquivos de upload.
    /// </summary>
    public class FilterUploadFilesRequestDto
    {
        /// <summary>
        /// Data inicial uilizada no filtro da litagem de arquivos (referente a data de gravação do arquivo de remessa).
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Data final uilizada no filtro da litagem de arquivos (referente a data de gravação do arquivo de remessa).
        /// </summary>
        public DateTime? DateFrom { get; set; }
    }
}
