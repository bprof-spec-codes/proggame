using proggame.Entities;
using proggame.Services.DomainServices;
using proggame.Services.Dtos.SolutionFileDtos;
using proggame.Services.Dtos.TaskFileDtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace proggame.Services.AppServices
{
    public class SolutionFileAppService : ApplicationService
    {
        private readonly IRepository<SolutionFile, Guid> _solutionFileRepository;
        private readonly FileDomainService _fileService;
        public SolutionFileAppService(
            IRepository<SolutionFile, Guid> solutionFileRepository,
            FileDomainService fileService)
        {
            _solutionFileRepository = solutionFileRepository;
            _fileService = fileService;
        }

        public async Task<double> CreateAsync(SolutionFileDto input)
        {
            /*
            Ez hozza létre az adatbázisban és fujttatja le a teszteket
            Az ID-t az SLN file legeléjéről olvassa ki
            */
            throw new NotImplementedException();
        }
    }
}
