using System;
using System.Reactive.Subjects;
using AventStack.ExtentReports;
using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Utilities;
using Reqnroll;
using RestSharp;

namespace BackEndAutomation
{
    [Binding]
    public class AddStudentStepDefinitions
    {
        private readonly RestCalls _restCalls;
        private readonly ResponseDataExtractors _extractResponseData;
        private readonly ScenarioContext _scenarioContext;
        private readonly ExtentTest _test;

        public AddStudentStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
        {
            _scenarioContext = scenarioContext;
            _restCalls = restCalls;
            _extractResponseData = extractResponseData;
            _test = scenarioContext.Get<ExtentTest>(ContextKeys.ExtentTestKey);
        }
        [When("teacher add student with {string} name and {string} class id.")]
        public void TeacherAddStudent(string studentName, string class_id)
        {
            _test.Info($"Adding student with name: {studentName} and class id {class_id}");

            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey);
            RestResponse response = _restCalls.AddStudentCall(studentName, class_id, token);
            string message = _extractResponseData.Extractor(response.Content, JsonIdentifierKeys.MessageKey);
            string studentID = _extractResponseData.Extractor(response.Content, JsonIdentifierKeys.StudentIdKey);
            _scenarioContext.Add(ContextKeys.MessageKey, message);
            _scenarioContext.Add(ContextKeys.StudentNameKey, studentName);
            _scenarioContext.Add(ContextKeys.StudentIdKey, studentID);
            _scenarioContext.Add(ContextKeys.ClassIdKey, class_id);

            Console.WriteLine(response.Content);
            _test.Pass($"{studentName} with {studentID} added successfully to class with class id: {class_id}. Response message: {message}");
        }

        [Then("validate that student is added {string}.")]
        public void ValidateStudentIsAdded_(string expectedMessage)
        {
            string actualMessage = _scenarioContext.Get<string>(ContextKeys.MessageKey);
            string class_id = _scenarioContext.Get<string>(ContextKeys.ClassIdKey);
            string studentName = _scenarioContext.Get<string>(ContextKeys.StudentNameKey);
            bool isStudentIdExtracted = string.IsNullOrEmpty(_scenarioContext.Get<string>(ContextKeys.StudentIdKey));

            Utilities.UtilitiesMethods.AssertEqual(
                false,
                isStudentIdExtracted,
                $"Student ID is not extracted or {studentName} is not added.",
                _scenarioContext);

            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Adding {studentName} failed.",
                _scenarioContext);

            _test.Pass($"Adding {studentName} validation passed. Student {studentName} is added to class with id {class_id}.");
        }

        [Then("validate that student is not added {string}.")]
        public void ValidateStudentIsNotAdded_(string expectedMessage)
        {
            string actualMessage = _scenarioContext.Get<string>(ContextKeys.MessageKey);
            string class_id = _scenarioContext.Get<string>(ContextKeys.ClassIdKey);
            string studentName = _scenarioContext.Get<string>(ContextKeys.StudentNameKey);

            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Validating {studentName} is not added - failed.",
                _scenarioContext);

            _test.Pass($"Student {studentName} is not added to class with id {class_id}.");
        }

    }
}
