using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace mantis_project_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void CreationNewProject()
        {
            
            ProjectData newProject = new ProjectData()
            {
                ProjectName = "new project5",
                Description = "description"
            };
            List<ProjectData> oldProjects = app.Project.GetProjectsList();
            
            app.Project.AddProject(newProject);

            List<ProjectData> newProjects = app.Project.GetProjectsList();
            oldProjects.Add(newProject);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);


        }
    }
}
