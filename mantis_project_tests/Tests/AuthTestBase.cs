using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace mantis_project_tests
{
    public class AuthTestBase : TestBase
    {
        
        [SetUp]
        public void SetupLogin()
        {

            app.Auth.Login(new AccountData()
            {
                Name = "administrator",
                Password = "root",
                Email = "root@localhost"
            });
        }
   
    }
}
