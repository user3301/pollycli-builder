using FileIOProcessor.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileIOProcessor.Implementations
{
    public class CSVFileService : IFileIOService
    {
        public async Task<List<EmployeeDetails>> ReadAsync(string filePath)
        {
            var employees = new List<EmployeeDetails>();

            if (!File.Exists(filePath)) throw new FileNotFoundException();
            using (var reader = File.OpenText(filePath))
            {
                string rawText = await reader.ReadToEndAsync();
                string[] csvData = rawText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (var line in csvData)
                {
                    employees.Add(Mapper(line));
                }
            }
            return employees;

        }

        #region private methods
        private EmployeeDetails Mapper(string line)
        {
            try
            {
                var entries = line.Split(new[] { ',' }, StringSplitOptions.None);
                return new EmployeeDetails
                {
                    FirstName = entries[0],
                    LastName = entries[1],
                    AnnualSalary = uint.Parse(entries[2]),
                    SuperRate = uint.Parse(Regex.Match((entries[3]), "[0-9]*\\.*[0-9]*").Value),
                    PayPeriod = entries[4]
                };
            }
            catch (Exception e)
            {

                throw new InvalidDataException(e.Message);
            }
        }

        #endregion
    }
}
