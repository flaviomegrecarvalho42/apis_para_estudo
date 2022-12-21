namespace ParaEstudoApi.DTO.UploadFiles.Request
{
    /// <summary>
    /// Classe DTO de entrada criada para passar os dados da imagem que será realizado o upload.
    /// </summary>
    public class UploadImageRequestDto
    {
        /// <summary>
        /// Imagem convertida em base64.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Tipo de extensão da imagem.
        /// </summary>
        public string Type { get; set; }
    }
}
