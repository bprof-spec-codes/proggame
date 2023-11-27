using proggame.Services.Dtos.SolutionFileDtos;
using Volo.Abp.DependencyInjection;

namespace proggame.Services
{
    public class SolutionService : ISingletonDependency
    {
        private readonly ISingletonDependency _process;
        public SolutionService(ISingletonDependency process)
        {
            _process = process;
        }
        public SeparatedSolutionFileDto Separate(CreateUpdateSolutionFileDto slnZip)
        {
            return new SeparatedSolutionFileDto();
        }
        public SolutionFileDto Join(SeparatedSolutionFileDto files)
        {
            return new SolutionFileDto();
        }
    }
}
