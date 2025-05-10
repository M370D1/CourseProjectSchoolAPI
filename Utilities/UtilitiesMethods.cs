using RestSharp;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using AventStack.ExtentReports;
using NUnit.Framework;
using Reqnroll;

namespace BackEndAutomation.Utilities
{
    public class UtilitiesMethods
    {
        public static RestResponse ExecuteRequest(
            RestClient client,
            string endpoint,
            Method method,
            Dictionary<string, object>? queryParams = null,
            Dictionary<string, string>? headers = null,
            bool isMultipart = false)
        {
            var request = new RestRequest(endpoint, method);
            request.AlwaysMultipartFormData = isMultipart;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (queryParams != null)
            {
                foreach (var param in queryParams)
                {
                    if (isMultipart)
                    {
                        request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                    }
                    else
                    {
                        request.AddQueryParameter(param.Key, param.Value.ToString());
                    }
                }
            }

            var response = client.Execute(request);
            Console.WriteLine($"[API CALL] {method} {endpoint}: {response.Content}");

            return HandleResponse(response);
        }

        //private static RestResponse HandleResponse(RestResponse response)
        //{
        //    if (!response.IsSuccessful)
        //    {
        //        if (JObject.Parse(response.Content).ContainsKey(JsonIdentifierKeys.DetailKey))
        //        {
        //            return response;
        //        }

        //        Console.WriteLine($"API Error: {response.StatusCode} - {response.ErrorMessage}");
        //        Console.WriteLine($"Response Content: {response.Content}");
        //        throw new Exception($"API Request Failed: {response.StatusCode}");
        //    }

        //    return response;
        //}
        private static RestResponse HandleResponse(RestResponse response)
        {
            if (!response.IsSuccessful)
            {
                try
                {
                    var content = response.Content?.Trim();

                    // Attempt to parse JSON only if it starts with '{' or '['
                    if (!string.IsNullOrEmpty(content) &&
                        (content.StartsWith("{") || content.StartsWith("[")))
                    {
                        var json = JObject.Parse(content);
                        if (json.ContainsKey(JsonIdentifierKeys.DetailKey))
                        {
                            return response;
                        }
                    }

                    Console.WriteLine($"API Error: {response.StatusCode} - {response.ErrorMessage}");
                    Console.WriteLine($"Response Content: {response.Content}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to parse error response as JSON:");
                    Console.WriteLine(response.Content);
                    Console.WriteLine($"Parsing Exception: {ex.Message}");
                }

                throw new Exception($"API Request Failed: {response.StatusCode}");
            }

            return response;
        }


        public static void AssertEqual<T>(T expected, T actual, string message, ScenarioContext scenarioContext)
        {
            ExtentTest _test = scenarioContext.Get<ExtentTest>("ExtentTest");

            if (!EqualityComparer<T>.Default.Equals(expected, actual))
            {
                Logger.Log.Error(message + Environment.NewLine + $"Expected: {expected}, but was: {actual}");
                _test.Log(Status.Fail, message + Environment.NewLine + $"Expected: {expected}, but was: {actual}");
                Assert.Fail(message + Environment.NewLine + $"Expected: {expected}, but was: {actual}");
            }
        }

        public static void LogMessage(
            string message,
            ScenarioContext scenarioContext,
            LogStatuses status = LogStatuses.Info)
        {
            ExtentTest _test = scenarioContext.Get<ExtentTest>("ExtentTest");
            if (status == LogStatuses.Info)
            {
                _test.Log(Status.Info, message);
                Logger.Log.Info(message);
            }
            else if (status == LogStatuses.Warning)
            {
                _test.Log(Status.Warning, message);
                Logger.Log.Warn(message);
            }
            else if (status == LogStatuses.Debug)
            {
                _test.Log(Status.Info, message);
                Logger.Log.Debug(message);
            }
        }
        public static string GenerateUniqueName(string baseName)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            return $"{baseName}_{timestamp}";
        }

        public enum LogStatuses
        {
            Info,
            Warning,
            Debug
        }
    }
}