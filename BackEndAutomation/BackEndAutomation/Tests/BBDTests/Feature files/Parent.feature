Feature: Parent

  Parent authentication student grades view.

Background:
	Given user signs in with "parent1" username and "parent1" password.
	Then validate parent is connected to student "<message>".

Scenario: Sign in and receive a token
	Then validate that the user is signed in.

Scenario Outline: View grades
	When parent view grades of student with id: "<student_id>".
	Then validate grads are visible.
