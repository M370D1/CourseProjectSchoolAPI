//using System.Diagnostics;
//using System.Xml.Linq;
//using RestSharp;

//namespace BackEndAutomation.Rest.Calls
//{
//    public class RestCalls
//    {
//        public RestResponse LoginCall(string url, string username, string password, bool rememberMe = false)
//        {
//            RestClientOptions options = new RestClientOptions(url)
//            {
//                Timeout = TimeSpan.FromSeconds(120),
//            };

//            RestClient client = new RestClient(options);

//            RestRequest request = new RestRequest("/users/login", Method.Post);

//            request.AddHeader("Content-Type", "application/json");

//            string body = @"{""usernameOrEmail"":""" + username + @""",""password"":""" + password + @""",""rememberMe"":""" + rememberMe.ToString().ToLower() + @"""}";

//            request.AddStringBody(body, DataFormat.Json);

//            RestResponse response = client.Execute(request);

//            return response;
//        }

//        public RestResponse GetUserPageInformationCall(string url, string userId, string token)
//        {
//            RestClientOptions options = new RestClientOptions(url)
//            {
//                Timeout = TimeSpan.FromSeconds(120),
//            };
//            RestClient client = new RestClient(options);

//            RestRequest request = new RestRequest($"/users/{userId}", Method.Get);

//            request.AddHeader("Authorization", $"Bearer {token}");

//            RestResponse response = client.Execute(request);

//            return response;
//        }

//        public RestResponse ToFollowUser(string url, string userIdToFollow, string token, bool isFollowed)
//        {
//            string toFollowCommand = isFollowed ? "followUser" : "unfollowUser";

//            RestClientOptions options = new RestClientOptions(url)
//            {
//                Timeout = TimeSpan.FromSeconds(120),
//            };

//            RestClient client = new RestClient(options);

//            RestRequest request = new RestRequest($"/users/{userIdToFollow}", Method.Patch);

//            request.AddHeader("Content-Type", "application/json");
//            request.AddHeader("Authorization", $"Bearer {token}");

//            string body = @"{""action"":""" + toFollowCommand + @"""}";

//            request.AddStringBody(body, DataFormat.Json);

//            RestResponse response = client.Execute(request);

//            return response;
//        }

//        public void restPostman()
//        {

//            RestClientOptions options = new RestClientOptions("http://161.35.202.130:3000")
//            {
//                Timeout = TimeSpan.FromSeconds(120),
//            };
//            var client = new RestClient(options);
//            var request = new RestRequest("/users/login", Method.Post);
//            request.AddHeader("Content-Type", "application/json");
//            var body = @"{""usernameOrEmail"":""vidko.v"",""password"":""123abc"",""rememberMe"":false}";
//            request.AddStringBody(body, DataFormat.Json);
//            RestResponse response = client.Execute(request);
//            Console.WriteLine(response.Content);
//        }

//        public RestResponse generalRestCall(
//            string baseUrl,
//            string endpoint,
//            Method method
//            //string ParametersType = "",
//            //Dictionary<string, string> paramters = Dictionary<string, string>()
//            )
//        {
//            RestClientOptions options = new RestClientOptions(baseUrl)
//            {
//                Timeout = TimeSpan.FromSeconds(120),
//            };
//            RestClient client = new RestClient(options);
//            RestRequest request = new RestRequest(endpoint, method);
//            //if (ParametersType != string.Empty)
//            //{
//            //    foreach (KeyValuePair<string, string> param in paramters)
//            //    {
//            //        request.AddParameter(param.Key, param.Value);
//            //    }
//            //}
//            RestResponse response = client.Execute(request);

//            return response;
//        }
///////////////////////////////////////////////////////////////////////////////////////////
//        public RestResponse SignInUserCall(string username, string password)
//        {
//            var options = new RestClientOptions("https://schoolprojectapi.onrender.com")
//            {
//                Timeout = TimeSpan.FromSeconds(120),
//            };
//            var client = new RestClient(options);
//            var requestSignIn = new RestRequest("/auth/login", Method.Post);
//            requestSignIn.AlwaysMultipartFormData = true;
//            requestSignIn.AddParameter("username", username);
//            requestSignIn.AddParameter("password", password);
//            RestResponse response = client.Execute(requestSignIn);
//            Console.WriteLine(response.Content);
//            return response;
//        }

//        public RestResponse CraeteTeacherCall(string username, string password, string role, string token)
//        {
//            var options = new RestClientOptions("https://schoolprojectapi.onrender.com")
//            {
//                Timeout = TimeSpan.FromSeconds(120),
//            };
//            var client = new RestClient(options);
//            var requestCreateTeacher = new RestRequest($"/users/create" +
//                $"?username={username}" +
//                $"&password={password}" +
//                $"&role={role}", 
//                Method.Post);
//            requestCreateTeacher.AlwaysMultipartFormData = true;
//            requestCreateTeacher.AddHeader("Authorization", $"Bearer {token}");
//            RestResponse response = client.Execute(requestCreateTeacher);
//            Console.WriteLine(response.Content);
//            return response;
//        }
//    }
//}

using System;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace BackEndAutomation.Rest.Calls;
public class RestCalls
{
    private readonly RestClient _client;

    public RestCalls()
    {
        var options = new RestClientOptions("https://schoolprojectapi.onrender.com")
        {
            Timeout = TimeSpan.FromSeconds(120)
        };
        _client = new RestClient(options);
    }

    public RestResponse SignInUserCall(string username, string password)
    {
        var request = new RestRequest("/auth/login", Method.Post);

        request.AlwaysMultipartFormData = true; // Switch to form-data
        request.AddParameter("username", username);
        request.AddParameter("password", password);

        RestResponse response = _client.Execute(request);
        Console.WriteLine($"SignIn Response: {response.Content}");

        return HandleResponse(response);
    }

    public RestResponse CraeteTeacherCall(string username, string password, string role, string token)
    {
        var request = new RestRequest($"/users/create", Method.Post);
        request.AddHeader("Authorization", $"Bearer {token}");

        // API expects query parameters, not form-data or JSON
        request.AddQueryParameter("username", username);
        request.AddQueryParameter("password", password);
        request.AddQueryParameter("role", role);

        RestResponse response = _client.Execute(request);
        Console.WriteLine($"CreateTeacher Response: {response.Content}");

        return HandleResponse(response);
    }


    private RestResponse HandleResponse(RestResponse response)
    {
        if (!response.IsSuccessful)
        {
            Console.WriteLine($"API Error: {response.StatusCode} - {response.ErrorMessage}");
            throw new Exception($"API Request Failed: {response.StatusCode}");
        }

        return response;
    }
}
