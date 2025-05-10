Feature: Teacher

  Teacher authentication and class management.

Background:
	Given user signs in with "teacher11" username and "teacher11" password.

Scenario: Sign in and receive a token
	Then validate that the user is signed in.

@Positive_flow
Scenario: Create a class
	When teacher creates a class with "classname" classname, "Math" subject_1, "History" subject_2 and "Phtsics" subject_3.
	Then validate class is created "Class created".

@Positive_flow
Scenario: Add stuednt to class
	When teacher creates a class with "classname" classname, "Math" subject_1, "History" subject_2 and "Phtsics" subject_3.
	Then validate class is created "Class created".
	Then teacher add student with "name" name and "class_id" class id.
	Then validate that student is added "Student added".

@Positive_flow
Scenario: Add and update grade
	When teacher creates a class with "classname" classname, "Math" subject_1, "History" subject_2 and "Phtsics" subject_3.
	Then validate class is created "Class created".
	Then teacher add student with "name" name and "class_id" class id.
	Then validate that student is added "Student added".
	Then teacher add grade: "2", to student: "student-id", in subject: "History".
	Then validate that grade is added to student "Grade added".
	And teacher update grade to "5".
	And validate that grade is updated "Grade updated".
	