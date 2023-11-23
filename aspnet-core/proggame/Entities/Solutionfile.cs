using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace proggame.Entities
{
    public class SolutionFile : Entity<Guid>, ICreationAuditedObject
    {
        public Guid TaskId { get; set; }
        public DateTime CreationTime => throw new NotImplementedException();

        public Guid? CreatorId => throw new NotImplementedException();
    }
}
