using Reqnroll;

namespace br.com.fiap.cloudgames.Specs.StepsDefinition;

[Binding]
public class UserAuthentication
{
    [Given("a user with email {string} and password {string} exists")]
    public void GivenAUserWithEmailAndPasswordExists(string p0, string p1)
    {
        ScenarioContext.StepIsPending();
    }

    [When("the user submits the login request with email {string} and password {string}")]
    public void WhenTheUserSubmitsTheLoginRequestWithEmailAndPassword(string p0, string p1)
    {
        ScenarioContext.StepIsPending();
    }

    [Then("the authentication should be successful")]
    public void ThenTheAuthenticationShouldBeSuccessful()
    {
        ScenarioContext.StepIsPending();
    }

    [Then("the response should contain an access token")]
    public void ThenTheResponseShouldContainAnAccessToken()
    {
        ScenarioContext.StepIsPending();
    }

    [Then("the authentication should fail")]
    public void ThenTheAuthenticationShouldFail()
    {
        ScenarioContext.StepIsPending();
    }

    [Given("no user exists with email {string}")]
    public void GivenNoUserExistsWithEmail(string p0)
    {
        ScenarioContext.StepIsPending();
    }
}