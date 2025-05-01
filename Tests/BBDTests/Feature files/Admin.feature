Feature: Admin

  Admin authentication and user management.

Background:
	Given user signs in with "admin1" username and "admin123" password.

Scenario: Sign in and receive a token
	Then validate that the user is signed in.

@Positive_flow
Scenario: Create user
	When admin creates a user with "<username>" username, "<password>" password, and "<role>" role.
	Then validate user is created. "created successfully" 

Examples:
	| title              | username  | password  | role      | 
	| CREATING TEACHER   | Metodi    | teacher11 | teacher   | 
	| CREATING MODERATOR | Meto		 | moderator | moderator | 
	| CREATING PARENT    | parent1   | parent1   | parent    | 

@Negative_flow
Scenario: Try to create user that exists
	When admin try to create existing user with "teacher11" username, "teacher11" password, and "teacher" role.
	Then validate user is already created "Username already exists".


@Positive_flow
Scenario: Connecting parent to student
	When admin connect parent "parent1" to student with id: "43bac5dc-ecba-4826-8b8d-204cecd07b18".
	Then validate parent is connected to student "Parent linked to student".
    

