using Volo.Abp.Domain.Entities;

namespace proggame.Entities
{
    public class TestFile : Entity<Guid>
    {
        public Guid TaskId { get; set; }
    }
}
