using System;

namespace ParaEstudoApi.Util.Exceptions
{
    /// <summary>
    /// Classe padronizada para exibição de mensagens de erro.
    /// </summary>
    public class ParaEstudoApiException : Exception
    {
        /// <summary>
        /// Construtor da classe ParaEstudoApiException.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public ParaEstudoApiException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Código do status de erro.
        /// </summary>
        public int StatusCode { get; set; }
    }
}
