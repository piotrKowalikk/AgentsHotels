using System.IO;
using System.Threading.Tasks;

namespace HotelsLogic
{
    public interface IResultService
    {
        public Task WaitForResult(FileSystemEventHandler onCreate);
    }
}
