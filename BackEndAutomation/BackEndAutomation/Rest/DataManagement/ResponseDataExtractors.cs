using System;
using Newtonsoft.Json.Linq;

namespace BackEndAutomation.Rest.DataManagement;
public class ResponseDataExtractors
{
    public string ExtractMessage(string jsonResponse)
    {
        try
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject["message"]?.ToString() ?? "No message found";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting message: {ex.Message}");
            return "Error extracting message";
        }
    }

    public string ExtractSignedInUserToken(string jsonResponse, string jsonIdentifier = "token")
    {
        try
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject[jsonIdentifier]?.ToString() ?? throw new Exception("Token not found in response");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting token: {ex.Message}");
            return null;
        }
    }

    public string ExtractClassID(string jsonResponse, string jsonIdentifier = "class_id")
    {
        try
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject[jsonIdentifier]?.ToString() ?? throw new Exception("Class ID not found in response");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting class ID: {ex.Message}");
            return null;
        }
    }

    public string ExtractStudentID(string jsonResponse, string jsonIdentifier = "student_id")
    {
        try
        {
            JObject jsonObject = JObject.Parse(jsonResponse);
            return jsonObject[jsonIdentifier]?.ToString() ?? throw new Exception("Student ID not found in response");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting Student ID: {ex.Message}");
            return null;
        }
    }
}
