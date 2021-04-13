using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.RegularExpressions;


namespace mantis_project_tests
{
    public class TestBase
    {
        protected ApplicationManager app;
        public static bool PERFORM_LONG_UI_CHECKS = false;


        [OneTimeSetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        public static Random rnd = new Random();

        public static string GenerateRandomString(int max)
        {
           
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65 )));
            }
            return Regex.Replace(builder.ToString(), "[`!@#$%^&*()_[{}<>.:/['-=+?]", "");
            // return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }
        public static string GeneratePhoneNumber()
        {
            return "+" + (rnd.Next(0000, 9999)).ToString() + "(" + (rnd.Next(000, 999)).ToString() + ")" + (rnd.Next(0000000, 9999999)).ToString();
        }

    }
}
