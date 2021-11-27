using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandTests
    {
        [Fact]
        public void CanChangeHowTo()
        {
            var testCommand = new Command
            {
                HowTo = "Do Something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };

            testCommand.HowTo = "Execute Unit Tests";

            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }
    }
}