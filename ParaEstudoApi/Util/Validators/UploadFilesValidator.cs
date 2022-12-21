using Microsoft.AspNetCore.Http;
using ParaEstudoApi.Util.Exceptions;
using ParaEstudoApi.Util.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ParaEstudoApi.Util.Validators
{
    public static class UploadFilesValidator
    {
        /// <summary>
        /// Realiza a validação do arquivo de upload.
        /// </summary>
        /// <param name="listFiles"></param>
        /// <param name="uploadFileSizeLimitInKb"></param>
        /// <param name="type"></param>
        /// <exception cref="ParaEstudoApiException"></exception>
        public static void Verify(IList<IFormFile> listFiles, long uploadFileSizeLimitInKb)
        {
            if (listFiles is null || !listFiles.Any(s => s != null))
                throw new ParaEstudoApiException(StatusCodes.Status400BadRequest, "Arquivo não existe.");

            if (listFiles
                .Select(x => Path.GetExtension(x.FileName.ToLower()))
                .Any(x => !x.Contains(ParaEstudoApi.Util.Constants.Constants.ExtensaoArquivoUpload)))
                throw new ParaEstudoApiException(StatusCodes.Status400BadRequest, "Extensão não suportada.");

            if (listFiles
                .Select(formFile => DataConversion.ByteToKiloByte(formFile.Length))
                .Any(length => length > uploadFileSizeLimitInKb))
                throw new ParaEstudoApiException(StatusCodes.Status400BadRequest, "Tamanho do arquivo excedido.");
        }
    }
}
