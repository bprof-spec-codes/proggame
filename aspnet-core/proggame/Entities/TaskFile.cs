using Volo.Abp.Domain.Entities;

namespace proggame.Entities
{
    public class TaskFile : FileBlob
    {
        public TaskFile(string name, byte[] content) : base(name, content)
        {
        }
    }
}
