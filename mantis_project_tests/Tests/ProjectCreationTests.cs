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
            AccountData admin = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            ProjectData newProject = new ProjectData()
            {
                ProjectName = "new project5",
                Description = "description"
            };
            List<ProjectData> oldProjects = app.Project.GetProjectsListByAPI(admin);
            ProjectData existingProject = oldProjects.Find(x => x.ProjectName == newProject.ProjectName);
            if (existingProject != null)    //если проект уже существует - удаляем его и получаем свежий список
            {
                app.Project.DeleteProject(existingProject.Id);
                oldProjects = app.Project.GetProjectsListByAPI(admin);
            }
            
            app.Project.AddProject(newProject);

            List<ProjectData> newProjects = app.Project.GetProjectsListByAPI(admin);
            oldProjects.Add(newProject);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);

        }
    }
}
