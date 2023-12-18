using Microsoft.AspNetCore.Mvc;
using proggame.Data;
using proggame.Entities;
using proggame.Services.DomainServices;
using proggame.Services.Dtos.SolutionFileDtos;
using proggame.Services.Dtos.TaskFileDtos;
using Scriban.Runtime.Accessors;
using System.Linq.Expressions;
using System.IO.Compression;
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
        }
        public async Task UploadEmptySolutionAsync(TaskFileDto input)
        {
            /*                           \
            kizippeli,                    \
            szeparálja,                    > SolutionService
            generál egy új ID-t neki,     /
            beleírja az SLN file elejébe,/
            bezippeli a külön fileokat és eltárolja a db-ben
            */
            string path = await _fileService.UnzipByteAsync(input.Name, input.Content);
            _fileService.SeparateAsync(path);
            Guid id = Guid.NewGuid();
            string slnPath = _fileService.GetFileWithExtension(path, "sln");
            string[] lines = File.ReadAllLines(slnPath);
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(id);
            foreach (string line in lines) sw.WriteLine(line);
            sw.Close();
            string[] zips = Directory.GetFiles(path, "*.zip", SearchOption.TopDirectoryOnly);
            List<TestFile> files = new List<TestFile>();
            foreach (string zip in zips)
            {
                files.Add(new TestFile(Path.GetFileName(zip), File.ReadAllBytes(zip), id));
                File.Delete(zip);
            }
            string zipPath = path + ".zip";
            ZipFile.CreateFromDirectory(path, zipPath);
            TaskFile task = new TaskFile(Path.GetFileName(zipPath), File.ReadAllBytes(zipPath), id, input.Description);
            File.Delete(zipPath);
            await _taskFileRepository.InsertAsync(task, true);
            foreach(TestFile file in files) _testFileRepository.InsertAsync(file, true);
            Results.Ok();
        }


        public async Task DeleteEmptySolutionAsync(Guid id)
        {
            //kitörli a taskot és a testeket is
            _taskFileRepository.DeleteAsync(id);
            var tests = _testFileRepository.GetDbSet().Where(x => x.TaskId == id).Select(x => x.Id);
            _testFileRepository.DeleteManyAsync(tests);
            Results.Ok(id);
        }

        public async Task<IEnumerable<TaskFileListDto>> ReadAllAsync()
        {
            //visszaadja az összes taskot kontent nélkül
            List<TaskFile> list = await _taskFileRepository.GetListAsync();
            List<TaskFileListDto> result = new List<TaskFileListDto>();
            foreach (var file in list)
            {
                result.Add(new TaskFileListDto()
                {
                    Description = file.Description,
                    Id = file.Id,
                    Name = file.Name
                });
            }
            return result;
        }

        public async Task<TaskFileDto> ReadAsync(Guid id)
        {
            //visszaad egy üres Taskot kontenttel
            TaskFile task = await _taskFileRepository.GetAsync(id);
            TaskFileDto result = new TaskFileDto()
            {
                Content = task.Content,
                Name = task.Name
            };
            return result;
        }
    }
}
