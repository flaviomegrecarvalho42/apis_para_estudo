using ParaEstudoApi.DAL.Context;
using ParaEstudoApi.DAL.Interfaces;
using ParaEstudoApi.DTO.DownloadFile.Request;
using ParaEstudoApi.Models;
using ParaEstudoApi.Util.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParaEstudoApi.DAL
{
    public class DownloadFilesRepository : BaseRepository, IDownloadFilesRepository
    {
        /// <summary>
        /// Instanciar a classe de paginação, que contém configurações da paginação do endpoint.
        /// </summary>
        private readonly PagingInformationAccessor _pagination;

        public DownloadFilesRepository(ParaEstudoApiContext context,
                                       PagingInformationAccessor pagination) : base(context)
        {
            _pagination = pagination ?? throw new ArgumentNullException(nameof(pagination));
        }

        /// <summary>
        /// Retorna o total de arquivos de para download. Esta informação será retornada no response.
        /// </summary>
        /// <param name="filterDownloadFileRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> GetTotalDownloadFilesRecordsAsync(FilterDownloadFilesRequestDto filterDownloadFileRequestDto)
        {
            Expression<Func<DownloadFile, bool>> predicate = PredicatedExpressionFilter(filterDownloadFileRequestDto);

            var resultedQuery = BaseDownloadFileCompositionQuery()
                                    .Where(predicate)
                                    .Count();

            return await Task.FromResult(resultedQuery);
        }

        /// <summary>
        /// Retorna uma lista de arquivos para download com base nos filtros.
        /// </summary>
        /// <param name="filterDownloadFileRequestDto"></param>
        /// <returns></returns>
        public async Task<List<DownloadFile>> GetListDownloadFilesByFilterAsync(FilterDownloadFilesRequestDto filterDownloadFileRequestDto)
        {
            Expression<Func<DownloadFile, bool>> predicate = PredicatedExpressionFilter(filterDownloadFileRequestDto);

            var downloadFiles = BaseDownloadFileCompositionQuery()
                                .Where(predicate)
                                .OrderByDescending(o => o.GeneratedAt)
                                .Skip(_pagination.Skip)
                                .Take(_pagination.PageSize)
                                .ToList();

            return await Task.FromResult(downloadFiles);
        }

        /// <summary>
        /// Retorna os dados do arquivo para que seja realizado o download.
        /// </summary>
        /// <param name="downloadFileId"></param>
        /// <returns></returns>
        public async Task<DownloadFile> GetDataDownloadFilesIdAsync(int downloadFileId)
        {
            var downloadFileRecord = BaseDownloadFileCompositionQuery()
                                    .Where(c => c.Id == downloadFileId)
                                    .FirstOrDefault();

            return await Task.FromResult(downloadFileRecord);
        }

        /// <summary>
        /// Método criado para adição dos filtros recebidos para a consulta da lista de arquivos para download.
        /// </summary>
        /// <param name="filterDownloadFileRequestDto"></param>
        /// <returns></returns>
        private Expression<Func<DownloadFile, bool>> PredicatedExpressionFilter(FilterDownloadFilesRequestDto filterDownloadFileRequestDto)
        {
            var predicate = PredicateBuilder.True<DownloadFile>();

            if (filterDownloadFileRequestDto.DateFrom.HasValue)
            {
                predicate = predicate.And((s) => s.GeneratedAt.Date >= filterDownloadFileRequestDto.DateFrom.Value.Date);
            }

            if (filterDownloadFileRequestDto.DateTo.HasValue)
            {
                predicate = predicate.And((s) => s.GeneratedAt.Date <= filterDownloadFileRequestDto.DateTo.Value.Date);
            }

            return predicate;
        }

        /// <summary>
        /// Retorna a query basica de DownloadFile.
        /// </summary>
        /// <returns></returns>
        private IQueryable<DownloadFile> BaseDownloadFileCompositionQuery()
        {
            return _context
                    .DownloadFile;
        }
    }
}
