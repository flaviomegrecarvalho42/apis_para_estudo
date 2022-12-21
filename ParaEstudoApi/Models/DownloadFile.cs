using System;

namespace ParaEstudoApi.Models
{
    /// <summary>
    /// Classe model que representa a tabela onde são gravadas as informações dos arquivos de download.
    /// </summary>
    public class DownloadFile: DEntity
    {
        /// <summary>
        /// Nome do arquivo na pasta.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data de criação do arquivo na pasta.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Data de geração do arquivo.
        /// </summary>
        public DateTime GeneratedAt { get; set; }
    }
}
