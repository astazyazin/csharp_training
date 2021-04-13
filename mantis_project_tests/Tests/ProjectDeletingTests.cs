using NUnit.Framework;
using System.Collections.Generic;

namespace mantis_project_tests
{
    [TestFixture]
    public class ProjectDeletingTests : AuthTestBase
    {
        [Test]
        public void DeletingProject()
        {
           
            List<ProjectData> oldProjects = app.Project.GetProjectsList();
            // если нет проектов, то создать новый
            if (oldProjects.Count == 0)
            {
                ProjectData newProject = new ProjectData()
                {
                    ProjectName = "new project5",
                    Description = "description"
                };
                app.Project.AddProject(newProject);
            }

            oldProjects = app.Project.GetProjectsList();

            app.Project.DeleteProject(oldProjects[0].Id);

            List<ProjectData> newProjects = app.Project.GetProjectsList();
            oldProjects.RemoveAt(0);
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
