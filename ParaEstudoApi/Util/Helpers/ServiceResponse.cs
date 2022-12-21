using System.Collections.Generic;

namespace ParaEstudoApi.Util.Helpers
{
    /// <summary>
    /// Resposta com dados referentes ao processamento de um serviço.
    /// </summary>
    public class ServiceResponse
    {
        /// <summary>
        /// Status do processamento da requisição.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Mensagens relacionadas ao processamento.
        /// </summary>
        public List<string> Messages { get; set; }

        public ServiceResponse(int statusCode)
        {
            Messages = new List<string>();
            StatusCode = statusCode;
        }

        public ServiceResponse(int statusCode, params string[] messages) : this(statusCode)
        {
            Messages.AddRange(messages);
        }
    }

    /// <summary>
    /// Resposta com dados referentes ao processamento de um serviço.
    /// </summary>
    /// <typeparam name="T">Valor genérico</typeparam>
    public class ServiceResponse<T> : ServiceResponse
    {
        /// <summary>
        /// Conteúdo específico do retorno.
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        public ServiceResponse(int statusCode) : base(statusCode)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="messages"></param>
        public ServiceResponse(int statusCode, params string[] messages) : base(statusCode, messages)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="content"></param>
        public ServiceResponse(int statusCode, T content) : base(statusCode)
        {
            Content = content;
        }
    }
}
