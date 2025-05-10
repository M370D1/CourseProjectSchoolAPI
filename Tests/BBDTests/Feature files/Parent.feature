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