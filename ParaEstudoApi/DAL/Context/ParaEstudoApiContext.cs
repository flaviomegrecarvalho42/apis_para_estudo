using Microsoft.EntityFrameworkCore;
using ParaEstudoApi.DAL.Configurations;
using ParaEstudoApi.Models;

namespace ParaEstudoApi.DAL.Context
{
    public class ParaEstudoApiContext : DbContext
    {
        /// <summary>
        /// Construtor do Context.
        /// </summary>
        /// <param name="options"></param>
        public ParaEstudoApiContext(DbContextOptions<ParaEstudoApiContext> options) : base(options) { }

        /// <summary>
        /// Declaração da classe model para consulta na base de dados (representação da tabela da base de dados).
        /// </summary>
        public DbSet<DownloadFile> DownloadFile { get; set; }

        /// <summary>
        /// Declaração da classe model para consulta na base de dados (representação da tabela da base de dados).
        /// </summary>
        public DbSet<UploadFile> UploadFile { get; set; }

        /// <summary>
        /// Ao executar a migration, este método é chamado para criar as tabelas .
        /// na base de dados de acordo com a classe de configuração (por exemplo: DownloadFilesConfiguration).
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DownloadFile>(new DownloadFileConfiguration().Configure);
            modelBuilder.Entity<UploadFile>(new UploadFileConfiguration().Configure);
        }
    }
}
