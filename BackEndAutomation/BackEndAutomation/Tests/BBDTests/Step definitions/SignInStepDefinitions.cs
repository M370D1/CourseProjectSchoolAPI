//using System;
//using AventStack.ExtentReports;
//using BackEndAutomation.Rest.Calls;
//using BackEndAutomation.Rest.DataManagement;
//using Reqnroll;
//using RestSharp;

//namespace BackEndAutomation
//{
//    [Binding]
//    public class StepSignIn
//    {
//        private RestCalls restCalls = new RestCalls();
//        private ResponseDataExtractors extractResponseData = new ResponseDataExtractors();
//        private RestResponse userLoginResponse, userProfileDetailsResponse, userFollowResponse, userUnfollowResponse;
//        private readonly ScenarioContext _scenarioContext;
//        private ExtentTest _test;

//        public StepSignIn(ScenarioContext scenarioContext)
//        {
//            _scenarioContext = scenarioContext;
//            _test = scenarioContext.Get<ExtentTest>("ExtentTest");
//        }
//        [When("user sign in with {string} username and {string} password.")]
//        public void UserSignIn_(string username, string password)
//        {
//            RestResponse response = restCalls.SignInUserCall(username, password);
//            string tokenValue = extractResponseData.ExtractLoggedInUserToken(response.Content, "access_token");

//            _scenarioContext.Add("UserToken", tokenValue);
//        }

//        [Then("validate that the user is signed in.")]
//        public void ThenValidateThatTheUserIsSignedIn_()
//        {
//            bool isTokenExtracted = string.IsNullOrEmpty(_scenarioContext.Get<string>("UserToken"));
//            Utilities.UtilitiesMethods.AssertEqual(
//                false,
//                isTokenExtracted,
//                "Token is not extracted or user is not logged in",
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
    public class StepSignIn
    {
        private readonly RestCalls _restCalls;
        private readonly ResponseDataExtractors _extractResponseData;
        private readonly ScenarioContext _scenarioContext;
        private readonly ExtentTest _test;

        private const string UserTokenKey = "UserToken"; // Constant for context key

        public StepSignIn(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
        {
            _scenarioContext = scenarioContext;
            _restCalls = restCalls;
            _extractResponseData = extractResponseData;
            _test = scenarioContext.Get<ExtentTest>("ExtentTest");
        }

        [Given("user signs in with {string} username and {string} password.")]
        public void UserSignIn_(string username, string password)
        {
            _test.Info($"Attempting to sign in with username: {username}");
            RestResponse response = _restCalls.SignInUserCall(username, password);
            string tokenValue = _extractResponseData.ExtractLoggedInUserToken(response.Content, "access_token");
            Console.WriteLine(response.Content);
            _scenarioContext.Add(UserTokenKey, tokenValue);
            _test.Pass("User successfully signed in and token retrieved.");
        }

        [Then("validate that the user is signed in.")]
        public void ThenValidateThatTheUserIsSignedIn_()
        {
            bool isTokenExtracted = string.IsNullOrEmpty(_scenarioContext.Get<string>(UserTokenKey));
            Utilities.UtilitiesMethods.AssertEqual(
                false,
                isTokenExtracted,
                "Token is not extracted or user is not logged in",
                _scenarioContext);

            _test.Pass("Token validation passed. User is signed in.");
        }
    }
}

