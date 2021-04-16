using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace mantis_tests 
{ 
    [TestFixture]
    public class AddNewIssues : TestBase
    {
        [Test]
        public void AddNewIssue()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            ProjectData project = new ProjectData()
            {
                Id = "11"
            };
            IssueData issueData = new IssueData()
            {
                Summary = "some text",
                Description = "some loooong text",
                Category = "General"
            };
            app.Api.CreateNewIssue(account, project, issueData);
        }
    }
}
