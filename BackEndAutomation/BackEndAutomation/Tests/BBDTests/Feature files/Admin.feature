#Feature: Admin
#
#A short summary of the feature
#
#@tag1
#Scenario: Sign in and receive a token
#	When user sign in with "admin1" username and "admin123" password.
#	Then validate that the user is signed in.
#
#Scenario: Create teacher.
#	When user sign in with "admin1" username and "admin123" password.
#	And admin create teacher with "teacher3" username and "teacher3" password and "teacher" role.
#	Then validate teacher is created "teacher 'teacher3' created successfully".

Feature: Admin

  Admin authentication and teacher management.

  Background:
    Given user signs in with "admin1" username and "admin123" password.

  @tag1
  Scenario: Sign in and receive a token
    Then validate that the user is signed in.

  Scenario Outline: Create a user
    When admin creates a user with "<username>" username, "<password>" password, and "<role>" role.
    Then validate user is created "<message>".

    Examples:
      | username  | password  | role       | message                                    |
      | teacher11 | teacher11 | teacher    | teacher 'teacher11' created successfully   |
      | moderator | moderator | moderator  | moderator 'moderator' created successfully |
