using proggame.Services.Dtos.SolutionFileDtos;
using Volo.Abp.Domain.Services;

namespace proggame.Services.DomainServices
{
    public class FileDomainService : IDomainService
    {
        public async Task<SeparatedSolutionFileDto> SeparateAsync(CreateUpdateSolutionFileDto slnZip)
        {
            throw new NotImplementedException();
        }
        public async Task<SolutionFileDto> JoinAsync(SeparatedSolutionFileDto files)
        {
            throw new NotImplementedException();
        }

        public async Task<double> RunTestsAsync(SolutionFileDto solutionFile)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> IsPlagiarism(SolutionFileDto solutionFile)
        {
            throw new NotImplementedException();
        }
    }
}
