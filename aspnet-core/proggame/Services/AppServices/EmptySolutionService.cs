using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace proggame.Services.AppServices
{
    public class EmptySolutionService : ApplicationService
    {
        private readonly ISingletonDependency _solutionService;
        public EmptySolutionService(ISingletonDependency solutionService)
        {
            _solutionService = solutionService;
        }
        public async Task UploadEmptySolution(string name, byte[] content)
        {
            
        }

        public async Task DeleteEmptySolution(Guid id)
        {

        }
    }
}
