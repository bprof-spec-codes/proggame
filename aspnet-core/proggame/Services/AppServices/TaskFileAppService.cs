using Microsoft.AspNetCore.Mvc;
using proggame.Data;
using proggame.Entities;
using proggame.Services.DomainServices;
using proggame.Services.Dtos.SolutionFileDtos;
using proggame.Services.Dtos.TaskFileDtos;
using Scriban.Runtime.Accessors;
using System.Linq.Expressions;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace proggame.Services.AppServices
{
    public class TaskFileAppService : ApplicationService
    {
        private readonly IFileDomainService _fileService;
        private readonly IRepository<SolutionFile, Guid> _solutionFileReporitoy;
        private readonly IRepository<TaskFile, Guid> _taskFileRepository;
        private readonly IRepository<TestFile, Guid> _testFileRepository;
        private readonly proggameDbContext _context;
        public TaskFileAppService(
            IFileDomainService fileService,
            IRepository<SolutionFile, Guid> solutionFileReporitoy,
            IRepository<TaskFile, Guid> taskFileRepository,
            IRepository<TestFile, Guid> testFileRepository,
            SolutionFileAppService solutionFileAppService,
            proggameDbContext context)


        {
            _fileService = fileService;
            _solutionFileReporitoy = solutionFileReporitoy;
            _taskFileRepository = taskFileRepository;
            _testFileRepository = testFileRepository;
            _solutionFileReporitoy = solutionFileReporitoy;
            _context = context;

        }
        [Consumes("multipart/form-data")]
        [DisableRequestSizeLimit]
        public async Task<TaskFile> UploadEmptySolutionAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("No file uploaded");
            }
            byte[] fileContent;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileContent = memoryStream.ToArray();
            }
            // 1: Zip kicsomagolás
            var extractedPath = await _fileService.ExtractZipAsync(fileContent);

            try
            {
                // 2: Solution és feladatok/tesztek szeparálása
                var separatedPath = await _fileService.SeparateAsyncFromZippedContent(extractedPath);

                // 3: ID generálás
                var newId = Guid.NewGuid();

                // 4: Solution filehoz hozzádjuk az ID-t
                _fileService.WriteIdToSlnFile(separatedPath[0], newId);

               

                // 5: Feladatok becsomagolása zipbe -> repohoz hozzáadás
                var pathToTaskFiles = Path.Combine(separatedPath[1]);
                var zippedTaskFile = _fileService.ZipDirectory(pathToTaskFiles);
                if (zippedTaskFile == null)
                {
                    throw new ArgumentNullException("A Feladatok mappa üres");
                }
                var taskFile = new TaskFile(newId.ToString(), zippedTaskFile);
                await _taskFileRepository.InsertAsync(taskFile);
                await _context.Tasks.AddAsync(taskFile);
                await _context.SaveChangesAsync(true);
                try
                {
                    // 6: Tesztek becsomagolása zipbe -> repohoz hozzáadás
                    var pathToTestFiles = Path.Combine(separatedPath[2]);
                    var zippedTestFiles = _fileService.ZipDirectory(pathToTestFiles);
                    if (zippedTestFiles == null) 
                    {
                        throw new ArgumentNullException("A Tesztek mappa üres");
                    }

                    var testFile = new TestFile(newId.ToString(), zippedTestFiles, taskFile.Id);
                    testFile.Task = taskFile;
                    await _testFileRepository.InsertAsync(testFile);
                    await _context.Tests.AddAsync(testFile);
                    await _context.SaveChangesAsync(true);

                    //Solution elmentése
                    var slnContent = File.ReadAllBytes(separatedPath[0]);
                    var userId = Guid.NewGuid();
                    if (CurrentUser.Id != null)
                    {
                        userId = (Guid)CurrentUser.Id;
                    }
                    var newSlnFile = new SolutionFile(newId.ToString(), slnContent, taskFile.Id, userId);

                    await _solutionFileReporitoy.InsertAsync(newSlnFile);
                    await _context.Solution.AddAsync(newSlnFile);
                    await _context.SaveChangesAsync(true);

                    return taskFile;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt a feltöltés közben: {ex.Message}");
                    throw;
                }
            }
            finally
            {
                // Törlés
                _fileService.DeleteDirectory(extractedPath);
            }
        }


        public async Task DeleteEmptySolutionAsync(Guid id)
        {
            //kitörli a taskot és a testeket is
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskFileListDto>> ReadAllAsync()
        {
            //visszaadja az összes taskot kontent nélkül
            throw new NotImplementedException();
        }

        public async Task<TaskFileDto> ReadAsync(Guid id)
        {
            //visszaad egy üres Taskot kontenttel
            throw new NotImplementedException();
        }
    }
}
