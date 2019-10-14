﻿using FileIOService.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
                string[] csvData = rawText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
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
