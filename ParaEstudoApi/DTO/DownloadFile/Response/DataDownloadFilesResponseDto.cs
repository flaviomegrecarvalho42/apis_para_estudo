namespace ParaEstudoApi.DTO.DownloadFile.Response
{
    /// <summary>
    /// Classe DTO de resposta para os endpoints de Download, que retorna os dados do arquivo de download.
    /// </summary>
    public class DataDownloadFilesResponseDto : BaseResponse
    {
        /// <summary>
        /// Identificador do arquivo.
        /// </summary>
        public int Id { get; set; }
    }
}
