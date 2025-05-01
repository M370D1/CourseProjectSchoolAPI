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
    public class CreateUserStepDefinitions
    {
        private readonly RestCalls _restCalls;
        private readonly ResponseDataExtractors _extractResponseData;
        private readonly ScenarioContext _scenarioContext;
        private readonly ExtentTest _test;

        public CreateUserStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
        {
            _scenarioContext = scenarioContext;
            _restCalls = restCalls;
            _extractResponseData = extractResponseData;
            _test = scenarioContext.Get<ExtentTest>(ContextKeys.ExtentTestKey);
        }

        [When("admin creates a user with {string} username, {string} password, and {string} role.")]
        public void AdminCreateUser_(string username, string password, string role)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            username = $"{username}_{timestamp}";

            _test.Info($"Creating user with username: {username}, role: {role}");

            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey);
            RestResponse response = _restCalls.CraeteUserCall(username, password, role, token);
            string message = _extractResponseData.Extractor(response.Content, JsonIdentifierKeys.MessageKey);
            _scenarioContext.Add(ContextKeys.MessageKey, message);
            _scenarioContext.Add(ContextKeys.RoleKey, role);
            _scenarioContext[ContextKeys.UserNameKey] = username;

            Console.WriteLine(response.Content);
            _test.Pass($"{username} {response.Content}");
        }

        [Then("validate user is created.")]
        public void ValidateUserIsCreated_()
        {
            string role = _scenarioContext.Get<string>(ContextKeys.RoleKey);
            string actualMessage = _scenarioContext.Get<string>(ContextKeys.MessageKey);
            string username = _scenarioContext.Get<string>(ContextKeys.UserNameKey);
            string expectedMessage = $"{role} '{username}' created successfully";

            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Creating {role} failed.",
                _scenarioContext);

            _test.Pass($"{role} validation passed. {role} is created.");
        }

        [When("admin try to create existing user with {string} username, {string} password, and {string} role.")]
        public void AdminTryToCreateExistingUser_(string username, string password, string role)
        {
            _test.Info($"Trying to create existing user with username: {username}, role: {role}");

            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey);
            RestResponse response = _restCalls.CraeteUserCall(username, password, role, token);
            string detail = _extractResponseData.Extractor(response.Content, JsonIdentifierKeys.DetailKey);
            _scenarioContext.Add(ContextKeys.DetailKey, detail);
            _scenarioContext.Add(ContextKeys.RoleKey, role);

            Console.WriteLine(response.Content);
            _test.Pass($"{username} {response.Content}");
        }

        [Then("validate user is already created {string}.")]
        public void ThenValidateUserIsAlreadyCreated_(string expectedMessage)
        {
            string role = _scenarioContext.Get<string>(ContextKeys.RoleKey);
            string actualMessage = _scenarioContext.Get<string>(ContextKeys.DetailKey);

            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Validating {role} already exists - failed.",
                _scenarioContext);

            _test.Pass($"{role} is already existing.");
        }

    }
}