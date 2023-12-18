using Volo.Abp.Application.Dtos;

namespace proggame.Services.Dtos.TaskFileDtos
{
    public class TaskFileDto
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Description { get; set; }
    }
}
