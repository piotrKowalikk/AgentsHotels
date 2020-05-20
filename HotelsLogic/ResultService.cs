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

    }
}
