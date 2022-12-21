using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParaEstudoApi.BLL;
using ParaEstudoApi.BLL.Interfaces;
using ParaEstudoApi.DAL;
using ParaEstudoApi.DAL.Context;
using ParaEstudoApi.DAL.Interfaces;
using ParaEstudoApi.Util.Helpers;

namespace ParaEstudoApi.Util.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configure the database context.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ParaEstudoApiContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        /// <summary>
        /// Configure DI Services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddServiceDependencyInjection(this IServiceCollection serviceCollection)
        {
            //Cria injeção dos repositorios
            serviceCollection.AddScoped<IDownloadFilesRepository, DownloadFilesRepository>();
            serviceCollection.AddScoped<IUploadFilesRepository, UploadFilesRepository>();

            //Cria injeção das BLLs
            serviceCollection.AddScoped<IDownloadFilesBll, DownloadFilesBll>();
            serviceCollection.AddScoped<IUploadFilesBll, UploadFilesBll>();

            //Cria injeção da Acessor
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddScoped<PagingInformationAccessor>();
        }
    }
}
