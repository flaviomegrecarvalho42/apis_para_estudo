using System;

namespace ParaEstudoApi.DTO.Pagination
{
    /// <summary>
    ///  Classe DTO de resposta que retorna os dados de paginação do endpoint.
    /// </summary>
    public class PaginationResponseDto
    {
        private readonly int _pageSize;
        private readonly int _totalRecords;

        /// <summary>
        /// Construtor do DTO Pagination.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        public PaginationResponseDto(int pageSize, int totalRecords)
        {
            _pageSize = pageSize;
            _totalRecords = totalRecords;
        }

        /// <summary>
        /// Total de páginas que terão na consulta que retorna a listagem dos arquivos para download (calculado usando Totalrecords e TotalPageSize).
        /// </summary>
        public int TotalPages { get { return (int)Math.Ceiling(TotalRecords / (double)TotalPageSize); } }

        /// <summary>
        /// Quantidade de registros exibidos por página.
        /// </summary>
        public int TotalPageSize { get { return _pageSize; } }

        /// <summary>
        /// Total de registros retornado na query de consulta que retorna a listagem dos arquivos para download.
        /// </summary>
        public int TotalRecords { get { return _totalRecords; } }
    }
}
