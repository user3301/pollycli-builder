using FileIOProcessor.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileIOProcessor.Implementations
{
    public class CSVFileProcessor : IFileIOProcessor
    {
        public Task<List<EmployeeDetails>> Read(in string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
