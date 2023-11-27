using proggame.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace proggame.Services.DomainServices
{
    public class TaskFileDomainService : DomainService
    {
        private readonly IRepository<TaskFile, Guid> _taskRepository;
        public TaskFileDomainService(IRepository<TaskFile, Guid> taskRepository)
        {
            _taskRepository = taskRepository;
        }
    }
}
