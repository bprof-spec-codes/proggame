﻿using Volo.Abp.Auditing;

namespace proggame.Entities
{
    public class SolutionFile : FileBlob, ICreationAuditedObject
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public double Point { get; set; }
        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }
        public SolutionFile(string name, byte[] content, Guid taskId, Guid userId)
            : base(name, content)
        {
            TaskId = taskId;
            UserId = userId;
            CreationTime = DateTime.Now;
        }
    }
}
