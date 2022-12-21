using Microsoft.AspNetCore.Http;
using ParaEstudoApi.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection.Metadata;

namespace ParaEstudoApi.Util.Helpers
{
    public static class StreamFileHelper
    {
        /// <summary>
        /// Lê o arquivo e pega as informações de nome e cnpj da empresa, data de criação do arquivo .ret no conteudo do arquivo.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static CnabInfoDto GetInternalInfoFileByPath(string path)
        {
            var lines = File.ReadAllLines(path);

            var lineZero = lines[0].ToString();

            var infoArquivo = new CnabInfoDto
            {
                CompanyName = GetName(lineZero),
                GeneratedAt = GetGeneratedAt(lineZero),
                Cnpj = GetCnpj(lines[1])
            };

            return infoArquivo;
        }

        /// <summary>
        /// Retornar o dto com informações do conteudo e do arquivo.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static CnabInfoDto GetInternalInfoFileByFile(IFormFile file)
        {
            CnabInfoDto cnabInfo = null;

            try
            {
                var list = GetAllLines(file.OpenReadStream());

                var cnpj = GetCnpj(list[1]);

                var originPath = Path.Combine(ParaEstudoApi.Util.Constants.Constants.PathUploadArquivosOriginal,
                                              cnpj);

                cnabInfo = new CnabInfoDto
                {
                    CompanyName = GetName(list[0]),
                    GeneratedAt = GetGeneratedAt(list[0]),
                    Cnpj = cnpj,
                    HashCode = GenerateHashFromFormFile(file),
                    UploadFormFile = file,
                    OriginPath = originPath,
                    OriginName = Path.Combine(originPath, file.FileName)
                };

            }
            catch { }

            return cnabInfo;
        }

        /// <summary>
        /// Gera o código hash do conteúdo do arquivo.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static string GenerateHashFromFormFile(IFormFile formFile)
        {
            string contendRead = string.Empty;

            var crypt = System.Security.Cryptography.SHA256.Create();

            var resultado = new System.Text.StringBuilder();

            using (var streamFormFile = formFile.OpenReadStream())
            {

                var hashComputedBytes = crypt.ComputeHash(streamFormFile);

                foreach (byte b in hashComputedBytes)
                    resultado.Append(b.ToString("x2"));
            }


            return resultado.ToString();
        }

        /// <summary>
        /// Retornar o dto com informações do conteudo do arquivo na validação do layout do CNAB.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static CnabInfoDto GetInternalInfoFileCnabValidator(IFormFile file)
        {
            CnabInfoDto cnabInfo = null;

            try
            {
                var list = GetAllLines(file.OpenReadStream());

                cnabInfo = new CnabInfoDto
                {
                    Cnpj = GetCnpjCedente(list[1]),
                    CompanyCode = GetCompanyCode(list[0]),
                    CompanyName = GetName(list[0]),
                    GeneratedAt = GetGeneratedAt(list[0]),
                    SequentialUploadFileNumber = GetSequentialUploadFileNumber(list[0]),
                    IssueTicket = GetIssueTicket(list[1])
                };
            }
            catch { }

            return cnabInfo;
        }

        /// <summary>
        /// Retorna o nome da empresa.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string GetName(string line)
        {
            return line.Substring(46, 30).Trim();
        }

        /// <summary>
        /// Retorna da data de geração do arquivo.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string GetGeneratedAt(string line)
        {
            return line.Substring(94, 6);
        }

        /// <summary>
        /// Retorna o CNPJ da empresa.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string GetCnpj(string line)
        {
            return line.Substring(3, 14).Trim();
        }

        /// <summary>
        /// Retorna todos os registros (linhas) do arquivo.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static IList<string> GetAllLines(Stream stream)
        {
            var list = new List<string>();
            var line = string.Empty;

            using (TextReader textReader = new StreamReader(stream))
            {
                while ((line = textReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            return list;
        }

        /// <summary>
        /// Retorna do código da empresa.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string GetCompanyCode(string line)
        {
            return line.Substring(26, 20).Trim();
        }

        /// <summary>
        /// Retorna o número sequencial do arquivo de upload
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string GetSequentialUploadFileNumber(string line)
        {
            return line.Substring(111, 7).Trim();
        }

        /// <summary>
        /// Método criado para buscar o CNPJ Cedente do arquivo de remessa (validação do arquivo CNAB 400).
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string GetCnpjCedente(string line)
        {
            return line.Substring(334, 15).Trim();
        }

        /// <summary>
        /// Método criado para buscar a propriedade Emite Boleto (validação do arquivo CNAB 400).
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static bool GetIssueTicket(string line)
        {
            var issueTicket = line.Substring(92, 1).Trim();
            return issueTicket == "2" ? true : false;
        }
    }
}
