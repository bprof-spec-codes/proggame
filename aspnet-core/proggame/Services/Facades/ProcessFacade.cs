using Volo.Abp.DependencyInjection;

namespace proggame.Services.Facades
{
    public class ProcessFacade : ISingletonDependency
    {
        public void RunProcessMultiArgs(string file, string[] args)
        {

        }
        public void RunProcess(string file, string arg) 
        {
            
        }
        private string GetPath(string path)
        {
            return string.Empty;
        }
    }
}
