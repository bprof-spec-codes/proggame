using Volo.Abp.Application.Dtos;

namespace proggame.Services.Dtos.TestFileDtos
{
    public class TestFileDto : AuditedEntityDto<string>
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
