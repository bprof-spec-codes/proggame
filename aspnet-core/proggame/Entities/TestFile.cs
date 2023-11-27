using Volo.Abp.Domain.Entities;

namespace proggame.Entities
{
    public class TestFile : FileBlob
    {
        public Guid TaskId { get; set; }
    }
}
