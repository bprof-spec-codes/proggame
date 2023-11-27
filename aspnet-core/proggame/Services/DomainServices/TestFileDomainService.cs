using proggame.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace proggame.Services.DomainServices
{
    public class TestFileDomainService : DomainService
    {
        private readonly IRepository<TestFile, Guid> _repository;
        public TestFileDomainService(IRepository<TestFile, Guid> repository)
        {
            _repository = repository;
        }
    }
}
