using System;

namespace ParaEstudoApi.Models
{
    public class UploadFile : DEntity
    {
        /// <summary>
        /// Nome original do arquivo ao fazer o upload.
        /// </summary>
        public string OriginName { get; set; }

        /// <summary>
        /// Nome modificado ao fazer o upload.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data ao fazer upload. Sempre sera o Datetime.Now.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data da geração do arquivo. Esta no conteudo do arquivo.
        /// </summary>
        public DateTime GeneratedAt { get; set; }

        /// <summary>
        /// Identificação unica do conteudo do arquivo.
        /// </summary>
        public string HashCode { get; set; }
    }
}
