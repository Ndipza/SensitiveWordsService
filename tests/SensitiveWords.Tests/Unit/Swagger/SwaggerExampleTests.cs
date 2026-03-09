using FluentAssertions;
using SensitiveWords.Api.Swagger.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensitiveWords.Tests.Unit.Swagger
{
    public class SwaggerExampleTests
    {
        [Fact]
        public void SanitizeRequestExample_Should_Return_Example()
        {
            var example = new SanitizeRequestExample();

            var result = example.GetExamples();

            result.Should().NotBeNull();
            result.Input.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void SanitizeResponseExample_Should_Return_Example()
        {
            var example = new SanitizeResponseExample();

            var result = example.GetExamples();

            result.Should().NotBeNull();
            result.Output.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void BadRequestExample_Should_Return_Example()
        {
            var example = new BadRequestExample();

            var result = example.GetExamples();

            result.Should().NotBeNull();
        }

        [Fact]
        public void DuplicateSensitiveWordExample_Should_Return_Example()
        {
            var example = new DuplicateSensitiveWordExample();

            var result = example.GetExamples();

            result.Should().NotBeNull();
        }

        [Fact]
        public void InternalServerErrorExample_Should_Return_Example()
        {
            var example = new InternalServerErrorExample();

            var result = example.GetExamples();

            result.Should().NotBeNull();
        }

        [Fact]
        public void NotFoundExample_Should_Return_Example()
        {
            var example = new NotFoundExample();

            var result = example.GetExamples();

            result.Should().NotBeNull();
        }
    }
}
