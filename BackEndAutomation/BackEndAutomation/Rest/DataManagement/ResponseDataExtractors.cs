using System;
using Newtonsoft.Json.Linq;

namespace BackEndAutomation.Rest.DataManagement;
public class ResponseDataExtractors
{
    public string Extractor(string jsonResponse, string jsonIdentifier)
    {
        try
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject[jsonIdentifier]?.ToString() ?? throw new Exception($"No {jsonIdentifier} found");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting {jsonIdentifier}: {ex.Message}");
            return $"Error extracting {jsonIdentifier}";
        }
    }

    public string ExtractAllGrades(string jsonResponse, string arrayKey = "grades", string propertyKey = "grade")
    {
        try
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            JArray gradesArray = (JArray)jsonObject[arrayKey];

            if (gradesArray == null || !gradesArray.Any())
                throw new Exception("Grades array not found or empty in response.");

            List<string> allGrades = gradesArray
                .Select(item => item[propertyKey]?.ToString())
                .Where(g => !string.IsNullOrEmpty(g))
                .ToList();

            if (!allGrades.Any())
                throw new Exception("No grades extracted from array.");

            return string.Join(",", allGrades);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting grades: {ex.Message}");
            return null;
        }
    }

}