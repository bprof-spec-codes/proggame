using Volo.Abp.Application.Dtos;

namespace proggame.Services.Dtos.TaskFileDtos
{
    public class TaskFileListDto : AuditedEntityDto<string>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
