Feature: Teacher

  Teacher authentication and class management.

Background:
	Given user signs in with "teacher11" username and "teacher11" password.

Scenario: Sign in and receive a token
	Then validate that the user is signed in.

@Positive_flow
Scenario Outline: Create a class
	When teacher creates a class with "<classname>" classname, "<subject_1>" subject_1, "<subject_2>" subject_2 and "<subject_3>" subject_3.
	Then validate class is created "<message>".

Examples:
	| classname | subject_1 | subject_2 | subject_3  | message       |
	| Class C   | Math      | History   | Biologic   | Class created |
	| Class B   | Chemistry | Physics   | Literature | Class created |

@Positive_flow
Scenario Outline: Add stuednt to class
	When teacher add student with "<name>" name and "<class_id>" class id.
	Then validate that student is added "<message>".

Examples:
	| name     | class_id                             | message       |
	| Student6 | ecadac35-dd50-4120-b876-411ec0d51cd9 | Student added |
	| Student2 | 2f2fa5e2-6c5e-4e58-80b4-bf469eff79e8 | Student added |

@Negative_flow
Scenario Outline: Try to add stuednt to class with invalid class id
	When teacher add student with "<name>" name and "<invalid_class_id>" class id.
	Then validate that student is not added "<message>".

Examples:
	| name     | invalid_class_id | message                        |
	| Student5 | invalid-class-id | Class ID is invalid or missing |

@Positive_flow
Scenario Outline: Add and update grade
	When teacher add grade: "<grade>", to student: "<student_id>", in subject: "<subject>".
	Then validate that grade is added to student "<message>".
	And teacher update grade to "<newGrade>".
	And validate that grade is updated "<newMessage>".

Examples:
	| student_id                           | subject  | grade | message     | newGrade | newMessage    |
	| 2164e5a5-8c01-40e4-9210-6f38476cdd2a | Biologic | 2     | Grade added | 5        | Grade updated |
	
@Negative_flow
Scenario Outline: Try to add grade to student with invalid id.
	When teacher add grade: "<grade>", to student: "<student_id>", in subject: "<subject>".
	Then validate that grade is added to student "<message>".

Examples:
	| student_id         | subject | grade | message                                   |
	| invalid student id | Math    | 3     | Error adding grade. Student ID is invalid |