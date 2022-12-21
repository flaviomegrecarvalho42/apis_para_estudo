using System;

namespace ParaEstudoApi.DTO
{
    public abstract class BaseResponse
    {
        /// <summary>
        /// Data da geração do arquivo.
        /// </summary>
        public DateTime GenerationDate { get; set; }

        /// <summary>
        /// Nome do arquivo gravado na base de dados.
        /// </summary>
        public string FileName { get; set; }
    }
}
