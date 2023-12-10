using proggame.Services.Dtos.SeparatedFilesDto;
using proggame.Services.Dtos.SolutionFileDtos;

namespace proggame.Services.DomainServices
{
    public interface IFileDomainService
    {
        Task<bool> IsPlagiarism(SolutionFileDto solutionFile);
        Task<string> JoinAsync(Guid id);
        Task<double> RunTestsAsync(string path);
        void SeparateAsync(string path);
        Task<string?[]> SeparateAsyncFromZippedContent(string extractedPath);
        void WriteIdToSlnFile(string solutionPath, Guid newId);
        byte[] ZipDirectory(string directoryPath);
        Task<string> ExtractZipAsync(byte[] zipContent);
        void DeleteDirectory(string path);
    }
}