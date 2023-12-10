using AutoMapper;
using proggame.Entities;
using proggame.Services.Dtos.SolutionFileDtos;
using proggame.Services.Facades;
using System.IO.Compression;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace proggame.Services.DomainServices
{
    public class FileDomainService : IDomainService, IFileDomainService
    {
        private readonly IProcessFacade _processFacade;
        private readonly IMapper mapper;
        private readonly IRepository<TestFile> testFileReposity;
        private readonly IRepository<SolutionFile> solutionFileRepository;

        public FileDomainService(IProcessFacade processFacade, IMapper mapper, IRepository<TestFile> testFileReposity, IRepository<SolutionFile> solutionFileRepository)
        {
            _processFacade = processFacade ?? throw new ArgumentNullException(nameof(processFacade));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.testFileReposity = testFileReposity ?? throw new ArgumentNullException(nameof(testFileReposity));
            this.solutionFileRepository = solutionFileRepository ?? throw new ArgumentNullException(nameof(solutionFileRepository));
        }
        public void SeparateAsync(string path)
        {
            throw new NotImplementedException();
        }
        public async Task<string> JoinAsync(Guid id)
        {
            var entity = await solutionFileRepository.GetAsync(x => x.Id == id);

            string folderPath = $"C:\\Temp\\proggame\\{entity.Name}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllBytes($"{folderPath}\\{entity.Name}", Unzip(entity.Content));

            //Guid key = new Guid();

            //if (ContainsSLNSignature(files.Content))
            //{
            //    int slnIndex = Array.IndexOf(files.Content, Encoding.UTF8.GetBytes("Microsoft Visual Studio Solution File,"));
            //    if (slnIndex >= 0)
            //    {
            //        string slnContent = Encoding.UTF8.GetString(files.Content, slnIndex, files.Content.Length - slnIndex);
            //        string firstLine = slnContent.Substring(0, slnContent.IndexOf('\n'));

            //        key = new Guid(firstLine);
            //    }
            //}

            var tests = await testFileReposity.GetListAsync(x => x.TaskId == entity.TaskId);

            foreach (var item in tests)
            {
                File.WriteAllBytes($"{folderPath}\\{item.Name}", Unzip(item.Content));
            }

            _processFacade.RunProcess("", "dotnet.exe");

            return folderPath;
        }

        public async Task<double> RunTestsAsync(string path)
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
