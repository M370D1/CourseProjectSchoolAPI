﻿using System;
using System.Reactive.Subjects;
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

    public RestResponse CraeteUserCall(string username, string password, string role, string token)
    {
        var request = new RestRequest($"/users/create", Method.Post);
        request.AddHeader("Authorization", $"Bearer {token}");

        // API expects query parameters, not form-data or JSON
        request.AddQueryParameter("username", username);
        request.AddQueryParameter("password", password);
        request.AddQueryParameter("role", role);

        RestResponse response = _client.Execute(request);
        Console.WriteLine($"CreateUser {role} Response: {response.Content}");

        return HandleResponse(response);
    }

    public RestResponse CraeteClassCall(string classname, string subject_1, string subject_2, string subject_3, string token)
    {
        var request = new RestRequest($"/classes/create", Method.Post);
        request.AddHeader("Authorization", $"Bearer {token}");

        // API expects query parameters, not form-data or JSON
        request.AddQueryParameter("class_name", classname);
        request.AddQueryParameter("subject_1", subject_1);
        request.AddQueryParameter("subject_2", subject_2);
        request.AddQueryParameter("subject_3", subject_3);

        RestResponse response = _client.Execute(request);
        Console.WriteLine($"CreateClass {classname} Response: {response.Content}");

        return HandleResponse(response);
    }

    public RestResponse AddStudentCall(string studentName, string class_id, string token)
    {
        var request = new RestRequest($"/classes/add_student", Method.Post);
        request.AddHeader("Authorization", $"Bearer {token}");

        request.AddQueryParameter("name", studentName);
        request.AddQueryParameter("class_id", class_id);

        RestResponse response = _client.Execute(request);
        Console.WriteLine($"AddStudent {studentName} Response: {response.Content}");

        return HandleResponse(response);
    }

    public RestResponse AddGradeCall(int grade, string student_id, string subject, string token)
    {
        var request = new RestRequest($"/grades/add", Method.Put);
        request.AddHeader("Authorization", $"Bearer {token}");

        request.AddQueryParameter("grade", grade);
        request.AddQueryParameter("student_id", student_id);
        request.AddQueryParameter("subject", subject);

        RestResponse response = _client.Execute(request);
        Console.WriteLine($"AddGrade {grade} to {student_id} in {subject}. Response: {response.Content}");

        return HandleResponse(response);
    }

    public RestResponse ConnectParentCall(string parent_username, string student_id, string token)
    {
        var request = new RestRequest($"/users/connect_parent", Method.Put);
        request.AddHeader("Authorization", $"Bearer {token}");

        request.AddQueryParameter("parent_username", parent_username);
        request.AddQueryParameter("student_id", student_id);

        RestResponse response = _client.Execute(request);
        Console.WriteLine($"Connect {parent_username} to student with id: {student_id}. Response: {response.Content}");

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
