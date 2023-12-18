using proggame.Entities;
using proggame.Services.DomainServices;
using proggame.Services.Dtos.SolutionFileDtos;
using proggame.Services.Dtos.TaskFileDtos;
using System.IO.Compression;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace proggame.Services.AppServices
{
    public class SolutionFileAppService : ApplicationService
    {
        private readonly IRepository<SolutionFile, Guid> _solutionFileRepository;
        private readonly FileDomainService _fileService;
        private readonly CurrentUser _currentUser;
        public SolutionFileAppService(
            IRepository<SolutionFile, Guid> solutionFileRepository,
            FileDomainService fileService,
            CurrentUser currentUser)
        {
            _solutionFileRepository = solutionFileRepository;
            _fileService = fileService;
            _currentUser = currentUser;
        }

        public async Task<double> CreateAsync(SolutionFileDto input)
        {
            /*
            Ez hozza létre az adatbázisban és fujttatja le a teszteket
            Az ID-t az SLN file legeléjéről olvassa ki
            */
            string path = await _fileService.UnzipByteAsync(input.Name, input.Content);
            string slnPath = _fileService.GetFileWithExtension(path, "sln");
            Guid id = Guid.Parse(File.ReadAllLines(slnPath)[0]);
            Guid uid = _currentUser.GetId();
            await _solutionFileRepository.InsertAsync(new SolutionFile(input.Name, input.Content, id, uid), true);
            Directory.Delete(path, true);
            path = await _fileService.JoinAsync(id);
            string zipPath = path + ".zip";
            ZipFile.CreateFromDirectory(path, zipPath);
            double result = await _fileService.RunTestsAsync(new SolutionFileDto() { Content = File.ReadAllBytes(zipPath), Name = Path.GetFileName(zipPath) });
            return result;
        }
    }
}
