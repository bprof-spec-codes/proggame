using Volo.Abp.Domain.Entities;

namespace proggame.Entities
{
    public class TaskFile : FileBlob
    {
        public string Description { get; set; }
        public TaskFile(string name, byte[] content, Guid id, string description) : base(name, content)
        {
            Id = id;
            Description = description;
        }
    }
}
