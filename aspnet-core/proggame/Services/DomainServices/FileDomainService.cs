using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using proggame.Services.Dtos;
using Volo.Abp.Domain.Services;
using proggame.Services.Facades;
using proggame.Services.Dtos.SolutionFileDtos;
using Microsoft.CodeAnalysis;
using System.Xml.Linq;

namespace proggame.Services.DomainServices
{
    public class FileDomainService : IDomainService, IFileDomainService
    {
        private readonly IProcessFacade _processFacade;

        public FileDomainService(IProcessFacade processFacade)
        {
            _processFacade = processFacade;
        }
        public async void SeparateAsync(string path)
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
            throw new NotImplementedException();
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

        private string GetFileWithExtension(string dir, string extension)
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

        public Task<string> UnzipByteAsync(string path, byte[] content)
        {
            throw new NotImplementedException();
        }
    }
}
