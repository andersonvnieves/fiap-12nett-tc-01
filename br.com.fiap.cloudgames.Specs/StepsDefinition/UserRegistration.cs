using Reqnroll;

namespace br.com.fiap.cloudgames.Specs.StepsDefinition;

[Binding]
public class UserRegistration
{
    [Given("a user with first name {string}, last name {string}, email {string} and password {string}")]
    public void GivenAUserWithFirstNameLastNameEmailAndPassword(string first, string last, string p2, string p3)
    {
        ScenarioContext.StepIsPending();
    }

    [When("the user submits the registration request")]
    public void WhenTheUserSubmitsTheRegistrationRequest()
    {
        ScenarioContext.StepIsPending();
    }

    [Then("the account should be created successfully")]
    public void ThenTheAccountShouldBeCreatedSuccessfully()
    {
        ScenarioContext.StepIsPending();
    }

    [Then("the response should contain the user id")]
    public void ThenTheResponseShouldContainTheUserId()
    {
        ScenarioContext.StepIsPending();
    }

    [Given("a user with email {string} already exists")]
    public void GivenAUserWithEmailAlreadyExists(string p0)
    {
        ScenarioContext.StepIsPending();
    }

    [Then("the registration should fail")]
    public void ThenTheRegistrationShouldFail()
    {
        ScenarioContext.StepIsPending();
    }

    [Then("an error {string} should be returned")]
    public void ThenAnErrorShouldBeReturned(string p0)
    {
        ScenarioContext.StepIsPending();
    }
}