using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using proggame.Services.Dtos;
using AutoMapper;
using proggame.Entities;
using proggame.Services.Dtos.SolutionFileDtos;
using proggame.Services.Facades;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Microsoft.CodeAnalysis;

namespace proggame.Services.DomainServices
{
    public class FileDomainService : IDomainService, IFileDomainService
    {
        private readonly IProcessFacade _processFacade;
        private readonly IRepository<TestFile> testFileReposity;
        private readonly IRepository<SolutionFile> solutionFileRepository;

        public FileDomainService(IProcessFacade processFacade, IRepository<TestFile> testFileReposity, IRepository<SolutionFile> solutionFileRepository)
        {
            _processFacade = processFacade ?? throw new ArgumentNullException(nameof(processFacade));
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

            File.WriteAllBytes($"{folderPath}", entity.Content);

            ZipFile.ExtractToDirectory($"{folderPath}\\{entity.Name}", $"{folderPath}\\{entity.Name}");

            var tests = await testFileReposity.GetListAsync(x => x.TaskId == entity.TaskId);

            foreach (var item in tests)
            {
                File.WriteAllBytes($"{folderPath}\\{item.Name}", item.Content);
                ZipFile.ExtractToDirectory($"{folderPath}\\{item.Name}", $"{folderPath}\\{item.Name}");
            }

            _processFacade.RunProcess("sln add", "<.csproj path>");

            return folderPath;
        }
        public async Task<double> RunTestsAsync(SolutionFileDto solutionFile)
        /// This method is responsible for running tests on a solution file. It performs the following steps:
        /// 1. Creates a temporary directory for unpacking the .zip file.
        /// 2. Unzips the .zip file into the temporary directory.
        /// 3. Compiles the solution files in the temporary directory.
        /// 4. Calculates a score based on the success of the compilation.
        /// 5. Deletes the temporary directory.
        /// 6. Returns the calculated score.
        {

           
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            Unzip(solutionFile.Content, tempDirectory);
            double score = Compile(tempDirectory) ? 1 : 0;
            Directory.Delete(tempDirectory, true);

            

            return score;
        }

        // .unzip a .zip file 
        private void Unzip(byte[] zipContent, string destinationDirectory)
        {
            using (var zipStream = new MemoryStream(zipContent))
            using (var archive = new ZipArchive(zipStream))
            {
                foreach (var entry in archive.Entries)
                {
                    var filePath = Path.Combine(destinationDirectory, entry.FullName);
                    var directoryPath = Path.GetDirectoryName(filePath);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    using (var entryStream = entry.Open())
                    {
                        entryStream.CopyTo(fileStream);
                    }
                }
            }
        }


        private bool Compile(string solutionPath)
        {
            var syntaxTrees = new List<SyntaxTree>();
            foreach (var filePath in Directory.EnumerateFiles(solutionPath, "*.cs"))
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(filePath));
                syntaxTrees.Add(syntaxTree);
            }

            var references = new List<MetadataReference>();
            foreach (var assemblyPath in Directory.EnumerateFiles(solutionPath, "*.dll"))
            {
                var reference = MetadataReference.CreateFromFile(assemblyPath);
                references.Add(reference);
            }

            var compilation = CSharpCompilation.Create("MySolution", syntaxTrees, references);

            
            using (var ms = new MemoryStream())
            {
                var compilationResult = compilation.Emit(ms);

                
                return compilationResult.Success;
            }
        }

        public Task<bool> IsPlagiarism(SolutionFileDto solutionFile)
        {
            throw new NotImplementedException();
        }
    }
}

