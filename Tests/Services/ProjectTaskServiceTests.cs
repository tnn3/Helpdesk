using System.Collections.Generic;
using System.Linq;
using Domain;
using Services;
using Xunit;

namespace Tests.Services
{
    public class ProjectTaskServiceTests
    {
        [Fact]
        public void Logging()
        {
            var newTask = MockTask();
            var oldTask = MockTask2();
            var service = new ProjectTaskService();
            var user = MockUser();
            service.LogChanges(newTask, oldTask, user);
            var changes = newTask.ChangeSets.First().Changes;
            
            Assert.Equal(12, changes.Count);
        }

        [Fact]
        public void DisplayNameFromAChangesFieldname()
        {
            var changes = MockChanges();

            var displayValues = new List<string>();
            foreach (var changee in changes)
            {
                var fieldName = changee.Fieldname;
                var field = typeof(ProjectTask).GetProperty(fieldName);
                object displayValue = fieldName;
                if (field.CustomAttributes.Any(ca => ca.NamedArguments.Any()))
                {
                    var fieldAttribute = field.CustomAttributes.First(ca => ca.NamedArguments.Any()).NamedArguments.First();
                    displayValue = fieldAttribute.TypedValue.Value;
                }

                displayValues.Add(displayValue.ToString());
            }

            Assert.Contains("Paid work", displayValues);
            Assert.Contains("Amount done", displayValues);
            Assert.Contains("Title", displayValues);
        }

        private ProjectTask MockTask()
        {
            return new ProjectTask
            {
                AmountDone = 12,
                ClientPhone = "533123",
                ClientName = "Someone",
                ClientEmail = "asd@asd.asd",
                ComponentPrice = 0,
                PriorityId = 1,
                Priority = new Priority{ Id = 1, Name = "Urgent"},
                StatusId = 1,
                Status = new Status { Id = 1, Name = "Open"},
                PaidWork = false,
                Description = "Some desc",
                Price = 5,
                Title = "Big title",
                AssigneeId = MockUser().Id,
                Assignee = MockUser()
            };
        }

        private ProjectTask MockTask2()
        {
            return new ProjectTask
            {
                AmountDone = 50,
                ClientPhone = "533125",
                ClientName = "Someone else",
                ClientEmail = "asd@asd.asde",
                ComponentPrice = 5,
                PriorityId = 2,
                Priority = new Priority { Id = 2, Name = "Low" },
                StatusId = 2,
                Status = new Status { Id = 2, Name = "Closed" },
                PaidWork = true,
                Description = "Some description",
                Price = 7,
                Title = "Big titles",
                AssigneeId = MockUser2().Id,
                Assignee = MockUser2()
            };
        }

        private ApplicationUser MockUser()
        {
            return new ApplicationUser
            {
                Id = "123123",
                Firstname = "Meelis",
                Lastname = "Maasikas"
            };
        }

        private ApplicationUser MockUser2()
        {
            return new ApplicationUser
            {
                Id = "123123asd",
                Firstname = "Mari",
                Lastname = "Maasikas"
            };
        }

        private IEnumerable<Change> MockChanges()
        {
            return new List<Change>
            {
                new Change
                {
                    Before = "Something",
                    After = "Changed",
                    Fieldname = "Title"
                },
                new Change
                {
                    Before = "False",
                    After = "True",
                    Fieldname = "PaidWork"
                },
                new Change
                {
                    Before = "0",
                    After = "50",
                    Fieldname = "AmountDone"
                }
            };
        }
    }
}
