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
            _test.Info($"View grades to student with id: {student_id}.");

            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey);
            RestResponse response = _restCalls.ViewGradesCall(student_id, token);
            string allGrades = _extractResponseData.ExtractAllGrades(response.Content);
            string detail = _extractResponseData.Extractor(response.Content, JsonIdentifierKeys.DetailKey);
            _scenarioContext.Add(ContextKeys.StudentIdKey, student_id);
            _scenarioContext.Add(ContextKeys.AllGradesKey, allGrades);
            _scenarioContext.Add(ContextKeys.DetailKey, detail);

            Console.WriteLine(response.Content);
            _test.Pass($"View grades to student with id: {student_id}. All grades: {allGrades}");
        }

        [Then("validate grades are visible.")]
        public void ValidateGradesAreVisible_()
        {
            string allGrades = _scenarioContext.Get<string>(ContextKeys.AllGradesKey);
            bool areGradesExtracted = !string.IsNullOrEmpty(_scenarioContext.Get<string>(ContextKeys.AllGradesKey));

            Utilities.UtilitiesMethods.AssertEqual(
                true,
                areGradesExtracted,
                "Grades not extracted or student doesn't have grades.",
                _scenarioContext);

            _test.Pass($"VALIDATION TEST PASSED: Grades are visible: {allGrades}");
        }

        [Then("validate student id is invalid {string}.")]
        public void ValidateStudentIdIsInvalid_(string expectedMessage)
        {
            string actualMessage = _scenarioContext.Get<string>(ContextKeys.DetailKey);

            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Validating invalid student id failed.",
                _scenarioContext);

            _test.Pass($"Validation of invalid id passed.");
        }

        [Then("validate student is not linked to parent {string}.")]
        public void ThenValidateStudentIsNotLinkedToParent_(string expectedMessage)
        {
            string actualMessage = _scenarioContext.Get<string>(ContextKeys.DetailKey);

            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Validating invalid student id failed.",
                _scenarioContext);

            _test.Pass($"Validation of invalid id passed.");
        }

    }
}
