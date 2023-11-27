using Volo.Abp.Domain.Entities;

namespace proggame.Entities
{
    public class FileBlob : Entity<Guid>
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
