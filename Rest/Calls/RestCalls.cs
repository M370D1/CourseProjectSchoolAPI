
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RestSharp;
using BackEndAutomation.Utilities;

namespace BackEndAutomation.Rest.Calls
{
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
            var queryParams = new Dictionary<string, object>
            {
                { "username", username },
                { "password", password }
            };

            return UtilitiesMethods.ExecuteRequest(_client, "/auth/login", Method.Post, queryParams, isMultipart: true);
        }

        public RestResponse CraeteUserCall(string username, string password, string role, string token)
        {
            var queryParams = new Dictionary<string, object>
            {
                { "username", username },
                { "password", password },
                { "role", role }
            };

            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            return UtilitiesMethods.ExecuteRequest(_client, "/users/create", Method.Post, queryParams, headers);
        }

        public RestResponse CraeteClassCall(string classname, string subject_1, string subject_2, string subject_3, string token)
        {
            var queryParams = new Dictionary<string, object>
            {
                { "class_name", classname },
                { "subject_1", subject_1 },
                { "subject_2", subject_2 },
                { "subject_3", subject_3 }
            };

            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            return UtilitiesMethods.ExecuteRequest(_client, "/classes/create", Method.Post, queryParams, headers);
        }

        public RestResponse AddStudentCall(string studentName, string class_id, string token)
        {
            var queryParams = new Dictionary<string, object>
            {
                { "name", studentName },
                { "class_id", class_id }
            };

            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            return UtilitiesMethods.ExecuteRequest(_client, "/classes/add_student", Method.Post, queryParams, headers);
        }

        public RestResponse AddGradeCall(int grade, string student_id, string subject, string token)
        {
            var queryParams = new Dictionary<string, object>
            {
                { "grade", grade },
                { "student_id", student_id },
                { "subject", subject }
            };

            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            return UtilitiesMethods.ExecuteRequest(_client, "/grades/add", Method.Put, queryParams, headers);
        }

        public RestResponse ConnectParentCall(string parent_username, string student_id, string token)
        {
            var queryParams = new Dictionary<string, object>
            {
                { "parent_username", parent_username },
                { "student_id", student_id }
            };

            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            return UtilitiesMethods.ExecuteRequest(_client, "/users/connect_parent", Method.Put, queryParams, headers);
        }

        public RestResponse ViewGradesCall(string student_id, string token)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            return UtilitiesMethods.ExecuteRequest(_client, $"/grades/student/{student_id}", Method.Get, headers: headers);
        }
    }
}
