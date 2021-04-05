using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    [TestFixture]
    class GroupRemovingTests : TestBase
    {
        [Test]
        public void TestGroupRemoval()
        {
            //если ни одной группы нет, то создаем ее
     
            if (app.Groups.GetGroupList().Count == 0)
            {
                app.Groups.Add(new GroupData()
                {
                    Name = "test22"
                });
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.DeleteGroup(3);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(3);
            Assert.AreEqual(oldGroups, newGroups);

        }
    }
}
