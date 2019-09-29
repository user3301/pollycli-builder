using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileIOService.Interfaces
{
    public interface IFileIOService
    {
        Task<List<EmployeeDetails>> ReadAsync(string filePath);
    }
}
