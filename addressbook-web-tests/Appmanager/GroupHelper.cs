﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebAddressbookTests

{
    public class GroupHelper : HelperBase
    {

        public GroupHelper(ApplicationManager manager) 
            : base(manager)
        {

        }

        public GroupHelper Remove()
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup();
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Modify(GroupData newGroup)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup();
            InitGroupModification();
            FillGroupForm(newGroup);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this; 
        }

        

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper RemoveGroup()
        {
            if (driver.FindElement(By.XPath("(//input[@name='selected[]'])")).Selected)
            {
                driver.FindElement(By.Name("delete")).Click();
                return this;
            }
            SelectGroup();
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public GroupHelper SelectGroup()
        {
            if (IsElementPresent(By.XPath("(//input[@name='selected[]'])")))
            {
                driver.FindElement(By.XPath("(//input[@name='selected[]'])")).Click();
                return this;
            }
            GroupData group = new GroupData("newgroup", "header", "footer");
            Create(group);
            driver.FindElement(By.XPath("(//input[@name='selected[]'])")).Click();
            return this;
        }


        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        

        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this; 
        }

        public GroupHelper InitGroupModification()
        {
            if (driver.FindElement(By.XPath("(//input[@name='selected[]'])")).Selected)
            {
                driver.FindElement(By.Name("edit")).Click();
                return this;
            }
            SelectGroup();
            driver.FindElement(By.Name("edit")).Click();
            return this; 
        }
    }
}
