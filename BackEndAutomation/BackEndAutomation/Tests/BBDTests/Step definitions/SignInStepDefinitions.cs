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
    public class UserSignInStepDefinitions
    {
        private readonly RestCalls _restCalls;
        private readonly ResponseDataExtractors _extractResponseData;
        private readonly ScenarioContext _scenarioContext;
        private readonly ExtentTest _test;

        public UserSignInStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
        {
            _scenarioContext = scenarioContext;
            _restCalls = restCalls;
            _extractResponseData = extractResponseData;
            _test = scenarioContext.Get<ExtentTest>(ContextKeys.ExtentTestKey);
        }

        [Given("user signs in with {string} username and {string} password.")]
        public void UserSignIn_(string username, string password)
        {
            _test.Info($"Attempting to sign in with username: {username}");

            RestResponse response = _restCalls.SignInUserCall(username, password);
            string tokenValue = _extractResponseData.Extractor(response.Content, JsonIdentifierKeys.AccessTokenKey);
            _scenarioContext.Add(ContextKeys.UserTokenKey, tokenValue);
            _scenarioContext.Add(ContextKeys.UserNameKey, username);

            Console.WriteLine(response.Content);
            _test.Pass($"User {username} successfully signed in and token retrieved.");
        }

        [Then("validate that the user is signed in.")]
        public void ValidateUserIsSignedIn_()
        {
            string username = _scenarioContext.Get<string>(ContextKeys.UserNameKey);
            bool isTokenExtracted = string.IsNullOrEmpty(_scenarioContext.Get<string>(ContextKeys.UserTokenKey));

            Utilities.UtilitiesMethods.AssertEqual(
                false,
                isTokenExtracted,
                $"Token is not extracted or user {username} is not signed in",
                _scenarioContext);

            _test.Pass($"Token validation passed. User {username} is signed in.");
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
//    public class UserSignInStepDefinitions
//    {
//        private readonly RestCalls _restCalls;
//        private readonly ResponseDataExtractors _extractResponseData;
//        private readonly ContextManager _ctx;

//        public UserSignInStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
//        {
//            _restCalls = restCalls;
//            _extractResponseData = extractResponseData;
//            _ctx = new ContextManager(scenarioContext);
//        }

//        [Given("user signs in with {string} username and {string} password.")]
//        public void UserSignIn_(string username, string password)
//        {
//            _ctx.Test.Info($"Attempting to sign in with username: {username}");

//            RestResponse response = _restCalls.SignInUserCall(username, password);
//            string tokenValue = _extractResponseData.ExtractSignedInUserToken(response.Content, "access_token");

//            _ctx.Set(ContextKeys.UserTokenKey, tokenValue);
//            _ctx.Set(ContextKeys.UserNameKey, username);

//            Console.WriteLine(response.Content);
//            _ctx.LogPass($"User {username} successfully signed in and token retrieved.");
//        }

//        [Then("validate that the user is signed in.")]
//        public void ValidateUserIsSignedIn_()
//        {
//            string username = _ctx.Get<string>(ContextKeys.UserNameKey);
//            string token = _ctx.Get<string>(ContextKeys.UserTokenKey);

//            bool isTokenMissing = string.IsNullOrEmpty(token);

//            UtilitiesMethods.AssertEqual(
//                false,
//                isTokenMissing,
//                $"Token is not extracted or user {username} is not signed in",
//                _ctx.ScenarioContext);

//            _ctx.LogPass($"Token validation passed. User {username} is signed in.");
//        }
//    }
//}

