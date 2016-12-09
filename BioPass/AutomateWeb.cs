using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.IO;
namespace BioPass
{
    public class automateWeb
    { 
        public ChromeDriverService service = null;
        public automateWeb(String website, long person_id)
        {
            Debug.WriteLine(website);
            if(website == null || website.Length == 0) return;
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
                string u = "test"; //Program.db.getUsername("facebook.com", person_id);
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
                int j = 0, k;
                Boolean founddata = true;
                Boolean foundUsername = false;
                Boolean foundPassword = false;
                //var pw;
                //var loginbtn;
                driver.Navigate().GoToUrl(website);
                String source = driver.PageSource;

                //Username
                int i = source.IndexOf("email");
                Debug.WriteLine("i = " + i + " length" + source.Length+ " arr[i] = "+ source[i]);
                if (i == -1)
                {
                    i = source.IndexOf("username");
                }
                if (i != -1)
                {
                    while (!foundUsername) {
                        for (; source[i] != '"'; i--)
                        {
                            Debug.WriteLine("i = " + i + " char at i = " + source[i]);
                        }
                        for (j = i + 1; source[j] != '"'; j++) ;
                        String uid = source.Substring(i + 1, j - (i + 1));
                        Debug.WriteLine(uid);
                        for (k = i; source[k] != ' '; k--) ;
                        String idtype = source.Substring(k + 1, (i - k) - 2);
                        Debug.WriteLine("k = " + k + " elemtype = " + idtype);


                        if (idtype.Equals("id"))
                        {
                            Debug.WriteLine("here");
                            var username = driver.FindElementById(uid);
                            username.SendKeys("test"); //DBhandler
                            foundUsername = true;
                        }
                        else if (idtype.Equals("name"))
                        {
                            var username = driver.FindElementByName(uid);
                            username.SendKeys("test"); //DBhandler.getUsername(uid, aid);
                            foundUsername = true;
                        }
                        else if (idtype.Equals("class"))
                        {
                            var username = driver.FindElementByName(uid);
                            username.SendKeys("test");
                            foundUsername = true;

                        }
                        else
                        {
                            i = source.IndexOf("email", j);
                            if (i == -1)
                            {
                                i = source.IndexOf("username", j);
                            }
                            if(i == -1) { foundUsername = true; founddata = false; }
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("Could not find username");
                }
                //password
                if (foundUsername == true && founddata == true)
                {
                    i = source.IndexOf("pass", j);
                    if(i == -1)
                    {
                        i = source.IndexOf("pw", j);
                    }
                    if (i != -1)
                    {
                        while (!foundPassword)
                        {
                            for (; source[i] != '"'; i--)
                            {
                                Debug.WriteLine("i = " + i + " char at i = " + source[i]);
                            }
                            for (j = i + 1; source[j] != '"'; j++) ;
                            String pid = source.Substring(i + 1, j - (i + 1));
                            Debug.WriteLine(pid);
                            for (k = i; source[k] != ' '; k--) ;
                            String idtype = source.Substring(k + 1, (i - k) - 2);
                            Debug.WriteLine("k = " + k + " elemtype = " + idtype);


                            if (idtype.Equals("id"))
                            {
                                Debug.WriteLine("here");
                                var username = driver.FindElementById(pid);
                                username.SendKeys("test"); //DBhandler
                                foundPassword = true;
                            }
                            else if (idtype.Equals("name"))
                            {
                                var username = driver.FindElementByName(pid);
                                username.SendKeys("test"); //DBhandler.getUsername(uid, aid);
                                foundPassword = true;
                            }
                            else if (idtype.Equals("class"))
                            {
                                var username = driver.FindElementByName(pid);
                                username.SendKeys("test");
                                foundPassword = true;

                            }
                            else
                            {
                                i = source.IndexOf("pass", j);
                                if (i == -1)
                                {
                                    i = source.IndexOf("pw", j);
                                }
                                if (i == -1) { foundPassword = true; founddata = false; }
                            }
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("could not find username");
                }

            }
            service.Dispose();
        }
    }
}
