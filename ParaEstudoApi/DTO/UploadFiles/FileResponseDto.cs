namespace ParaEstudoApi.DTO.UploadFiles
{
    /// <summary>
    /// Classe DTO de retorno, informando o nome e o tamanho do arquivo do upload.
    /// </summary>
    public class FileResponseDto
    {
        /// <summary>
        /// Tamanho do arquivo.
        /// </summary>
        public long Length { get; set; }
        
        /// <summary>
        /// Nome do arquivo.
        /// </summary>
        public string Name { get; set; }
    }
}
