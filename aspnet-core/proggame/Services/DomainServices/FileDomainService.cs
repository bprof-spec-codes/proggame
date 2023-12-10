using proggame.Services.Dtos.SeparatedFilesDto;
using proggame.Services.Dtos.SolutionFileDtos;
using proggame.Services.Dtos.TestFileDtos;
using proggame.Services.Facades;
using System;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using Volo.Abp.Domain.Services;

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
            throw new NotImplementedException();
        }
        public async Task<string> JoinAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<double> RunTestsAsync(string path)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> IsPlagiarism(SolutionFileDto solutionFile)
        {
            throw new NotImplementedException();
        }
        public async Task<string?[]> SeparateAsyncFromZippedContent(string extractedPath)
        {
            // Ellenőrizzük, hogy az elérési út érvényes-e
            if (string.IsNullOrEmpty(extractedPath) || !Directory.Exists(extractedPath))
            {
                throw new ArgumentException("Érvénytelen elérési út.");
            }

            // Keresünk .sln fájlt az extrahált mappában
            var slnFiles = Directory.GetFiles(extractedPath, "*.sln", SearchOption.TopDirectoryOnly);

            // Ellenőrizzük, hogy van-e .sln fájl
            if (slnFiles.Length == 0)
            {
                throw new InvalidOperationException("Nem található .sln fájl az extrahált mappában.");
            }

            // Külön mappa a projekt fájloknak
            var taskFolderPath = Path.Combine(extractedPath, "Tasks");
            Directory.CreateDirectory(taskFolderPath);

            // Külön mappa a tesztfájloknak
            var testFolderPath = Path.Combine(extractedPath, "Tests");
            Directory.CreateDirectory(testFolderPath);

            //Másoláss
            CopyFiles(extractedPath, testFolderPath, taskFolderPath);
            string?[] paths = { slnFiles[0], taskFolderPath, testFolderPath };
            return paths;
        }
        public void WriteIdToSlnFile(string solutionPath, Guid newId)
        {
            // Ellenőrizzük, hogy az elérési út érvényes-e
            if (string.IsNullOrEmpty(solutionPath) || !File.Exists(solutionPath))
            {
                throw new ArgumentException("Érvénytelen elérési út a .sln fájlhoz.");
            }

            // Olvassuk be a .sln fájl tartalmát
            string slnContent = File.ReadAllText(solutionPath);

            // Keresünk a projektazonosítót tartalmazó sort
            string pattern = @"Project\(""{(.*?)}""\)";
            Match match = Regex.Match(slnContent, pattern);
            if (match.Success)
            {
                // Az új ID beillesztése a projektazonosítóba
                string oldId = match.Groups[1].Value;
                string newProjectId = $"Project(\"{{{newId}}}')";
                slnContent = slnContent.Replace(oldId, newProjectId);
            }
            else
            {
                throw new InvalidOperationException("Nem található projektazonosító a .sln fájlban.");
            }

            // Írjuk vissza a módosított tartalmat a .sln fájlba
            File.WriteAllText(solutionPath, slnContent);
        }
        public byte[] ZipDirectory(string directoryPath)
        {
            // Ellenőrizzük, hogy az elérési út érvényes-e?
            if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            {
                throw new ArgumentException("Érvénytelen elérési út a könyvtárhoz.");
            }

            // Az új ZIP fájl elérési útja
            var zipFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.zip");

            try
            {
                // Tömörítsük a könyvtárat a ZIP fájlba
                ZipFile.CreateFromDirectory(directoryPath, zipFilePath);

                // Olvassuk be a ZIP fájl tartalmát byte tömbbe
                return File.ReadAllBytes(zipFilePath);
            }
            finally
            {
                // Töröljük a létrehozott ZIP fájlt
                File.Delete(zipFilePath);
            }
        }
        public async Task<string> ExtractZipAsync(byte[] zipContent)
        {
            try
            {
                using (var memoryStream = new MemoryStream(zipContent))
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
                {
                    var extractionDirectory = Path.Combine(Path.GetTempPath(), "TEST");
                    Directory.CreateDirectory(extractionDirectory);
                    // Az első (esetlegesen egyetlen) fájl kibontása
                    if (archive.Entries.Count > 0)
                    {
                        var entry = archive.Entries[0];

                        // Egyedi, ideiglenes fájlnevet generál
                        var extractionPath = Path.Combine(extractionDirectory, Guid.NewGuid().ToString());

                        // Fájl kibontása
                        archive.ExtractToDirectory(extractionPath);

                        return extractionPath;
                    }

                    // Ha a zip üres, vagy nincs tartalom
                    throw new ArgumentException("A zip fájl üres vagy nem tartalmaz fájlt.");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // Log the detailed error information
                Console.WriteLine($"UnauthorizedAccessException: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Rethrow the exception to propagate it up the call stack
                throw;
            }
        }
        private void CopyFiles(string mainPath, string testPath, string taskPath)
        {
            // Ellenőrizzük, hogy a célmappák léteznek-e, ha nem, létrehozzuk
            if (!Directory.Exists(testPath))
            {
                Directory.CreateDirectory(testPath);
            }

            if (!Directory.Exists(taskPath))
            {
                Directory.CreateDirectory(taskPath);
            }

            // Végigiterálunk az összes fájlon és almappán a forrás elérési útvonalon
            foreach (string filePath in Directory.GetFiles(mainPath, "*", SearchOption.AllDirectories))
            {
                bool containsNUnit = FileContainsNUnit(filePath);
                // Ellenőrizzük, hogy a fájl tartalmazza-e a "NUnit" szöveget
                if (containsNUnit)
                {
                    // Az új elérési útvonal meghatározása a cél mappán belül
                    string targetFilePath = Path.Combine(testPath, Path.GetRelativePath(mainPath, filePath));

                    // Ellenőrizzük, hogy a cél mappa létezik-e, ha nem, létrehozzuk
                    string targetDirectory = Path.GetDirectoryName(targetFilePath);
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }

                    // Másoljuk át a fájlt a forrásról a célra
                    File.Copy(filePath, targetFilePath, true);
                }
                else
                {
                    // Az új elérési útvonal meghatározása a másik cél mappán belül
                    string targetFilePath = Path.Combine(taskPath, Path.GetRelativePath(mainPath, filePath));

                    // Ellenőrizzük, hogy a cél mappa létezik-e, ha nem, létrehozzuk
                    string targetDirectory = Path.GetDirectoryName(targetFilePath);
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }

                    // Másoljuk át a fájlt a forrásról a célra
                    File.Copy(filePath, targetFilePath, true);
                }
            }
        }

        private bool FileContainsNUnit(string filePath)
        {
            foreach (string line in File.ReadLines(filePath))
            {
                if (line.Contains("NUnit", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public void DeleteDirectory(string path)
        {
            // Ellenőrizzük, hogy az elérési út érvényes-e
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                throw new ArgumentException("Érvénytelen elérési út a törlendő könyvtárhoz.");
            }
            try
            {
                // Töröljük a könyvtárat
                Directory.Delete(path, true);
            }
            catch (Exception ex)
            {
                // További hibakezelés
                throw new ApplicationException($"Hiba történt a könyvtár törlésekor: {ex.Message}", ex);
            }
        }
    }
}
