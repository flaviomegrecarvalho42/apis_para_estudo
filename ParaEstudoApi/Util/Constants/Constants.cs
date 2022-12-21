using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParaEstudoApi.Util.Constants
{
    public class Constants
    {
        /// <summary>
        /// Caminho onde os arquivos para download estão gravados.
        /// </summary>
        public const string PathDownloadArquivos = "c:\\Files\\Downloads";

        /// <summary>
        /// Caminho onde os arquivos de upload serão gravados.
        /// </summary>
        public const string PathUploadArquivos = "c:\\Files\\Uploads";

        /// <summary>
        /// Caminho onde os arquivos originais de upload serão gravados.
        /// </summary>
        public const string PathUploadArquivosOriginal = "c:\\Files\\UploadsOriginal";

        /// <summary>
        /// Tipo de extensão que o endpoint de download irá utilizar.
        /// </summary>
        public const string TipoExtensaoArquivoDownload = "application/octetstream";
        
        /// <summary>
        /// Extensão que o arquivo de upload.
        /// </summary>
        public const string ExtensaoArquivoUpload = ".rem";

        /// <summary>
        /// Mensagem de erro da validação dos filtros de entrada (parâmetros de entrada dos endpoints) data início e data fim.
        /// </summary>
        public const string DateToDeveSerMaiorQueDateFrom = "A data início deve ser maior do que a data fim.";

        /// <summary>
        /// Tamanho Máximo do arquivo de upload.
        /// </summary>
        public const long LimiteTamanhoArquivoDownoladEmKb = 5120;
    }

    public static class Pagination
    {
        /// <summary>
        /// Número da página solicitada para consulta dos endpoints.
        /// </summary>
        public const int Page = 1;
        
        /// <summary>
        /// Total de registros por página para consulta dos endpoints.
        /// </summary>
        public const int PageSize = 30;
    }
}
