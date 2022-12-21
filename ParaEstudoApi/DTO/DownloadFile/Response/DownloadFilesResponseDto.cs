using System.IO;

namespace ParaEstudoApi.DTO.DownloadFile.Response
{
    /// <summary>
    /// Classe DTO de resposta para os endpoints de Download, que retorna o arquivo de download.
    /// </summary>
    public class DownloadFilesResponseDto
    {
        /// <summary>
        /// Dados do arquivo (em byte), que serão baixados no download.
        /// </summary>
        public MemoryStream Memory { get; set; }

        /// <summary>
        /// Extensão do arquivo gravado na base de dados.
        /// </summary>
        public string ExtensionType { get; set; }

        /// <summary>
        /// Nome do arquivo gravado na base de dados.
        /// </summary>
        public string FileName { get; set; }
    }
}
