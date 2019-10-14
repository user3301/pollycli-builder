using System.Threading.Tasks;

namespace FileIOService.Interfaces
{
    public interface IFileIOService<T>
    {
        Task<T> ReadAsync(string filePath);
    }
}
