using Microsoft.AspNetCore.Http;
using System;

namespace ParaEstudoApi.DTO
{
    public class CnabInfoDto
    {
        /// <summary>
        /// Cnpj da empresa dentro do arquivo.
        /// </summary>
        public string Cnpj { get; set; }

        /// <summary>
        /// Nome da empresa dentro do arquivo.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Data de geração dentro do arquivo.
        /// </summary>
        public string GeneratedAt { get; set; }

        /// <summary>
        /// Código hash do conteúdo do arquivo.
        /// </summary>
        public string HashCode { get; set; }

        /// <summary>
        /// Arquivo enviado pelo front.
        /// </summary>
        public IFormFile UploadFormFile { get; set; }

        /// <summary>
        /// Retorna o path original com o cnpj da empresa do path.
        /// </summary>
        public string OriginPath { get; set; }

        /// <summary>
        /// Path completo com nome pra salvar o arquivo original.
        /// </summary>
        public string OriginName { get; set; }

        /// <summary>
        /// Código da empresa.
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Número do Sequêncial do arquivo CNAB.
        /// </summary>
        public string SequentialUploadFileNumber { get; set; }

        /// <summary>
        /// Emite boleto (propriedade do arquivo CNAB).
        /// </summary>
        public bool IssueTicket { get; set; }
    }
}
