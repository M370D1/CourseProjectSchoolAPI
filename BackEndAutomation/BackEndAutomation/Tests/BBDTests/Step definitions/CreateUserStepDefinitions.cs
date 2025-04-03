//using System;
//using AventStack.ExtentReports;
//using BackEndAutomation.Rest.Calls;
//using BackEndAutomation.Rest.DataManagement;
//using Reqnroll;
//using RestSharp;

//namespace BackEndAutomation
//{
//    [Binding]
//    public class CreateTeacherStepDefinitions
//    {
//        private RestCalls restCalls = new RestCalls();
//        private ResponseDataExtractors extractResponseData = new ResponseDataExtractors();
//        private RestResponse userLoginResponse, userProfileDetailsResponse, userFollowResponse, userUnfollowResponse;
//        private readonly ScenarioContext _scenarioContext;
//        private ExtentTest _test;

//        public CreateTeacherStepDefinitions(ScenarioContext scenarioContext)
//        {
//            _scenarioContext = scenarioContext;
//            _test = scenarioContext.Get<ExtentTest>("ExtentTest");
//        }
//        [When("admin create teacher with {string} username and {string} password and {string} role.")]
//        public void WhenAdminCreateTeacher_(string username, string password, string role)
//        {
//            {
//                string token = _scenarioContext.Get<string>("UserToken");
//                RestResponse response = restCalls.CraeteTeacherCall(username, password, role, token);
//                string message = extractResponseData.ExtractMessage(response.Content);
//                _scenarioContext.Add("message", message);
//            }
//        }

//        [Then("validate teacher is created {string}.")]
//        public void ThenValidateThatTheTeacherIsCreated_(string expectedMessage)
//        {
//            string actualMessage = _scenarioContext.Get<string>("message");
//            Utilities.UtilitiesMethods.AssertEqual(
//                expectedMessage,
//                actualMessage,
//                "Creating teacher faild.",
//                _scenarioContext);
//        }
//    }
//}

using System;
using AventStack.ExtentReports;
using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
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

        private const string UserTokenKey = "UserToken"; // Constant for context key
        private const string MessageKey = "Message"; // Constant for response message key
        private const string Role = "Role";

        public CreateUserStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
        {
            _scenarioContext = scenarioContext;
            _restCalls = restCalls;
            _extractResponseData = extractResponseData;
            _test = scenarioContext.Get<ExtentTest>("ExtentTest");
        }

        [When("admin creates a user with {string} username, {string} password, and {string} role.")]
        public void AdminCreateUser_(string username, string password, string role)
        {
            string token = _scenarioContext.Get<string>(UserTokenKey);
            _test.Info($"Creating user with username: {username}, role: {role}");

            RestResponse response = _restCalls.CraeteUserCall(username, password, role, token);
            string message = _extractResponseData.ExtractMessage(response.Content);
            _scenarioContext.Add(MessageKey, message);
            _scenarioContext.Add(Role, role);

            _test.Pass($"{role} created successfully. Response message: {message}");
        }

        [Then("validate user is created {string}.")]
        public void ValidateUserIsCreated_(string expectedMessage)
        {
            string role = _scenarioContext.Get<string>(Role);
            string actualMessage = _scenarioContext.Get<string>(MessageKey);
            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Creating {role} failed.",
                _scenarioContext);

            _test.Pass($"{role} creation validated successfully.");
        }
    }
}

