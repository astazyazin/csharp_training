using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests 
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithInvalidCredentials()
        {
            //prepared
            app.Auth.Logout();
            //action
            AccountData account = new AccountData("admin", "11111");
            app.Auth.Login(account);
            //verifiacation
            Assert.IsFalse(app.Auth.IsLoggedIn(account));

        }
        [Test]
        public void LoginWithValidCredentials()
        {
            //prepared
            app.Auth.Logout();
            //action
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
            //verifiacation
            Assert.IsTrue(app.Auth.IsLoggedIn(account));

        }
        
    }
}
