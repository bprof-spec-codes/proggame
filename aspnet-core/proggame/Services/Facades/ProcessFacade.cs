using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace proggame.Services.Facades
{
    public class ProcessFacade : ISingletonDependency, IProcessFacade
    {
        public void RunProcessMultiArgs(string file, string[] args)
        {
            string filePath = GetPath(file);
            if (filePath == null)
            {
                throw new FileNotFoundException($"The file '{file}' was not found in the PATH.");
            }

            using (Process process = new Process())
            {
                process.StartInfo.FileName = filePath;

                
                process.StartInfo.ArgumentList.Clear();
                foreach (var arg in args)
                {
                    process.StartInfo.ArgumentList.Add(arg);
                }

                process.StartInfo.CreateNoWindow = true;
                process.Start();

                process.WaitForExit();
            }
        }

        public void RunProcess(string file, string arg)
        {
            RunProcessMultiArgs(file, new string[] { arg });
        }

        private string GetPath(string fileName)
        {
            var paths = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in paths.Split(Path.PathSeparator))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }

            return null;
        }
    }
}
