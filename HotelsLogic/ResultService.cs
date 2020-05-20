using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HotelsLogic
{
    public class ResultService : IResultService
    {
        public static ResultService ResultServiceInstance { get; private set; } = new ResultService();

        public Task WaitForResult(FileSystemEventHandler onCreate)
        {
            string path = Directory.GetCurrentDirectory() + "/Results";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var tcs = new TaskCompletionSource<bool>();
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = path;
            watcher.Created += onCreate;
            watcher.EnableRaisingEvents = true;

            return tcs.Task;
        }

        public List<SearchResult> GetResultsFromFile(string path)
        {
            List<SearchResult> results = new List<SearchResult>();

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

            return results;
        }

    }
}
