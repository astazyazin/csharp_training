using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace mantis_tests
{
    
    
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void SetUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open)) 
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
            
        }
        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData() 
            { 
            Name = "testuser1",
            Password = "password",
            Email = "testuser1@localhost.localdomain"
            };

            List<AccountData> oldAccounts = app.Admin.GetAllAccounts();
            AccountData existingAccount =  oldAccounts.Find(x => x.Name == account.Name);

            if (existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }
            oldAccounts = app.Admin.GetAllAccounts();
            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
            List<AccountData> newAccounts = app.Admin.GetAllAccounts();
            oldAccounts.Add(account);
            oldAccounts.Sort();
            newAccounts.Sort();
            Assert.AreEqual(oldAccounts,newAccounts);
        }
        [OneTimeTearDown]
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}
