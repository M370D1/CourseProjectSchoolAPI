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
            _test.Info($"Creating user with username: {username}, role: {role}");

            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey);
            RestResponse response = _restCalls.CraeteUserCall(username, password, role, token);
            string message = _extractResponseData.ExtractMessage(response.Content);
            _scenarioContext.Add(ContextKeys.MessageKey, message);
            _scenarioContext.Add(ContextKeys.RoleKey, role);

            Console.WriteLine(response.Content);
            _test.Pass($"{role} created successfully. Response message: {message}");
        }

        [Then("validate user is created {string}.")]
        public void ValidateUserIsCreated_(string expectedMessage)
        {
            string role = _scenarioContext.Get<string>(ContextKeys.RoleKey);
            string actualMessage = _scenarioContext.Get<string>(ContextKeys.MessageKey);

            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Creating {role} failed.",
                _scenarioContext);

            _test.Pass($"Creating {role} validation passed. {role} is created.");
        }
    }
}

//using System;
//using AventStack.ExtentReports;
//using BackEndAutomation.Rest.Calls;
//using BackEndAutomation.Rest.DataManagement;
//using BackEndAutomation.Utilities;
//using Reqnroll;
//using RestSharp;

//namespace BackEndAutomation
//{
//    [Binding]
//    public class CreateUserStepDefinitions
//    {
//        private readonly RestCalls _restCalls;
//        private readonly ResponseDataExtractors _extractResponseData;
//        private readonly ScenarioContext _scenarioContext;
//        private readonly ExtentTest _test;

//        public CreateUserStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
//        {
//            _scenarioContext = scenarioContext;
//            _restCalls = restCalls;
//            _extractResponseData = extractResponseData;
//            _test = _scenarioContext.Get<ExtentTest>(ContextKeys.ExtentTestKey);
//        }

//        [When("admin creates a user with {string} username, {string} password, and {string} role.")]
//        public void AdminCreateUser_(string username, string password, string role)
//        {
//            _test.Info($"Creating user with username: {username}, role: {role}");

//            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey); 
//            RestResponse response = _restCalls.CraeteUserCall(username, password, role, token);

//            string message = _extractResponseData.ExtractMessage(response.Content);
//            _scenarioContext.Add(ContextKeys.MessageKey, message); 
//            _scenarioContext.Add(ContextKeys.RoleKey, role); 

//            Console.WriteLine(response.Content);
//            _test.Pass($"{role} created successfully. Response message: {message}");
//        }

//        [Then("validate user is created {string}.")]
//        public void ValidateUserIsCreated_(string expectedMessage)
//        {
//            string role = _scenarioContext.Get<string>(ContextKeys.RoleKey); // Get role from context
//            string actualMessage = _scenarioContext.Get<string>(ContextKeys.MessageKey); // Get message from context

//            Utilities.UtilitiesMethods.AssertEqual(
//                expectedMessage,
//                actualMessage,
//                $"Creating {role} failed.",
//                _scenarioContext);

//            _test.Pass($"Creating {role} validation passed. {role} is created.");
//        }
//    }
//}
