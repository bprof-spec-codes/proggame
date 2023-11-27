using proggame.Services.Dtos.TaskFileDtos;
using proggame.Services.Dtos.TestFileDtos;

namespace proggame.Services.Dtos.SolutionFileDtos
{
    public class SeparatedSolutionFileDto
    {
        public TaskFileDto Task { get; set; }
        public TestFileDto[] Tests { get; set; }
    }
}
