using ParaEstudoApi.DTO.UploadFiles.Response;
using ParaEstudoApi.Models;
using System.Collections.Generic;

namespace ParaEstudoApi.BLL.Mappings
{
    public static class MappingUploadFilesToDto
    {
        /// <summary>
        /// Realiza a conversão da classe model (List<UploadFile>) para a classe de DTO (List<UploadFilesResponseDto>).
        /// </summary>
        /// <param name="uploadFiles"></param>
        /// <returns></returns>
        public static List<UploadFilesResponseDto> MapToListUploadFilesResponseDto(List<UploadFile> uploadFiles)
        {
            var listUploadFilesResponseDto = new List<UploadFilesResponseDto>();

            foreach (var uploadFile in uploadFiles)
            {
                listUploadFilesResponseDto.Add(MapToUploadFilesResponseDto(uploadFile));
            }

            return listUploadFilesResponseDto;
        }

        /// <summary>
        /// Realiza a conversão da classe model (UploadFile) para a classe de DTO (UploadFilesResponseDto).
        /// </summary>
        /// <param name="uploadFile"></param>
        /// <returns></returns>
        public static UploadFilesResponseDto MapToUploadFilesResponseDto(UploadFile uploadFile)
        {
            if (uploadFile == null)
                return null;

            return new UploadFilesResponseDto
            {
                GenerationDate = uploadFile.GeneratedAt,
                FileName = uploadFile.OriginName
            };
        }
    }
}
