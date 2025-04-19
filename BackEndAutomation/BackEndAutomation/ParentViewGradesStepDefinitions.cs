using System;
using Reqnroll;

namespace BackEndAutomation
{
    [Binding]
    public class ParentViewGradesStepDefinitions
    {
        [When("parent view grades of student with id: {string}.")]
        public void ParentViewGrades_(string student_id)
        {
            throw new PendingStepException();
        }

        [Then("validate grades are visible.")]
        public void ThenValidateGradesAreVisible_()
        {
            throw new PendingStepException();
        }
    }
}
