using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    [TestFixture]

    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newGroup = new GroupData("newgroup1",null,null);
            //если ни одной группы нет, то создаем ее
            bool isGroupPresent = app.Groups.IsElementPresent(By.XPath("(//input[@name='selected[]'])"));
            if (!isGroupPresent)
            {
                GroupData group = new GroupData("newgroup", "header", "footer");
                app.Groups.Create(group);
            }
            app.Groups.Modify(newGroup);
        }
    }
}
