using System.ComponentModel.DataAnnotations;

namespace proggame.Services.Dtos.TaskFileDtos
{
    public class CreateUpdateTaskFileDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] Content { get; set; }
    }
}
