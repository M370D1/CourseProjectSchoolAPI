using System;
using System.Reactive.Subjects;
using AventStack.ExtentReports;
using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Utilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Reqnroll;
using RestSharp;

namespace BackEndAutomation
{
    [Binding]
    public class AddGradeStepDefinitions
    {
        private readonly RestCalls _restCalls;
        private readonly ResponseDataExtractors _extractResponseData;
        private readonly ScenarioContext _scenarioContext;
        private readonly ExtentTest _test;

        public AddGradeStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
        {
            _scenarioContext = scenarioContext;
            _restCalls = restCalls;
            _extractResponseData = extractResponseData;
            _test = scenarioContext.Get<ExtentTest>(ContextKeys.ExtentTestKey);
        }
        [Then("teacher add grade: {string}, to student: {string}, in subject: {string}.")]
        public void TeacherAddGrade_(int grade, string student_id, string subject)
        {
            student_id = _scenarioContext.Get<string>(ContextKeys.StudentIdKey);
            string studentName = _scenarioContext.Get<string>(ContextKeys.StudentNameKey);

            _test.Info($"Initiating request to assign grade '{grade}' to student {studentName} with ID '{student_id}' for subject '{subject}'.");

            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey);
            RestResponse response = _restCalls.AddGradeCall(grade, student_id, subject, token);

            if (response.Content.Contains(JsonIdentifierKeys.DetailKey))
            {
                string errorMessage = _extractResponseData.Extractor(response.Content, JsonIdentifierKeys.DetailKey);

                _test.Fail($"Failed to assign grade '{grade}' to student {studentName} with ID '{student_id}' in subject '{subject}'. Error: {errorMessage}");
                Console.WriteLine($"Error while assigning grade. Student {studentName} with ID: {student_id}, Subject: {subject}. Error: {errorMessage}");
                Assert.Fail($"Failed to assign grade. Error: {errorMessage}");
            }

            string message = _extractResponseData.Extractor(response.Content, JsonIdentifierKeys.MessageKey);
            _scenarioContext[ContextKeys.MessageKey] = message;
            _scenarioContext.Add(ContextKeys.SubjectKey, subject);
            //_scenarioContext.Add(ContextKeys.StudentIdKey, student_id);
            _scenarioContext.Add(ContextKeys.GradeKey, grade);

            Console.WriteLine(response.Content);
            _test.Pass($"Successfully assigned grade '{grade}' to student {studentName} with ID '{student_id}' in subject '{subject}'. API response: \"{message}\".");
        }

        [Then("validate that grade is added to student {string}.")]
        public void ValidateGradeIsAdded_(string expectedMessage)
        {
            string actualMessage = _scenarioContext.Get<string>(ContextKeys.MessageKey);
            string student_id = _scenarioContext.Get<string>(ContextKeys.StudentIdKey);
            int grade = _scenarioContext.Get<int>(ContextKeys.GradeKey);
            string subject = _scenarioContext.Get<string>(ContextKeys.SubjectKey);

            UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Validation failed: Expected message does not match actual after assigning grade '{grade}' to student ID '{student_id}' for subject '{subject}'.",
                _scenarioContext);

            Console.WriteLine($"{expectedMessage}");
            _test.Pass($"Validation successful: Grade '{grade}' has been correctly added to student ID '{student_id}' for subject '{subject}'.");
        }
    }
}