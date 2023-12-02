using Volo.Abp.DependencyInjection;

namespace proggame.Services.Facades
{
    public class ProcessFacade : ISingletonDependency, IProcessFacade
    {
        public void RunProcessMultiArgs(string file, string[] args)
        {
            throw new NotImplementedException();
        }
        public void RunProcess(string file, string arg)
        {
            throw new NotImplementedException();
        }
        private string GetPath(string path)
        {
            throw new NotImplementedException();
        }
    }
}
