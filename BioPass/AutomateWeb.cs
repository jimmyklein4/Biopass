using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.IO;
namespace BioPass
{
    public class automateWeb { 
        public ChromeDriverService service = null;
        public automateWeb(String website, String account_id, Boolean accountIDAsUserId = false) {
            if(accountIDAsUserId) {
                account_id = Program.db.getAppFromUID(website, account_id);
            }
            Debug.WriteLine(website);
            if(website == null || website.Length == 0) return;
            service = ChromeDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.LeaveBrowserRunning = true;

            var driver = new ChromeDriver(service, options);
            if (website == "tumail.temple.edu")
            {
                driver.Navigate().GoToUrl("http://tumail.temple.edu");
                string u = Program.db.getUsername(account_id);
                string p = Program.db.getPassword(account_id);

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
            else if (website == "learn.temple.edu")
            {
                driver.Navigate().GoToUrl("http://learn.temple.edu");
                string u = Program.db.getUsername(account_id);
                string p = Program.db.getPassword(account_id);

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
                string u = Program.db.getUsername(account_id);
                string p = Program.db.getPassword(account_id);

                Debug.WriteLine(u + " " + p);

                var userNameField = driver.FindElementById("email");
                var userPasswordField = driver.FindElementById("pass");
                var loginButton = driver.FindElementById("u_0_n");
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
                //Debug.WriteLine("i = " + i + " length" + source.Length+ " arr[i] = "+ source[i]);
                int u = source.IndexOf("username");
                if (i == -1) { i = u; }
                if (i != -1)
                {
                    while (!foundUsername) {
                        //check if the element is an input
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
                            if (isInput(source, i))
                            {
                                username.SendKeys(Program.db.getUsername(account_id)); //DBhandler
                                foundUsername = true;
                            }
                        }
                        else if (idtype.Equals("name"))
                        {
                            var username = driver.FindElementByName(uid);
                            if (isInput(source, i))
                            {
                                username.SendKeys(Program.db.getUsername(account_id)); //DBhandler.getUsername(uid, aid);
                                foundUsername = true;
                            }
                        }
                        /*else if (idtype.Equals("class"))
                        {
                            var username = driver.FindElementByClassName(uid);
                            if (isInput(source, i))
                            {
                                username.SendKeys("test");
                                foundUsername = true;
                            }
                        }*/
                        i = source.IndexOf("email", j);
                        if (i == -1)
                        {
                            i = source.IndexOf("username", j);
                        }
                        if (i == -1)
                        {
                            foundUsername = true; founddata = false;
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
                    i = source.IndexOf("pass");
                    if(i == -1)
                    {
                        i = source.IndexOf("pw");
                    }
                    if (i != -1)
                    {
                        while (!foundPassword)
                        {
                            Debug.WriteLine(i);
                            for (; source[i] != '"'; i--) { Debug.WriteLine(i); } ;
                            for (j = i + 1; source[j] != '"'; j++) ;
                            String pid = source.Substring(i + 1, j - (i + 1));
                            Debug.WriteLine(pid);
                            for (k = i; source[k] != ' '; k--) ;
                            String idtype = source.Substring(k + 1, (i - k) - 2);
                            Debug.WriteLine("k = " + k + " elemtype = " + idtype);


                            if (idtype.Equals("id"))
                            {
                                var username = driver.FindElementById(pid);
                                if (isInput(source, i))
                                {
                                    username.SendKeys(Program.db.getPassword(account_id)); //DBhandler
                                    foundPassword = true;
                                }
                            }
                            else if (idtype.Equals("name"))
                            {
                                var username = driver.FindElementByName(pid);
                                if (isInput(source, i))
                                {
                                    username.SendKeys(Program.db.getPassword(account_id)); //DBhandler.getUsername(uid, aid);
                                    foundPassword = true;
                                }
                            }
                            /*else if (idtype.Equals("class"))
                            {
                                var username = driver.FindElementByName(pid);
                                if (isInput(source, i))
                                {
                                    username.SendKeys("test");
                                    foundUsername = true;
                                }
                            }*/
                            i = source.IndexOf("pass", j);
                            if (i == -1)
                            {
                                i = source.IndexOf("pw", j);
                            }
                            if (i == -1)
                            {
                                foundPassword = true; founddata = false;
                            }
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("could not find username");
                }
                if(foundPassword == true && foundUsername == true && founddata == true)
                {
 //                   Program.db.addApp(website, )
 //                   Program.db.addAccount()
                }

            }
            service.Dispose();
        }
        private Boolean isInput(String source, int i)
        {
            int tagindex = i;
            for (tagindex = i; source[tagindex-1] != '<'; tagindex--) ;
            Debug.WriteLine("isInput: " +source.Substring(tagindex, 5));
            return (source.Substring(tagindex, 5).Equals("input"));
        }
    }
}
