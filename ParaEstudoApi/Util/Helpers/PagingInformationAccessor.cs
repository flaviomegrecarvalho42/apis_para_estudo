using Microsoft.AspNetCore.Http;
using ParaEstudoApi.Util.Exceptions;
using ParaEstudoApi.Util.Constants;
using System;

namespace ParaEstudoApi.Util.Helpers
{
    /// <summary>
    /// Classe criada para receber os parâmetros de pagina e total de registros por página via context e config.
    /// Estes parâmetros serão utilizados para montar a consulta que retornará os arquivos para fazer download.
    /// </summary>
    public class PagingInformationAccessor
    {
        private readonly int _page;
        private readonly int _pageSize;
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public PagingInformationAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
            _page = GetPaginationQueryParameter("page", Pagination.Page);
            _pageSize = GetPaginationQueryParameter("pagesize", Pagination.PageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize => _pageSize;
        
        /// <summary>
        /// 
        /// </summary>
        public int Page => _page;
        
        /// <summary>
        /// 
        /// </summary>
        public int Skip => ((Page * PageSize) - PageSize);

        private HttpRequest GetRequest()
        {
            return _accessor.HttpContext?.Request;
        }

        private int GetPaginationQueryParameter(string queryParameter, int defaultValue)
        {
            var valueQuery = GetRequest().Query[queryParameter].ToString();
            int value = string.IsNullOrWhiteSpace(valueQuery) ? defaultValue : Convert.ToInt32(valueQuery);

            if (value < 0)
                throw new ParaEstudoApiException(StatusCodes.Status400BadRequest, $"Parâmetero '{queryParameter}' não pode ser negativo.");

            return value;
        }
    }
}
