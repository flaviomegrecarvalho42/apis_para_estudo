using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ParaEstudoApi.BLL.Interfaces;
using ParaEstudoApi.BLL.Mappings;
using ParaEstudoApi.DAL.Interfaces;
using ParaEstudoApi.DTO;
using ParaEstudoApi.DTO.Pagination;
using ParaEstudoApi.DTO.UploadFiles.Request;
using ParaEstudoApi.DTO.UploadFiles.Response;
using ParaEstudoApi.Models;
using ParaEstudoApi.Util.Helpers;
using ParaEstudoApi.Util.Validators;
using ParaEstudoApi.Util.Constants;
using ParaEstudoApi.Util.Exceptions;
using ParaEstudoApi.Util.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ParaEstudoApi.Util.Extensions;

namespace ParaEstudoApi.BLL
{
    public class UploadFilesBll : IUploadFilesBll
    {
        private readonly IUploadFilesRepository _uploadFilesRepository;
        private readonly PagingInformationAccessor _pagination;

        public UploadFilesBll(IUploadFilesRepository uploadFilesRepository, PagingInformationAccessor pagination)
        {
            _uploadFilesRepository = uploadFilesRepository ?? throw new ArgumentNullException(nameof(uploadFilesRepository));
            _pagination = pagination ?? throw new ArgumentNullException(nameof(pagination));
        }

        /// <summary>
        /// Retorna uma lista de arquivos existentes na pasta com base nos filtros do endpoint.
        /// </summary>
        /// <param name="filterUploadFilesRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="ParaEstudoApiException"></exception>
        public async Task<ServiceResponse<GetUploadFilesResponseDto>> GetAllUploadFilesAsync(FilterUploadFilesRequestDto filterUploadFilesRequestDto)
        {
            //Realiza a validação dos dados do filtro enviados pelo endpoint
            //Por exemplo, se a data início é menor do que a data fim da consulta
            if (!FilterUploadFilesValidator.IsValid(filterUploadFilesRequestDto))
                throw new ParaEstudoApiException(StatusCodes.Status400BadRequest, Constants.DateToDeveSerMaiorQueDateFrom);

            //Retorna o total de arquivos de upload encontrados
            //Esse total é exibido no endpoint a título de informação
            var totalRecords = _uploadFilesRepository.GetTotalUploadFilesRecordsAsync(filterUploadFilesRequestDto).ConfigureAwait(false);
            
            //Retorna a lista de arquivos de upload encontrados na Base de Dados
            var listUploadFiles = await _uploadFilesRepository.GetListUploadFilesByFilterAsync(filterUploadFilesRequestDto).ConfigureAwait(false);

            //Realiza a conversão da lista dos arquivos de upload em uma lista de DTO response que será exibido no endpoint
            var uploadFilesResponseDto = MappingUploadFilesToDto.MapToListUploadFilesResponseDto(listUploadFiles);

            //Cria o DTO response dos arquivos de upload
            //Este DTO é o que será retornado para o endopoint com as informações dos arquivos que foram feito upload
            var getUploadFilesResponseDto = new GetUploadFilesResponseDto
            {
                UploadFiles = uploadFilesResponseDto,
                Pagination = new PaginationResponseDto(_pagination.PageSize, await totalRecords)
            };

            return new ServiceResponse<GetUploadFilesResponseDto>(StatusCodes.Status200OK, getUploadFilesResponseDto);
        }

        /// <summary>
        /// Recebe uma lista de arquivos para serem salvos na pasta.
        /// </summary>
        /// <param name="listFiles"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<IList<UploadFilesResponseDto>>> PostUploadFilesAsync(IList<IFormFile> listFiles)
        {
            //Retorna o DTO com os dados dos arquivos de upload
            var listFileInfo = GetListFileInfo(listFiles);

            var listUploadFiles = new List<UploadFile>();

            foreach (var fileInfo in listFileInfo)
            {
                if (!Directory.Exists(fileInfo.OriginPath))
                    Directory.CreateDirectory(fileInfo.OriginPath);

                //Cria os arquivos de upload originais na pasta do servidor
                CreateUploadFileInFileServer(fileInfo.UploadFormFile, fileInfo.OriginName);

                string nomeArquivo = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}_{fileInfo.Cnpj}{Constants.ExtensaoArquivoUpload}";
                string path = Path.Combine(Constants.PathUploadArquivos, nomeArquivo);

                //Cria os arquivos de upload atualizados na pasta do servidor
                CreateUploadFileInFileServer(fileInfo.UploadFormFile, path);

                //Cria a classe de upload com os dados do arquivo
                //As informações dessa classe serão salvas na base de dados
                var uploadFile = new UploadFile
                {
                    Name = nomeArquivo,
                    OriginName = fileInfo.UploadFormFile.FileName,
                    CreatedAt = DateTime.Now.Date,
                    GeneratedAt = fileInfo.GeneratedAt.ToDateTime(),
                    HashCode = fileInfo.HashCode
                };

                listUploadFiles.Add(uploadFile);
            }

            //Salva os dados dos arquivos de upload na Base de Dados
            if (listUploadFiles.Any())
                await _uploadFilesRepository.SaveAllAsync(listUploadFiles);

            //Realiza a conversão da lista dos arquivos de upload em uma lista de DTO response que será exibido no endpoint
            var resp = MappingUploadFilesToDto.MapToListUploadFilesResponseDto(listUploadFiles);

            return new ServiceResponse<IList<UploadFilesResponseDto>>(StatusCodes.Status201Created, resp);
        }

        /// <summary>
        /// Cria os arquivos de upload na pasta do servidor
        /// </summary>
        /// <param name="arquivo"></param>
        /// <param name="path"></param>
        private void CreateUploadFileInFileServer(IFormFile arquivo, string path)
        {
            var stream = new FileStream(path, FileMode.Create);
            arquivo.CopyTo(stream);
            stream.Close();
            stream.Dispose();
        }

        /// <summary>
        /// Retorna um DTO (uma lista) com os dados dos arquivos que serão feito upload
        /// </summary>
        /// <param name="listFiles"></param>
        /// <returns></returns>
        private IList<CnabInfoDto> GetListFileInfo(IList<IFormFile> listFiles)
        {
            //Realiza a validação dos arquivos (se tem o tamanho máximo permitido, etc.)
            UploadFilesValidator.Verify(listFiles, Constants.LimiteTamanhoArquivoDownoladEmKb);

            //Verifica se as pastas de upload existem, caso contrário retorna erro
            VerifyExistDirectory(Constants.PathUploadArquivos);
            VerifyExistDirectory(Constants.PathUploadArquivosOriginal);

            var listInfoFile = listFiles
                .Select(file => StreamFileHelper.GetInternalInfoFileByFile(file));

            return listInfoFile.ToList();
        }

        /// <summary>
        /// Verifica se a pasta existe, caso não é lançado uma execeção.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <exception cref="Exception"></exception>
        private void VerifyExistDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new Exception($"A pasta {directoryPath} não foi criada");
        }

    }
}
