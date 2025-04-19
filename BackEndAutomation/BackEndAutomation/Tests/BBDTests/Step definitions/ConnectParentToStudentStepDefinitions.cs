using System;
using AventStack.ExtentReports;
using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Utilities;
using Reqnroll;
using RestSharp;

namespace BackEndAutomation
{
    [Binding]
    public class ConnectParentToStudentStepDefinitions
    {
        private readonly RestCalls _restCalls;
        private readonly ResponseDataExtractors _extractResponseData;
        private readonly ScenarioContext _scenarioContext;
        private readonly ExtentTest _test;

        public ConnectParentToStudentStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
        {
            _scenarioContext = scenarioContext;
            _restCalls = restCalls;
            _extractResponseData = extractResponseData;
            _test = scenarioContext.Get<ExtentTest>(ContextKeys.ExtentTestKey);
        }
        [When("admin connect parent {string} to student with id: {string}.")]
        public void AdminConnectParentToStudent_(string parent_username, string student_id)
        {
            _test.Info($"Connecting parent {parent_username}, to student with id: {student_id}.");
            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey);
            RestResponse response = _restCalls.ConnectParentCall(parent_username, student_id, token);
            string message = _extractResponseData.ExtractMessage(response.Content);
            _scenarioContext.Add(ContextKeys.MessageKey, message);
            _scenarioContext.Add(ContextKeys.ParentUsernameKey, parent_username);
            _scenarioContext.Add(ContextKeys.StudentIdKey, student_id);

            Console.WriteLine(response.Content);
            _test.Pass($"Parent {parent_username} connected successfully to student with id: {student_id}. Response message: {message}");
        }

        [Then("validate parent is connected to student {string}.")]
        public void ValidateParentIsConnected_(string expectedMessage)
        {
            string actualMessage = _scenarioContext.Get<string>(ContextKeys.MessageKey);
            string parent_username = _scenarioContext.Get<string>(ContextKeys.ParentUsernameKey);
            string student_id = _scenarioContext.Get<string>(ContextKeys.StudentIdKey);

            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"TEST FAILED AT: Connecting parent {parent_username} to student with id: {student_id}.",
                _scenarioContext);

            _test.Pass($"VALIDATION TEST PASSED: Parent {parent_username} is connected to student with id: {student_id}.");
        }
    }
}
