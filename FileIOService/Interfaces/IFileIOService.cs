using System.Threading.Tasks;

namespace FileIOService.Interfaces
{
    public interface IFileIOService<TIn, TOut>
    {
        Task<TOut> ReadAsync(TIn dataSource);
    }
}
