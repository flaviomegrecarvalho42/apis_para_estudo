using ParaEstudoApi.DAL.Context;
using ParaEstudoApi.DAL.Interfaces;
using ParaEstudoApi.DTO.UploadFiles.Request;
using ParaEstudoApi.Models;
using ParaEstudoApi.Util.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParaEstudoApi.DAL
{
    public class UploadFilesRepository : BaseRepository, IUploadFilesRepository
    {
        /// <summary>
        /// Instanciar a classe de paginação, que contém configurações da paginação do endpoint.
        /// </summary>
        private readonly PagingInformationAccessor _pagination;

        public UploadFilesRepository(ParaEstudoApiContext context,
                                     PagingInformationAccessor pagination) : base(context)
        {
            _pagination = pagination ?? throw new ArgumentNullException(nameof(pagination));
        }

        /// <summary>
        /// Retorna o total de arquivos. Esta informação será retornada no response para o endpoint.
        /// </summary>
        /// <param name="filterUploadFilesRequestDto"></param>
        /// <returns></returns>
        public async Task<int> GetTotalUploadFilesRecordsAsync(FilterUploadFilesRequestDto filterUploadFilesRequestDto)
        {
            Expression<Func<UploadFile, bool>> predicate = PredicatedExpressionFilter(filterUploadFilesRequestDto);

            var resultedQuery = BaseUploadFileCompositionQuery()
                                    .Where(predicate)
                                    .Count();

            return await Task.FromResult(resultedQuery);
        }

        /// <summary>
        /// Retorna uma lista de arquivos de upload com base nos filtros.
        /// </summary>
        /// <param name="filterUploadFilesRequestDto"></param>
        /// <returns></returns>
        public async Task<List<UploadFile>> GetListUploadFilesByFilterAsync(FilterUploadFilesRequestDto filterUploadFilesRequestDto)
        {
            Expression<Func<UploadFile, bool>> predicate = PredicatedExpressionFilter(filterUploadFilesRequestDto);

            var uploadFiles = BaseUploadFileCompositionQuery()
                                .Where(predicate)
                                .OrderByDescending(o => o.GeneratedAt)
                                .Skip(_pagination.Skip)
                                .Take(_pagination.PageSize)
                                .ToList();

            return await Task.FromResult(uploadFiles);
        }

        /// <summary>
        /// Salva uma lista de informações de arquivos enviados.
        /// </summary>
        /// <param name="saveAllFiles"></param>
        /// <returns></returns>
        public async Task SaveAllAsync(IList<UploadFile> saveAllFiles)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.UploadFile.AddRange(saveAllFiles);
                    _context.SaveChanges();
                    transaction.Commit();
                    await Task.CompletedTask;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Retorna os dados do arquivo para que seja realizado o upload.
        /// </summary>
        /// <param name="hashcode"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<UploadFile> GetDataUploadFilesHashcodeDocument(string hashcode, string document)
        {
            var uploadFile = BaseUploadFileCompositionQuery()
                .Where(r => r.HashCode == hashcode)
                .FirstOrDefault();

            return await Task.FromResult(uploadFile);
        }

        /// <summary>
        /// Método criado para adição dos filtros recebidos para a consulta da lista de arquivos para upload.
        /// </summary>
        /// <param name="filterUploadFilesRequestDto"></param>
        /// <returns></returns>
        private Expression<Func<UploadFile, bool>> PredicatedExpressionFilter(FilterUploadFilesRequestDto filterUploadFilesRequestDto)
        {
            var predicate = PredicateBuilder.True<UploadFile>();

            if (filterUploadFilesRequestDto.DateFrom.HasValue)
                predicate = predicate.And((s) => s.GeneratedAt.Date >= filterUploadFilesRequestDto.DateFrom.Value.Date);

            if (filterUploadFilesRequestDto.DateTo.HasValue)
                predicate = predicate.And((s) => s.GeneratedAt.Date <= filterUploadFilesRequestDto.DateTo.Value.Date);

            return predicate;
        }

        /// <summary>
        /// Retorna a query basica de UploadFile.
        /// </summary>
        /// <returns></returns>
        private IQueryable<UploadFile> BaseUploadFileCompositionQuery()
        {
            return _context
                    .UploadFile;
        }
    }
}
