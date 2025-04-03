//using Newtonsoft.Json.Linq;

//namespace BackEndAutomation.Rest.DataManagement
//{
//    public class ResponseDataExtractors
//    {

//        public string ExtractLoggedInUserToken(string jsonResponse, string jsonIdentfier = "token")
//        {
//            JObject jsonObject = JObject.Parse(jsonResponse);
//            return jsonObject[jsonIdentfier]?.ToString();
//        }

//        public int ExtractUserId(string jsonResponse)
//        {
//            var jsonObject = JObject.Parse(jsonResponse);
//            return jsonObject["user"]?["id"]?.Value<int>() ?? 0;
//        }

//        public string ExtractStockMessage(string jsonResponse)
//        {
//            JObject jsonObject = JObject.Parse(jsonResponse);
//            return jsonObject["message"]?.ToString();

//        }
//        //////////////////////////////////////////
//        public string ExtractMessage(string jsonResponse)
//        {
//            JObject jsonObject = JObject.Parse(jsonResponse);
//            return jsonObject["message"]?.ToString();

//        }
//    }
//}

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

    public string ExtractLoggedInUserToken(string jsonResponse, string jsonIdentifier = "token")
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
}
