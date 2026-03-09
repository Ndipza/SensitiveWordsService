using FluentAssertions;
using SensitiveWords.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensitiveWords.Tests.Unit.Application
{
    public class NotFoundExceptionTests
    {
        [Fact]
        public void Should_Set_Message()
        {
            var ex = new NotFoundException("not found");

            ex.Message.Should().Contain("not found");
        }
    }
}
