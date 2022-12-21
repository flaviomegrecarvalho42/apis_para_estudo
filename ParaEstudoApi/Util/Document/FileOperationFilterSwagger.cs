using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace ParaEstudoApi.Util.Document
{
    public class FileOperationFilterSwagger : IOperationFilter
    {
        /// <summary>
        /// Método responsável pela configuração dos filtros no swagger.
        /// Foi necessário criar essa classe, para poder utilizar os parâmetros IFormFile e demais juntos.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileUploadMime = "multipart/form-data";
            if (operation.RequestBody == null || !operation.RequestBody.Content.Any(x => x.Key.Equals(fileUploadMime, StringComparison.InvariantCultureIgnoreCase)))
                return;

            var fileParams = context.MethodInfo.GetParameters().Where(p => p.ParameterType == typeof(IFormFile));

            if (fileParams.Any())
                operation.RequestBody.Content[fileUploadMime].Schema.Properties =
                    fileParams.ToDictionary(k => k.Name, v => new OpenApiSchema()
                    {
                        Type = "string",
                        Format = "binary"
                    });
        }
    }
}
