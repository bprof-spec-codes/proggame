using Volo.Abp.Domain.Entities;

namespace proggame.Entities
{
    public class TestFile : FileBlob
    {
        public TestFile(string name, byte[] content, Guid taskId) : base(name, content)
        {
            TaskId = taskId;
        }

        public Guid TaskId { get; set; }
    }
}
