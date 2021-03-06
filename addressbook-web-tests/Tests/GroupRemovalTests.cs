using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
       
        [Test]
        public void GroupRemovalTest()
        {
            //если ни одной группы нет, то создаем ее
            bool isGroupPresent = app.Groups.IsElementPresent(By.XPath("(//input[@name='selected[]'])"));
            if (!isGroupPresent)
            {
                GroupData group = new GroupData("newgroup", "header", "footer");
                app.Groups.Create(group);
            }
            app.Groups.Remove();
            
        }
                  
    }
}
