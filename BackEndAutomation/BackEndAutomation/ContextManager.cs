using AventStack.ExtentReports;
using Reqnroll;
using System;

namespace BackEndAutomation.Utilities
{
    //public class ContextManager
    //{
    //    private readonly ScenarioContext _scenarioContext;

    //    // Constructor to initialize ScenarioContext
    //    public ContextManager(ScenarioContext scenarioContext)
    //    {
    //        _scenarioContext = scenarioContext;
    //    }

    //    // Set value in ScenarioContext
    //    public void Set<T>(string key, T value)
    //    {
    //        _scenarioContext[key] = value!;
    //    }

    //    // Get value from ScenarioContext
    //    public T Get<T>(string key)
    //    {
    //        return _scenarioContext.TryGetValue(key, out var value) ? (T)value : default!;
    //    }

    //    // Utility to log pass messages (using ExtentTest)
    //    public ExtentTest Test => Get<ExtentTest>(ContextKeys.ExtentTestKey);

    //    public void LogPass(string message) => Test?.Pass(message);
    //    public void LogFail(string message) => Test?.Fail(message);
    //}

    //// Centralized keys for accessing values in ScenarioContext
    public static class ContextKeys
    {
        public const string UserTokenKey = "UserToken";
        public const string UserNameKey = "Username";
        public const string MessageKey = "Message";
        public const string RoleKey = "Role";
        public const string ClassIdKey = "ClassID";
        public const string ClassNameKey = "ClassName";
        public const string StudentIdKey = "StudentID";
        public const string StudentNameKey = "StudentName";
        public const string GradeKey = "Grade";
        public const string SubjectKey = "Subject";
        public const string NewGradeKey = "NewGrade";
        public const string ExtentTestKey = "ExtentTest";
    }
}
