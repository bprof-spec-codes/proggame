using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using proggame.Entities;
using proggame.Services.Dtos.SolutionFileDtos;
using proggame.Services.Dtos.TestFileDtos;
using proggame.Services.Facades;
using System.IO.Compression;
using System.Text;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace proggame.Services.DomainServices
{
    public class FileDomainService : IDomainService
    {
        private readonly IProcessFacade _processFacade;
        private readonly IMapper mapper;
        private readonly IRepository<TestFile> testFileReposity;

        public FileDomainService(IProcessFacade processFacade, IMapper mapper, IRepository<TestFile> testFileReposity)
        {
            _processFacade = processFacade ?? throw new ArgumentNullException(nameof(processFacade));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.testFileReposity = testFileReposity ?? throw new ArgumentNullException(nameof(testFileReposity));
        }

        public async Task<SeparatedSolutionFileDto> SeparateAsync(CreateUpdateSolutionFileDto slnZip)
        {
            throw new NotImplementedException();
        }

        public async Task<string> JoinAsync(SolutionFileDto files)
        {
            string folderPath = $"C:\\Temp\\proggame\\{files.Name}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllBytes($"{folderPath}\\{files.Name}", Unzip(files.Content));

            Guid key = new Guid();

            if (ContainsSLNSignature(files.Content))
            {
                int slnIndex = Array.IndexOf(files.Content, Encoding.UTF8.GetBytes("Microsoft Visual Studio Solution File,"));
                if (slnIndex >= 0)
                {
                    string slnContent = Encoding.UTF8.GetString(files.Content, slnIndex, files.Content.Length - slnIndex);
                    string firstLine = slnContent.Substring(0, slnContent.IndexOf('\n'));

                    key = new Guid(firstLine);
                }
            }

            var tests = await testFileReposity.GetListAsync(x => x.Id == key);

            foreach (var item in tests)
            {
                File.WriteAllBytes($"{folderPath}\\{item.Name}", item.Content);
            }

            _processFacade.RunProcess("", "dotnet.exe");

            return folderPath;
        }

        public async Task<double> RunTestsAsync(SolutionFileDto solutionFile)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> IsPlagiarism(SolutionFileDto solutionFile)
        {
            throw new NotImplementedException();
        }

        static byte[] Unzip(byte[] compressedBytes)
        {
            using (MemoryStream compressedStream = new MemoryStream(compressedBytes))
            using (MemoryStream decompressedStream = new MemoryStream())
            {
                using (ZipArchive archive = new ZipArchive(compressedStream, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        using (Stream entryStream = entry.Open())
                        {
                            entryStream.CopyTo(decompressedStream);
                        }
                    }
                }

                return decompressedStream.ToArray();
            }
        }

        static bool ContainsSLNSignature(byte[] bytes)
        {
            byte[] slnSignature = Encoding.UTF8.GetBytes("Microsoft Visual Studio Solution File,");
            return Array.IndexOf(bytes, slnSignature) != -1;
        }
    }
}
