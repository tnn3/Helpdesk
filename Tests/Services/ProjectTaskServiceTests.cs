using System.Linq;
using Domain;
using Services;
using Xunit;

namespace Tests.Services
{
    public class ProjectTaskServiceTests
    {
        [Fact]
        public void TestLogging()
        {
            var newTask = new ProjectTask
            {
                AmountDone = 12,
                ClientPhone = "533123",
                ClientName = "Someone",
                ClientEmail = "asd@asd.asd",
                ComponentPrice = 0,
                Priority = new Priority { Id = 1},
                Status = new Status { Id = 1},
                PaidWork = false,
                Description = "Some desc",
                Price = 5,
                Title = "Big title"
            };
            var oldTask = new ProjectTask
            {
                AmountDone = 50,
                ClientPhone = "533125",
                ClientName = "Someone else",
                ClientEmail = "asd@asd.asde",
                ComponentPrice = 5,
                Priority = new Priority { Id = 2 },
                Status = new Status { Id = 2 },
                PaidWork = true,
                Description = "Some description",
                Price = 7,
                Title = "Big titles"
            };
            var service = new ProjectTaskService();
            var user = new ApplicationUser
            {
                Id = "123123"
            };
            service.LogChanges(newTask, oldTask, user);
            var changes = newTask.ChangeSets.First().Changes;
            Assert.Equal(changes.Count, 11);
        }
    }
}
