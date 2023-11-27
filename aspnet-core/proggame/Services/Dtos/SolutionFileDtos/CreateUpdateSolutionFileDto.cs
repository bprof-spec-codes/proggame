using System.ComponentModel.DataAnnotations;

namespace proggame.Services.Dtos.SolutionFileDtos
{
    public class CreateUpdateSolutionFileDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] Content { get; set; }
    }
}
