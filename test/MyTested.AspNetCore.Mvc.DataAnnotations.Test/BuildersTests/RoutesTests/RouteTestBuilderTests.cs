namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.RoutesTests
{
    using Exceptions;
    using Setups;
    using Xunit;

    public class RouteTestBuilderTests
    {
        [Fact]
        public void ToValidModelStateShouldNotThrowExceptionWithValidModelState()
        {
            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithPath("/Normal/ActionWithModel/5")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(@"{""Integer"":5,""String"":""Test""}"))
                .ToValidModelState();
        }

        [Fact]
        public void ToValidModelStateShouldThrowExceptionWithUnresolvedAction()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyMvc
                        .Routes()
                        .ShouldMap(request => request
                            .WithPath("/Normal/Invalid/5")
                            .WithMethod(HttpMethod.Post)
                            .WithJsonBody(@"{""Integer"":5,""String"":""Test""}"))
                        .ToValidModelState();
                },
                "Expected route '/Normal/Invalid/5' to have valid model state with no errors but action could not be matched.");
        }

        [Fact]
        public void ToValidModelStateShouldThrowExceptionWithInvalidModelState()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyMvc
                        .Routes()
                        .ShouldMap(request => request
                            .WithPath("/Normal/ActionWithModel/5")
                            .WithMethod(HttpMethod.Post)
                            .WithJsonBody(@"{""Integer"":5}"))
                        .ToValidModelState();
                },
                "Expected route '/Normal/ActionWithModel/5' to have valid model state with no errors but it had some.");
        }

        [Fact]
        public void ToInvalidModelStateShouldNotThrowExceptionWithInvalidModelState()
        {
            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithPath("/Normal/ActionWithModel/5")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(@"{""Integer"":5}"))
                .ToInvalidModelState();
        }

        [Fact]
        public void ToInvalidModelStateShouldNotThrowExceptionWithInvalidModelStateAndCorrectNumberOfErrors()
        {
            MyMvc
                .Routes()
                .ShouldMap(request => request
                    .WithPath("/Normal/ActionWithModel/5")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(@"{""Integer"":5}"))
                .ToInvalidModelState(withNumberOfErrors: 1);
        }

        [Fact]
        public void ToInvalidModelStateShouldThrowExceptionWithValidModelState()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyMvc
                        .Routes()
                        .ShouldMap(request => request
                            .WithPath("/Normal/ActionWithModel/5")
                            .WithMethod(HttpMethod.Post)
                            .WithJsonBody(@"{""Integer"":5,""String"":""Test""}"))
                        .ToInvalidModelState();
                },
                "Expected route '/Normal/ActionWithModel/5' to have invalid model state but was in fact valid.");
        }

        [Fact]
        public void ToInvalidModelStateShouldThrowExceptionWithIncorrectNumberOfErrors()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyMvc
                        .Routes()
                        .ShouldMap(request => request
                            .WithPath("/Normal/ActionWithModel/5")
                            .WithMethod(HttpMethod.Post)
                            .WithJsonBody(@"{""Integer"":5}"))
                        .ToInvalidModelState(withNumberOfErrors: 3);
                },
                "Expected route '/Normal/ActionWithModel/5' to have invalid model state with 3 errors but in fact contained 1.");
        }

        [Fact]
        public void ToInvalidModelStateShouldThrowExceptionWithIncorrectOneNumberOfErrors()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyMvc
                        .Routes()
                        .ShouldMap(request => request
                            .WithPath("/Normal/ActionWithModel/5")
                            .WithMethod(HttpMethod.Post)
                            .WithJsonBody(@"{""Integer"":5,""String"":""Test""}"))
                        .ToInvalidModelState(withNumberOfErrors: 1);
                },
                "Expected route '/Normal/ActionWithModel/5' to have invalid model state with 1 error but in fact contained 0.");
        }

        [Fact]
        public void ToInvalidModelStateShouldThrowExceptionWithUnresolvedAction()
        {
            Test.AssertException<RouteAssertionException>(
                () =>
                {
                    MyMvc
                        .Routes()
                        .ShouldMap(request => request
                            .WithPath("/Normal/Invalid/5")
                            .WithMethod(HttpMethod.Post)
                            .WithJsonBody(@"{""Integer"":5,""String"":""Test""}"))
                        .ToInvalidModelState();
                },
                "Expected route '/Normal/Invalid/5' to have invalid model state but action could not be matched.");
        }

    }
}
