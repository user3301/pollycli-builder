using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileIOProcessor.Interfaces
{
    public interface IFileIOProcessor
    {
        Task<List<EmployeeDetails>> ReadAsync(string filePath);
    }
}
