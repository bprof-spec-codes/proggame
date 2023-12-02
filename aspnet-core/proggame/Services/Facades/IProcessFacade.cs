namespace proggame.Services.Facades
{
    public interface IProcessFacade
    {
        void RunProcess(string file, string arg);
        void RunProcessMultiArgs(string file, string[] args);
    }
}