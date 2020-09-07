using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HotelsLogic
{
    public class ResultService : IResultService
    {
        public static ResultService ResultServiceInstance { get; private set; } = new ResultService();
        public static string ResultsPath;
        public static List<string> SearchOrders;
        FileSystemWatcher watcher;

        static ResultService()
        {
            ResultsPath = (Directory.GetCurrentDirectory() + @"\\Results").Replace(@"\\", @"\");
            SearchOrders = new List<string>();
            SearchOrders.Add((Directory.GetCurrentDirectory() + @"\\BookingSearch").Replace(@"\\", @"\"));
            SearchOrders.Add((Directory.GetCurrentDirectory() + @"\\TrivagoSearch").Replace(@"\\", @"\"));
            SearchOrders.Add((Directory.GetCurrentDirectory() + @"\\HotelsSearch").Replace(@"\\", @"\"));
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
            watcher.EnableRaisingEvents = true;

            return tcs.Task;
        }

        public async Task<List<SearchResult>> GetResultsFromFile(string path)
        {
            watcher.EnableRaisingEvents = false;
            List<SearchResult> results = new List<SearchResult>();
            await Task.Delay(1000);
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string fileContent = reader.ReadToEnd();

                    try
                    {
                        results.Add( JsonConvert.DeserializeObject<SearchResult>(fileContent));

                    }
                    catch (System.Exception e)
                    {
                        throw;
                    }
                }
            }
            catch
            {

            }
            watcher.EnableRaisingEvents = true;

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

        public void CleanSearchOrders()
        {
            foreach(var path in SearchOrders)
            {
                try
                {
                    string[] filesToClean = Directory.GetFiles(path);

                    foreach (string filepath in filesToClean)
                    {
                        try
                        {
                            File.Delete(filepath);
                        }
                        catch
                        {

                        }
                    }
                }
                catch
                {
                    //xddd
                }
            }
        }
    }
}
