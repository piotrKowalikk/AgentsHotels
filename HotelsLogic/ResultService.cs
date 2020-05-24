using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HotelsLogic
{
    public class ResultService : IResultService
    {
        public static ResultService ResultServiceInstance { get; private set; } = new ResultService();
        public static string ResultsPath;
        FileSystemWatcher watcher;

        static ResultService()
        {
            ResultsPath = (Directory.GetCurrentDirectory() + @"\\Results").Replace(@"\\", @"\");
        }

        public Task WaitForResult(FileSystemEventHandler onCreate)
        {

            if (!Directory.Exists(ResultsPath))
            {
                Directory.CreateDirectory(ResultsPath);
            }

            var tcs = new TaskCompletionSource<bool>();
            watcher = new FileSystemWatcher();

            watcher.Path = ResultsPath;
            watcher.Created += onCreate;
            //watcher.Changed += onCreate;
            watcher.EnableRaisingEvents = true;

            return tcs.Task;
        }

        public List<SearchResult> GetResultsFromFile(string path)
        {
            List<SearchResult> results = new List<SearchResult>();
            //watcher.EnableRaisingEvents = false;
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string fileContent = reader.ReadToEnd();

                    try
                    {
                        results = JsonConvert.DeserializeObject<List<SearchResult>>(fileContent);

                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }
            catch
            {

            }
            //watcher.EnableRaisingEvents = true;

            return results;
        }

        public void CleanResultsFolder()
        {
            try
            {
                string[] filesToClean = Directory.GetFiles(ResultsPath);

                foreach (string path in filesToClean)
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {
                //xd
            }
        }

    }
}
