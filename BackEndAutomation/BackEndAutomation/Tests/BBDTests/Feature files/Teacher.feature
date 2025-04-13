Feature: Teacher

  Teacher authentication and class management.

Background:
	Given user signs in with "teacher11" username and "teacher11" password.

Scenario: Sign in and receive a token
	Then validate that the user is signed in.

Scenario Outline: Create a class
	When teacher creates a class with "<classname>" classname, "<subject_1>" subject_1, "<subject_2>" subject_2 and "<subject_3>" subject_3.
	Then validate class is created "<message>".

Examples:
	| classname | subject_1 | subject_2 | subject_3  | message       |
	| Class A   | Math      | History   | Biologic   | Class created |
	| Class B   | Chemistry | Physics   | Literature | Class created |

Scenario Outline: Add stuednt to class
	When teacher add student with "<name>" name and "<class_id>" class id.
	Then validate that student is added "<message>".

Examples:
	| name     | class_id                             | message       |
	| Student1 | ecadac35-dd50-4120-b876-411ec0d51cd9 | Student added |
	| Student2 | 2f2fa5e2-6c5e-4e58-80b4-bf469eff79e8 | Student added |
#	| Student3 | incorrect-class-id                   | Error message |

Scenario Outline: Add grade to student.
	When teacher add grade: "<grade>", to student: "<student_id>", in subject: "<subject>".
	Then validate that grade is added to student "<message>".

Examples: 
	| student_id							 | subject | grade | message       |
	| 43bac5dc-ecba-4826-8b8d-204cecd07b18   | History | 2     | Grade added   |
	| 43bac5dc-ecba-4826-8b8d-204cecd07b18   | Math    | 5     | Grade added   |
#	| incorrect student id				     | Math    | 3     | Error message |


Scenario Outline: Update grade 
	When teacher add grade: "<grade>", to student: "<student_id>", in subject: "<subject>".
	Then validate that grade is added to student "<message>".
	Then teacher update grade to "<newGrade>".
	Then validate that grade is updated "<newMessage>".

Examples: 
	| student_id                           | subject    | grade | message       | newGrade | newMessage    |
	| 43bac5dc-ecba-4826-8b8d-204cecd07b18 | Literature | 4     | Grade added   | 5        | Grade updated |
	