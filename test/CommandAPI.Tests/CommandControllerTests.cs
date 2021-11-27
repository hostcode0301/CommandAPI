using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Controllers;
using Moq;
using CommandAPI.Data;
using CommandAPI.Models;
using CommandAPI.Profiles;
using AutoMapper;
using CommandAPI.Dtos;

namespace CommandAPI.Tests
{
    public class CommandControllerTests : IDisposable
    {
        Mock<ICommandAPIRepo> mockRepo;
        CommandProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandControllerTests()
        {
            mockRepo = new Mock<ICommandAPIRepo>();
            realProfile = new CommandProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        public void Dispose()
        {
            mockRepo = null;
            realProfile = null;
            configuration = null;
            mapper = null;
        }

        [Fact]
        public async Task GetCommandItems_Return200OK_WhenDBIsEmpty()
        {
            //Given
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
            var controller = new CommandsController(mockRepo.Object, mapper);

            //When
            var rs = await controller.GetAllCommands();

            //Then
            Assert.IsType<OkObjectResult>(rs.Result);
        }

        [Fact]
        public async Task GetCommandItems_ReturnOneItem_WhenDBHasOneResource()
        {
            //Given
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepo.Object, mapper);

            //When
            var rs = await controller.GetAllCommands();

            //Then
            var okResult = rs.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;

            Assert.Single(commands);
        }

        private List<Command> GetCommands(int num)
        {
            var commands = new List<Command>();
            if (num > 0)
            {
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }
            return commands;
        }
    }
}