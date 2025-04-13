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
    public class CreateClassStepDefinitions
    {
        private readonly RestCalls _restCalls;
        private readonly ResponseDataExtractors _extractResponseData;
        private readonly ScenarioContext _scenarioContext;
        private readonly ExtentTest _test;

        public CreateClassStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
        {
            _scenarioContext = scenarioContext;
            _restCalls = restCalls;
            _extractResponseData = extractResponseData;
            _test = scenarioContext.Get<ExtentTest>(ContextKeys.ExtentTestKey);
        }
        [When("teacher creates a class with {string} classname, {string} subject_1, {string} subject_2 and {string} subject_3.")]
        public void TeacherCreateClass_(string classname, string subject_1, string subject_2, string subject_3)
        {
            _test.Info($"Creating class with classname: {classname} and subjects: {subject_1}, {subject_2}, {subject_3}");

            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey);
            RestResponse response = _restCalls.CraeteClassCall(classname, subject_1, subject_2, subject_3, token);
            string message = _extractResponseData.ExtractMessage(response.Content);
            string classID = _extractResponseData.ExtractClassID(response.Content);
            _scenarioContext.Add(ContextKeys.MessageKey, message);
            _scenarioContext.Add(ContextKeys.ClassIdKey, classID);
            _scenarioContext.Add(ContextKeys.ClassNameKey, classname);

            Console.WriteLine(response.Content);
            _test.Pass($"{classname} created successfully. Response message: {message}");
        }

        [Then("validate class is created {string}.")]
        public void ValidateClassIsCreated_(string expectedMessage)
        {

            string actualMessage = _scenarioContext.Get<string>(ContextKeys.MessageKey);
            string class_id = _scenarioContext.Get<string>(ContextKeys.ClassIdKey);
            string classname = _scenarioContext.Get<string>(ContextKeys.ClassNameKey);
            bool isClassIdExtracted = string.IsNullOrEmpty(_scenarioContext.Get<string>(ContextKeys.ClassIdKey));

            Utilities.UtilitiesMethods.AssertEqual(
                false,
                isClassIdExtracted,
                $"ClassID is not extracted or {classname} is not created.",
                _scenarioContext);

            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Creating {classname} failed.",
                _scenarioContext);

            _test.Pass($"Creating class {classname} validation passed. {classname} with id: {class_id} is created .");
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
//    public class CreateClassStepDefinitions
//    {
//        private readonly RestCalls _restCalls;
//        private readonly ResponseDataExtractors _extractResponseData;
//        private readonly ScenarioContext _scenarioContext;
//        private readonly ContextManager _ctx; // Use ContextManager for shared context
//        private readonly ExtentTest _test;

//        // Constructor to inject dependencies
//        public CreateClassStepDefinitions(ScenarioContext scenarioContext, ContextManager ctx, RestCalls restCalls, ResponseDataExtractors extractResponseData)
//        {
//            _ctx = ctx;
//            _scenarioContext = scenarioContext;
//            _restCalls = restCalls;
//            _extractResponseData = extractResponseData;
//            _test = scenarioContext.Get<ExtentTest>("ExtentTest");
//        }

//        [When("teacher creates a class with {string} classname, {string} subject_1, {string} subject_2 and {string} subject_3.")]
//        public void TeacherCreateClass_(string classname, string subject_1, string subject_2, string subject_3)
//        {
//            _test.Info($"Creating class with classname: {classname} and subjects: {subject_1}, {subject_2}, {subject_3}");

//            // Retrieve the token from the context
//            string token = _ctx.Get<string>(ContextKeys.UserTokenKey); // Fetch from ContextManager
//            RestResponse response = _restCalls.CraeteClassCall(classname, subject_1, subject_2, subject_3, token);

//            // Extract message and class ID
//            string message = _extractResponseData.ExtractMessage(response.Content);
//            string classID = _extractResponseData.ExtractClassID(response.Content);

//            // Add extracted values to ContextManager
//            _ctx.Set(ContextKeys.MessageKey, message);
//            _ctx.Set(ContextKeys.ClassIdKey, classID);
//            _ctx.Set(ContextKeys.ClassNameKey, classname);

//            Console.WriteLine(response.Content);
//            _test.Pass($"{classname} created successfully. Response message: {message}");
//        }

//        [Then("validate class is created {string}.")]
//        public void ValidateClassIsCreated_(string expectedMessage)
//        {
//            // Retrieve values from ContextManager
//            string actualMessage = _ctx.Get<string>(ContextKeys.MessageKey);
//            string class_id = _ctx.Get<string>(ContextKeys.ClassIdKey);
//            string classname = _ctx.Get<string>(ContextKeys.ClassNameKey);

//            // Check if Class ID was extracted properly
//            bool isClassIdExtracted = !string.IsNullOrEmpty(class_id);

//            // Use Utilities to assert if class ID extraction was successful
//            Utilities.UtilitiesMethods.AssertEqual(
//                true,
//                isClassIdExtracted,
//                $"ClassID is not extracted or {classname} is not created.",
//                _scenarioContext);

//            // Validate the creation message
//            Utilities.UtilitiesMethods.AssertEqual(
//                expectedMessage,
//                actualMessage,
//                $"Creating {classname} failed.",
//                _scenarioContext);

//            _test.Pass($"Creating class {classname} validation passed. {classname} with id: {class_id} is created.");
//        }
//    }
//}
