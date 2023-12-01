using proggame.Entities;
using proggame.Services.DomainServices;
using proggame.Services.Dtos.SolutionFileDtos;
using proggame.Services.Dtos.TaskFileDtos;
using Scriban.Runtime.Accessors;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace proggame.Services.AppServices
{
    public class TaskFileAppService : ApplicationService
    {
        private readonly FileDomainService _fileService;
        private readonly IRepository<TaskFile, Guid> _taskFileRepository;
        private readonly IRepository<TestFile, Guid> _testFileRepository;

        public TaskFileAppService(
            FileDomainService fileService,
            IRepository<TaskFile, Guid> taskFileRepository,
            IRepository<TestFile, Guid> testFileRepository)
        {
            _fileService = fileService;
            _taskFileRepository = taskFileRepository;
            _testFileRepository = testFileRepository;
        }
        public async Task<TaskFile> UploadEmptySolutionAsync(TaskFileDto input)
        {
            /*                           \
            kizippeli,                    \
            szeparálja,                    > SolutionService
            generál egy új ID-t neki,     /
            beleírja az SLN file elejébe,/
            bezippeli a külön fileokat és eltárolja a db-ben
            */
            throw new NotImplementedException();
        }

        public async Task DeleteEmptySolutionAsync(Guid id)
        {
            //kitörli a taskot és a testeket is
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmptyTaskFileDto>> ReadAllAsync()
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
