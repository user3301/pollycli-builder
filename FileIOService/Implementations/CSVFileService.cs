using FileIOService.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileIOService.Implementations
{
    public class CSVFileService : IFileIOService<List<EmployeeDetails>>
    {
        public async Task<List<EmployeeDetails>> ReadAsync(string filePath)
        {
            var employees = new List<EmployeeDetails>();

            if (!File.Exists(filePath)) throw new FileNotFoundException();
            using (var reader = File.OpenText(filePath))
            {
                string rawText = await reader.ReadToEndAsync().ConfigureAwait(false);
                string[] csvData = rawText.Split(Environment.NewLine.ToCharArray());
                foreach (var line in csvData)
                {
                    // process non-empty line in csv
                    if (!string.IsNullOrWhiteSpace(line)) employees.Add(Mapper(line));
                }
            }
            return employees;
        }

        #region private methods
        EmployeeDetails Mapper(string line)
        {
            try
            {
                var entries = line.Split(new[] { ',' }, StringSplitOptions.None);
                if (string.IsNullOrWhiteSpace(entries[0])) throw new ArgumentException("First name cannot be null or empty.");
                if (string.IsNullOrWhiteSpace(entries[1])) throw new ArgumentException("Last name cannot be null or empty.");

                var firstName = entries[0];
                var lastName = entries[1];
                var annualSalary = uint.Parse(entries[2]);
                var superRate = uint.Parse(entries[3].Remove(entries[3].Length - 1));
                var payPeriod = entries[4];
                return new EmployeeDetails
                {
                    FirstName = firstName,
                    LastName = lastName,
                    AnnualSalary = annualSalary,
                    SuperRate = superRate,
                    PayPeriod = payPeriod
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
