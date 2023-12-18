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
using System.Xml.Linq;

namespace proggame.Services.DomainServices
{
    public class FileDomainService : IDomainService, IFileDomainService
    {
        private readonly IProcessFacade _processFacade;
        private readonly IRepository<SolutionFile, Guid> _solutionRepository;
        private readonly IRepository<TestFile, Guid> _testRepository;
        public FileDomainService(
            IProcessFacade processFacade,
            IRepository<SolutionFile, Guid> solutionRepository)
        {
            _processFacade = processFacade;
            _solutionRepository = solutionRepository;
        }
        public void SeparateAsync(string path)
        {
            string slnPath = GetFileWithExtension(path, "sln");
            List<string> projectPaths = GetFilesWithExtension(path, "csproj");
            foreach (string projectPath in projectPaths)
            {
                if (IsTestProject(projectPath))
                {
                    _processFacade.RunProcess("dotnet.exe", "sln " + slnPath + " remove " + projectPath);
                    string dir = Path.GetDirectoryName(projectPath);
                    string zipPath = Path.Combine(path, dir + ".zip");
                    ZipFile.CreateFromDirectory(projectPath, zipPath);
                    Directory.Delete(projectPath, true);
                }
            }
        }
        public async Task<string> JoinAsync(Guid id)
        {
            SolutionFile solution = await _solutionRepository.GetAsync(id);
            string dir = $"C:\\Temp\\proggame\\{id}";
            string slnZip = Path.Combine(dir, solution.Name);
            List<TestFile> tests = _testRepository.GetDbSet().Where(x => x.TaskId == solution.TaskId).ToList();
            Directory.CreateDirectory(dir);
            string slnDir = await UnzipByteAsync(slnZip, solution.Content);
            string slnPath = GetFileWithExtension(slnDir, "sln");
            foreach (TestFile test in tests)
            {
                string testDir = await UnzipByteAsync(Path.Combine(slnDir, test.Name), test.Content);
                _processFacade.RunProcess("dotnet.exe", "sln " + slnPath + " add " + GetFileWithExtension(testDir, "csproj"));
            }
            return slnDir;
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

        public string GetFileWithExtension(string dir, string extension)
        {
            return Directory
                .EnumerateFiles(dir, "*." + extension, SearchOption.AllDirectories)
                .ToList()
                .FirstOrDefault();
        }
        private List<string> GetFilesWithExtension(string dir, string extension)
        {
            return Directory
                .EnumerateFiles(dir, "*." + extension, SearchOption.AllDirectories)
                .ToList();
        }
        private bool IsTestProject(string path)
        {
            XDocument projDef = XDocument.Load(path);
            IEnumerable<string> nodes = projDef
                .Element("Project")
                .Elements("ItemGroup")
                .Elements("PackageReference")
                .Attributes("Include")
                .Select(x => x.Value);
            return nodes.Contains("NUnit");
        }

        public Task<bool> IsPlagiarism(SolutionFileDto solutionFile)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UnzipByteAsync(string path, byte[] content)
        {
            string dir = path.Replace(".zip", "");
            await File.WriteAllBytesAsync(path, content);
            ZipFile.ExtractToDirectory(dir, path);
            File.Delete(path);
            return dir;
        }
    }
}

