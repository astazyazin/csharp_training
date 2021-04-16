using System;
using OpenQA.Selenium;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace mantis_project_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager)
            : base(manager)
        {

        }
       
        public void AddProject(ProjectData project)
        {
            manager.Menu.GoToManageProjectsPage();
            manager.Menu.GoToManageProjectsTab();
            InitCreationNewProject();
            FillNewProject(project);
            SubmitNewProject();
        }
        public void AddProjectByAPI(ProjectData project, AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData issue = new Mantis.ProjectData
            {
                name = project.ProjectName,
                description = project.Description
            };
            client.mc_project_add(account.Name, account.Password, issue);
        }

        public List<ProjectData> GetProjectsList()
        {
            manager.Menu.GoToManageProjectsPage();
            manager.Menu.GoToManageProjectsTab();
            List<ProjectData> projects = new List<ProjectData>();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("body > div:nth-child(3) > div:nth-child(2)" +
                " > div:nth-child(2) > div:nth-child(1) > div:nth-child(2) > div:nth-child(2) > div:nth-child(2) > div:nth-child(1) " +
                "> div:nth-child(2) > table:nth-child(1) > tbody:nth-child(2) > tr"));
            foreach (IWebElement element in elements)
            {
                string s = element.FindElement(By.XPath("td[1]//a")).GetAttribute("href");
                s = s.Substring(s.Length - 4);
                Match match = Regex.Match(s, @"(\d+)"); // выдергиваем Id из последних 4 символов href
                projects.Add(new ProjectData() 
                {
                    ProjectName = element.FindElement(By.XPath("td[1]")).Text,
                    Description = element.FindElement(By.XPath("td[5]")).Text,
                    Id = Convert.ToInt32(match.Value)
                });
            }
            return projects;
        }

        public List<ProjectData> GetProjectsListByAPI(AccountData account)
        {
            List<ProjectData> projects = new List<ProjectData>();
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData [] projectsAPI = client.mc_projects_get_user_accessible(account.Name, account.Password);
            for(int i=0; i < projectsAPI.Length; i++)
            {
                projects.Add(new ProjectData()
                {
                    ProjectName = projectsAPI[i].name,
                    Description = projectsAPI[i].description,
                    Id = Convert.ToInt32(projectsAPI[i].id)
                });
            }
            return projects;
        }



        private void InitCreationNewProject()
        {
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        private void SubmitNewProject()
        {
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }

        private void FillNewProject(ProjectData project)
        {
            Type(By.Id("project-name"), project.ProjectName);
            Type(By.Name("description"), project.Description);
            
        }

        public void DeleteProject(int index)
        {
            manager.Menu.GoToManageProjectsPage();
            manager.Menu.GoToManageProjectsTab();
            driver.FindElement(By.XPath("//a[@href='manage_proj_edit_page.php?project_id=" + index + "']")).Click();
            ClickDeletingButton();
            ClickDeletingButton();
        }

        private void ClickDeletingButton()
        {
            driver.FindElement(By.XPath("//input[@type='submit'][@value='Delete Project']")).Click();
        }
    }
}
