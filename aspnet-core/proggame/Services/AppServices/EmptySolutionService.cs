using proggame.Services.Dtos.SolutionFileDtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;

namespace proggame.Services.AppServices
{
    public class EmptySolutionService : ApplicationService
    {
        private readonly ISingletonDependency _solutionService;
        private readonly IDomainService _taskFileService;
        private readonly IDomainService _testFileService;

        public EmptySolutionService(
            ISingletonDependency solutionService,
            IDomainService taskFileService,
            IDomainService testFileService)
        {
            _solutionService = solutionService;
            _taskFileService = taskFileService;
            _testFileService = testFileService;
        }
        public async Task UploadEmptySolution(SolutionFileDto input)
        {
            
        }

        public async Task DeleteEmptySolution(Guid id)
        {

        }
    }
}
