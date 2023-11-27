using Volo.Abp.Application.Dtos;

namespace proggame.Services.Dtos.TaskFileDtos
{
    public class TaskFileDto : AuditedEntityDto<string>
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
