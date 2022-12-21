using ParaEstudoApi.DTO.DownloadFile.Response;
using ParaEstudoApi.Models;
using System.Collections.Generic;

namespace ParaEstudoApi.BLL.Mappings
{
    public static class MappingDownloadFilesToDto
    {
        /// <summary>
        /// Realiza a conversão da classe model (List<DownloadFile>) para a classe de DTO (List<DataDownloadFileResponseDto>).
        /// </summary>
        /// <param name="downloadFiles"></param>
        /// <returns></returns>
        public static List<DataDownloadFilesResponseDto> MapToListDataDownloadFilesResponseDto(List<DownloadFile> downloadFiles)
        {
            var listDonloadFilesResponseDto = new List<DataDownloadFilesResponseDto>();

            foreach (var downloadFile in downloadFiles)
            {
                listDonloadFilesResponseDto.Add(MapToDataDownloadFilesResponseDto(downloadFile));
            }

            return listDonloadFilesResponseDto;
        }

        /// <summary>
        /// Realiza a conversão da classe model (DownloadFile) para a classe de DTO (DataDownloadFileResponseDto).
        /// </summary>
        /// <param name="downloadFile"></param>
        /// <returns></returns>
        public static DataDownloadFilesResponseDto MapToDataDownloadFilesResponseDto(DownloadFile downloadFile)
        {
            if (downloadFile == null)
                return null;

            return new DataDownloadFilesResponseDto
            {
                GenerationDate = downloadFile.GeneratedAt,
                FileName = downloadFile.Name,
                Id = downloadFile.Id
            };
        }
    }
}
