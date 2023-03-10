using ParaEstudoApi.DTO.DownloadFile.Request;

namespace ParaEstudoApi.Util.Validators
{
    public static class FilterDownloadFilesValidator
    {
        /// <summary>
        /// Realiza a validação do filtro passado para o endpoint.
        /// Verifica se a data início é maoir do que a data fim.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool IsValid(FilterDownloadFilesRequestDto filter)
        {
            var resp = filter.DateFrom.HasValue && filter.DateTo.HasValue && filter.DateTo.Value.Date < filter.DateFrom.Value.Date;

            return !resp;
        }
    }
}
