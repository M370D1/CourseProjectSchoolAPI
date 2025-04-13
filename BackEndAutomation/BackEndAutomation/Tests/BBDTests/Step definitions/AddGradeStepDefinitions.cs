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
    public class AddGradeStepDefinitions
    {
        private readonly RestCalls _restCalls;
        private readonly ResponseDataExtractors _extractResponseData;
        private readonly ScenarioContext _scenarioContext;
        private readonly ExtentTest _test;

        public AddGradeStepDefinitions(ScenarioContext scenarioContext, RestCalls restCalls, ResponseDataExtractors extractResponseData)
        {
            _scenarioContext = scenarioContext;
            _restCalls = restCalls;
            _extractResponseData = extractResponseData;
            _test = scenarioContext.Get<ExtentTest>(ContextKeys.ExtentTestKey);
        }
        [When("teacher add grade: {string}, to student: {string}, in subject: {string}.")]
        public void TeacherAddGrade_(int grade, string student_id, string subject)
        {
            _test.Info($"Adding grade: {grade}, to student with id: {student_id}, in subject: {subject}.");

            string token = _scenarioContext.Get<string>(ContextKeys.UserTokenKey);
            RestResponse response = _restCalls.AddGradeCall(grade, student_id, subject, token);
            string message = _extractResponseData.ExtractMessage(response.Content);
            _scenarioContext.Add(ContextKeys.MessageKey, message);
            _scenarioContext.Add(ContextKeys.SubjectKey, subject);
            _scenarioContext.Add(ContextKeys.StudentIdKey, student_id);
            _scenarioContext.Add(ContextKeys.GradeKey, grade);

            Console.WriteLine(response.Content);
            _test.Pass($"{grade} added successfully to {student_id} in {subject}. Response message: {message}");
        }

        [Then("validate that grade is added to student {string}.")]
        public void ValidateGradeIsAdded_(string expectedMessage)
        {
            string actualMessage = _scenarioContext.Get<string>(ContextKeys.MessageKey);
            string student_id = _scenarioContext.Get<string>(ContextKeys.StudentIdKey);
            int grade = _scenarioContext.Get<int>(ContextKeys.GradeKey);
            string subject = _scenarioContext.Get<string>(ContextKeys.SubjectKey);

            Utilities.UtilitiesMethods.AssertEqual(
                expectedMessage,
                actualMessage,
                $"Adding grade: {grade}, to student id: {student_id}, in subject: {subject} - failed.",
                _scenarioContext);

            _test.Pass($"Validation passed, grade {grade} is added to student id: {student_id}, in subject: {subject}.");
        }
    }
}
