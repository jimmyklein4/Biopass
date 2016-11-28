using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using System.Diagnostics;
using System.IO;
namespace BioPass
{
    public class automateWeb
    { 
        public ChromeDriverService service = null;
        public automateWeb(String website, String person_id)
        {
            service = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.LeaveBrowserRunning = true;


            var driver = new ChromeDriver(service, options);
            if (website == "tumail")
            {
                driver.Navigate().GoToUrl("http://tumail.temple.edu");
                string u = "test"; //Program.db.getUsername("tumail", person_id);
                string p = "blah"; //Program.db.getPassword("tumail", person_id);

                Debug.WriteLine(u + " " + p);

                var userNameField = driver.FindElementById("ctl00_ContentPlaceHolder1_txtUserName");
                var userPasswordField = driver.FindElementById("ctl00_ContentPlaceHolder1_txtPassword");
                var loginButton = driver.FindElementById("ctl00_ContentPlaceHolder1_btnLogin");
                try
                {
                    userNameField.SendKeys(u);
                    userPasswordField.SendKeys(p);
                    loginButton.Click();
                    //service.Dispose();
                }
                catch (Exception E)
                {
                    Console.Out.WriteLine(E);
                }
            }
            else if (website == "blackboard.temple.edu")
            {
                driver.Navigate().GoToUrl("http://blackboard.temple.edu");
                string u = "test"; // Program.db.getUsername("blackboard.temple.edu", person_id);
                string p = "test"; // Program.db.getPassword("blackboard.temple.edu", person_id);

                Debug.WriteLine(u + " " + p);

                var userNameField = driver.FindElementById("user_id");
                var userPasswordField = driver.FindElementById("password");
                var loginButton = driver.FindElementByName("login");
                try
                {
                    userNameField.SendKeys(u);
                    userPasswordField.SendKeys(p);
                    loginButton.Click();
                    //service.Dispose();
                }
                catch (Exception E)
                {
                    Console.Out.WriteLine(E);
                }
            }
            else if (website == "facebook.com")
            {
                driver.Navigate().GoToUrl("http://facebook.com");
                string u = "test";// Program.db.getUsername("facebook.com", person_id);
                string p = "test"; //Program.db.getPassword("facebook.com", person_id);

                Debug.WriteLine(u + " " + p);

                var userNameField = driver.FindElementById("email");
                var userPasswordField = driver.FindElementById("pass");
                var loginButton = driver.FindElementById("u_0_y");
                try
                {
                    userNameField.SendKeys(u);
                    userPasswordField.SendKeys(p);
                    loginButton.Click();
                    //service.Dispose()
                }
                catch (Exception E)
                {
                    Console.Out.WriteLine(E);
                }
            }
            else if (website == "en.wikipedia.org")
            {
                driver.Navigate().GoToUrl("http://en.wikipedia.org/w/index.php?title=Special:UserLogin");
                string u = "test"; //Program.db.getUsername("en.wikipedia.org", person_id);
                string p = "test"; //Program.db.getPassword("en.wikipedia.org", person_id);

                Debug.WriteLine(u + " " + p);

                var userNameField = driver.FindElementById("wpName1");
                var userPasswordField = driver.FindElementById("wpPassword1");
                var loginButton = driver.FindElementById("wpLoginAttempt");
                try
                {
                    userNameField.SendKeys(u);
                    userPasswordField.SendKeys(p);
                    loginButton.Click();
                    //service.Dispose();
                }
                catch (Exception E)
                {
                    Console.Out.WriteLine(E);
                }
            }
            else
            {

            }
            service.Dispose();
        }
    }
}
