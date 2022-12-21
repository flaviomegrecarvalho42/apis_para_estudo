namespace ParaEstudoApi.DTO.Error
{
    /// <summary>
    /// DTO que retornará as informações de erro.
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Código do status do erro.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Mensagem do erro.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Título do erro.
        /// </summary>
        public string Title { get; set; }
    }
}
