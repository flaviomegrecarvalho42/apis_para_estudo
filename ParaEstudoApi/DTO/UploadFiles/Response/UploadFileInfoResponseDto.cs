namespace ParaEstudoApi.DTO.UploadFiles.Response
{
    /// <summary>
    /// Classe DTO de response (retorno) criada para enviar informações do arquivo de upload para o endpoint.
    /// </summary>
    public class UploadFileInfoResponseDto
    {
        /// <summary>
        /// Propriedade que será utilizada para limitar o tamanho do arquivo de upload.
        /// O tamanho será calculado em Kbytes
        /// </summary>
        public long UploadFileLimitSize { get; set; }
    }
}
