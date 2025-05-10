School API - Automated BDD Tests
This project implements automated API tests for the School API using Behavior Driven Development (BDD) in C# with the RestSharp library and SpecFlow/Reqnroll.

🧪 Project Overview
The goal of this project is to test the School API’s key functionalities using BDD-style scenarios. These tests are written in Gherkin syntax and executed through a structured test automation framework that ensures readability, reliability, and clear reporting.

✅ Key Features
Written in C# using BDD (Behavior Driven Development)
Uses RestSharp for API calls
Integrated with Reqnroll for Gherkin-style step definitions
Includes logging for all test steps
Generates HTML test reports with clear human-readable messages
Handles errors gracefully with meaningful feedback in the report and logs

📋 Functionalities Covered:
The test suite covers 5 core functionalities of the School API, such as:

1. Login and token retrieval
2. Creating a class by a teacher
3. Adding a student to a class
4. Adding a grade to a student and update it
5. Parent views their child's grades

Tests list:

1. Create user
2. Sign in and receive a token
3. Try to create user that exists
4. Connecting parent to student
5. View student grades
6. Create a class
7. Add stuednt to class
8. Add and update grade

🛠️ Tech Stack
.NET 6 / .NET Core
C#
Reqnroll (for BDD)
RestSharp (for HTTP requests)
Extent Reports (for HTML reporting)
NLog (for logging)
NUnit (as the test runner)

🧰 Installation
Clone the repository:
```bash
git clone git@github.com:M370D1/CourseProjectSchoolAPI.git
cd CourseProjectSchoolAPI
```
Restore NuGet packages:
```bash
dotnet restore
```
Build the project:
```bash
dotnet build
```
🚀 Running the Tests
You can run the test suite using the command:
```bash
dotnet test
```
Test results will be available as:
HTML report (e.g., /Reports/ExtentReport.html)
Log file (e.g., /Logs/logfile.txt)

🧾 Error Handling
All failed tests are caught gracefully with:

Human-readable error messages in the test report
Corresponding log entries with detailed debug info

Note: This project is for educational and demo purposes and assumes access to the School API endpoints with valid authentication.
