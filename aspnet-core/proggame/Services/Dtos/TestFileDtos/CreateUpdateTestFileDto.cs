using System.ComponentModel.DataAnnotations;

namespace proggame.Services.Dtos.TaskFileDtos
{
    public class CreateUpdateTestFileDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] Content { get; set; }
    }
}
