using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ParaEstudoApi.Util.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace ParaEstudoApi.Util.Document
{
    /// <summary>
    /// Classe responsável pela exibição dos parâmetros Page e PageSize no swagger.
    /// </summary>
    public class PaginationFilterSwagger : IOperationFilter
    {
        /// <summary>
        /// Método responsável pela configuração e exibição dos parâmetros Page e PageSize no swagger.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var methodsWithPagination = new List<string> { "GetAllDonwloadFiles", "GetAllUploadFiles" };

            if (methodsWithPagination.Contains(context.MethodInfo.Name))
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "page",
                    In = ParameterLocation.Query,
                    Description = "Número da página que está sendo requisitada.",
                    Schema = new OpenApiSchema
                    {
                        Type = "Integer",
                        Default = new OpenApiInteger(Pagination.Page)
                    }
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "pageSize",
                    In = ParameterLocation.Query,
                    Description = "Quantidade total de registros por páginas.",
                    Schema = new OpenApiSchema
                    {
                        Type = "Integer",
                        Default = new OpenApiInteger(Pagination.PageSize)
                    }
                });
            }
        }
    }
}
