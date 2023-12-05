using proggame.Services.Dtos.SolutionFileDtos;
using proggame.Services.Facades;
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
    }
}
