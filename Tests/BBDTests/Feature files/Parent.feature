Feature: Parent

  Parent authentication and student grades view.

Background:
	Given user signs in with "parent1" username and "parent1" password.

Scenario: Sign in and receive a token
	Then validate that the user is signed in.

@Positive_flow
Scenario: View student grades
	When parent view grades of student with id: "43bac5dc-ecba-4826-8b8d-204cecd07b18".
	Then validate grades are visible.

@Positive_flow
Scenario: Try to view student grades with invalid id
	When parent view grades of student with id: "invalid-student-id".
	Then validate student id is invalid "Student not found".

@Negative_flow
Scenario: Try to view student grades, who isn't connected to parent
	When parent view grades of student with id: "5b238374-36b6-477b-9ff0-9333a0b194b1".
	Then validate student is not linked to parent "You can't view this student's grades".
