using proggame.Services.Dtos.SolutionFileDtos;

namespace proggame.Services.DomainServices
{
    public interface IFileDomainService
    {
        Task<bool> IsPlagiarism(SolutionFileDto solutionFile);
        Task<string> JoinAsync(Guid id);
        Task<double> RunTestsAsync(SolutionFileDto solutionFile);

        void SeparateAsync(string path);
    }
}