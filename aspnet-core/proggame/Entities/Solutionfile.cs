using Volo.Abp.Auditing;

namespace proggame.Entities
{
    public class SolutionFile : FileBlob, ICreationAuditedObject
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }
    }
}
