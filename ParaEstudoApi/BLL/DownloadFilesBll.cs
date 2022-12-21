using Microsoft.AspNetCore.Http;
using ParaEstudoApi.BLL.Interfaces;
using ParaEstudoApi.BLL.Mappings;
using ParaEstudoApi.DAL.Interfaces;
using ParaEstudoApi.DTO.DownloadFile.Request;
using ParaEstudoApi.DTO.DownloadFile.Response;
using ParaEstudoApi.DTO.Pagination;
using ParaEstudoApi.Util.Exceptions;
using ParaEstudoApi.Util.Helpers;
using ParaEstudoApi.Util.Validators;
using ParaEstudoApi.Util.Constants;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ParaEstudoApi.BLL
{
    public class DownloadFilesBll : IDownloadFilesBll
    {
        /// <summary>
        /// Instanciar o repository, o qual fara a consulta na base de dados
        /// </summary>
        private readonly IDownloadFilesRepository _downloadFilesRepository;

        /// <summary>
        /// Instanciar a classe de paginação, que contém configurações da paginação do endpoint
        /// </summary>
        private readonly PagingInformationAccessor _pagination;

        public DownloadFilesBll(IDownloadFilesRepository downloadFilesRepository, PagingInformationAccessor pagination)
        {
            _downloadFilesRepository = downloadFilesRepository ?? throw new ArgumentNullException(nameof(downloadFilesRepository));
            _pagination = pagination ?? throw new ArgumentNullException(nameof(pagination));
        }

        /// <summary>
        /// Retorna uma lista de arquivos para download com base nos filtros
        /// </summary>
        /// <param name="filterDownloadFileRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="ParaEstudoApiException"></exception>
        public async Task<ServiceResponse<GetDownloadFilesResponseDto>> GetAllDownloadFilesAsync(FilterDownloadFilesRequestDto filterDownloadFileRequestDto)
        {
            // Realiza a validação do filtro passado
            if (!FilterDownloadFilesValidator.IsValid(filterDownloadFileRequestDto))
                throw new ParaEstudoApiException(StatusCodes.Status400BadRequest, Constants.DateToDeveSerMaiorQueDateFrom);

            // Retorna o total de arquivos para Download que estão gravados no banco de dados
            var totalDownloadFiles = _downloadFilesRepository.GetTotalDownloadFilesRecordsAsync(filterDownloadFileRequestDto).ConfigureAwait(false);

            // Retorna a lista de arquivos para Download, após consulta da base de dados
            var listDownloadFiles = await _downloadFilesRepository.GetListDownloadFilesByFilterAsync(filterDownloadFileRequestDto).ConfigureAwait(false);

            // Realiza a conversão (mapping) da classe ListDTO para a classe ListResponseDTO
            var downloadFilesResponseDto = MappingDownloadFilesToDto.MapToListDataDownloadFilesResponseDto(listDownloadFiles);

            // Criar o objeto que será retornado pelo endpoint
            var getDownloadFilesResponseDto = new GetDownloadFilesResponseDto
            {
                DownloadFiles = downloadFilesResponseDto,
                Pagination = new PaginationResponseDto(_pagination.PageSize, await totalDownloadFiles)
            };

            return new ServiceResponse<GetDownloadFilesResponseDto>(StatusCodes.Status200OK, getDownloadFilesResponseDto);
        }

        /// <summary>
        /// Realiza do download do arquivo gravado na pasta
        /// </summary>
        /// <param name="downloadFileId"></param>
        /// <returns></returns>
        /// <exception cref="ParaEstudoApiException"></exception>
        public async Task<ServiceResponse<DownloadFilesResponseDto>> GetDownloadFilesByIdAsync(int downloadFileId)
        {
            // Retorna os dados do arquivo que está salvo na base de dados para realização do download
            var response = await GetDataDownloadFilesIdAsync(downloadFileId).ConfigureAwait(false);

            // Valida de o id informado retorna o nome do arquivo, ou seja se o arquivo foi encontrado
            if (string.IsNullOrWhiteSpace(response.Content.FileName))
                throw new ParaEstudoApiException(StatusCodes.Status412PreconditionFailed, "Nome do arquivo vazio.");

            // Gera o DTO do arquivo que será utilizado para realizar o download
            DownloadFilesResponseDto downloadFilesResponseDto = await GenerateDownloadFilesResponseDto(response);

            return new ServiceResponse<DownloadFilesResponseDto>(StatusCodes.Status200OK, downloadFilesResponseDto);
        }

        /// <summary>
        /// Retorna os dados do arquivo para que seja realizado o download
        /// </summary>
        /// <param name="downloadFileId"></param>
        /// <returns></returns>
        /// <exception cref="ParaEstudoApiException"></exception>
        public async Task<ServiceResponse<DataDownloadFilesResponseDto>> GetDataDownloadFilesIdAsync(int downloadFileId)
        {
            // Retorna os dados do arquivo que será realizado o download (retorna a classe model)
            var downloadFileRecord = await _downloadFilesRepository.GetDataDownloadFilesIdAsync(downloadFileId).ConfigureAwait(false);

            // Valida se o arquivo foi encontrado na base de dados
            if (downloadFileRecord == null)
                throw new ParaEstudoApiException(StatusCodes.Status404NotFound, "Arquivo não encontrado.");

            // Realiza a conversão (mapping) do arquivo (classe model) para o arquivo de retorno (response Dto)
            var donloadFileResponseDto = MappingDownloadFilesToDto.MapToDataDownloadFilesResponseDto(downloadFileRecord);

            return new ServiceResponse<DataDownloadFilesResponseDto>(StatusCodes.Status200OK, donloadFileResponseDto);
        }

        /// <summary>
        /// Gera o DTO do arquivo que será utilizado para realizar o download
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        /// <exception cref="ParaEstudoApiException"></exception>
        private async Task<DownloadFilesResponseDto> GenerateDownloadFilesResponseDto(ServiceResponse<DataDownloadFilesResponseDto> serviceResponse)
        {
            // Retorna o caminho onde os arquivos estão salvos
            var path = Path.Combine(Constants.PathDownloadArquivos, serviceResponse.Content.FileName);

            // Verifica se o arquivo existe na pasta
            if (!File.Exists(path))
                throw new ParaEstudoApiException(StatusCodes.Status404NotFound, "Arquivo não encontrado.");

            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
                stream.Close();

                await stream.DisposeAsync();

                memory.Position = 0;
            }

            DownloadFilesResponseDto downloadFilesResponseDto = new DownloadFilesResponseDto
            {
                Memory = memory,
                ExtensionType = Constants.TipoExtensaoArquivoDownload,
                FileName = serviceResponse.Content.FileName
            };

            return downloadFilesResponseDto;
        }
    }
}
