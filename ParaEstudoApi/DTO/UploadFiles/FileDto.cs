using System;

namespace ParaEstudoApi.DTO.UploadFiles
{
    public class FileDto
    {
        public FileDto(string url, string format)
        {
            Id = Guid.NewGuid();
            Url = url;
            Format = format;
        }

        public Guid Id { get; private set; }
        public string Url { get; private set; }
        public string Format { get; private set; }
    }
}
