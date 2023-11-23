﻿using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace proggame.Entities
{
    public class SolutionFile : Entity<Guid>, ICreationAuditedObject
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }
    }
}
