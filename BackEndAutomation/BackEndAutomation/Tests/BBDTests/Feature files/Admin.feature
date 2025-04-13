Feature: Admin

  Admin authentication and user management.

  Background:
    Given user signs in with "admin1" username and "admin123" password.

  Scenario: Sign in and receive a token
    Then validate that the user is signed in.

  Scenario Outline: Create a user
    When admin creates a user with "<username>" username, "<password>" password, and "<role>" role.
    Then validate user is created "<message>".

    Examples:
      | username  | password  | role       | message                                    |
      | teacher11 | teacher11 | teacher    | teacher 'teacher11' created successfully   |
      | moderator | moderator | moderator  | moderator 'moderator' created successfully |
