using System;
using AventStack.ExtentReports;
using BackEndAutomation.Rest.Calls;
using BackEndAutomation.Rest.DataManagement;
using BackEndAutomation.Utilities;
using NUnit.Framework;
using Reqnroll;
using RestSharp;

namespace BackEndAutomation
{
    [Binding]
    public class ParentViewGradesStepDefinitions
    {
        private readonly RestCalls _restCalls;
        private readonly ResponseDataExtractors _extractResponseData;
        private readonly ScenarioContext _scenarioContext;
        private readonly ExtentTest _test;

        public ParentViewGradesStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
        {
            _scenarioContext = scenarioContext;
            _restCalls = restCalls;
            _extractResponseData = extractResponseData;
            _test = scenarioContext.Get<ExtentTest>(ContextKeys.ExtentTestKey);
        }
        [When("parent view grades of student with id: {string}.")]
        public void ParentViewGrades_(string student_id)
        {
            _test.Info($"Parent is attempting to view grades for student with ID: {student_id}.");

            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey);
            RestResponse response = _restCalls.ViewGradesCall(student_id, token);

            if (response.Content.Contains(JsonIdentifierKeys.DetailKey))
            {
                string errorMessage = _extractResponseData.Extractor(response.Content, JsonIdentifierKeys.DetailKey);

                _test.Fail($"Failed to retrieve grades for student ID: {student_id}. Error: {errorMessage}");
                Console.WriteLine($"Error: Failed to retrieve grades for student ID: {student_id}. Error: {errorMessage}");
                Assert.Fail($"Failed to retrieve grades for student ID: {student_id}. Error: {errorMessage}");
            }

            string allGrades = _extractResponseData.ExtractAllGrades(response.Content);
            _scenarioContext.Add(ContextKeys.StudentIdKey, student_id);
            _scenarioContext.Add(ContextKeys.AllGradesKey, allGrades);

            Console.WriteLine(response.Content);
            _test.Pass($"Grades successfully retrieved for student ID: {student_id}. Extracted grades: {allGrades}.");
        }

        [Then("validate grades are visible.")]
        public void ValidateGradesAreVisible_()
        {
            string allGrades = _scenarioContext.Get<string>(ContextKeys.AllGradesKey);
            bool areGradesExtracted = !string.IsNullOrEmpty(_scenarioContext.Get<string>(ContextKeys.AllGradesKey));

            UtilitiesMethods.AssertEqual(
                true,
                areGradesExtracted,
                "Grade extraction failed: No grades found or student has no assigned grades.",
                _scenarioContext);

            Console.WriteLine($"Grades {allGrades} are visible: {areGradesExtracted}");
            _test.Pass($"Validation successful: Grades are visible for the student. Grades: {allGrades}.");
        }
    }
}