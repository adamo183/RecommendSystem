using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;

namespace RecommendSystem.ReadFile
{
    public class CsvManager
    {
        public CsvConfiguration configuration { get; set; } = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };

        public List<T> ReadFromCsv<T>(string fileNameRecord)
        {
            var records = new List<T>();

            using (var reader = new StreamReader(fileNameRecord))
            using (var csv = new CsvReader(reader, configuration))
            {
                records = csv.GetRecords<T>().ToList();
            }

            return records;
        }
    }
}
