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

            String uid = null, pid = null, buttonId = null;

            var driver = new ChromeDriver(service, options);
            //write if statement that checks if the app exists
            if (Program.db.appExists(website)) {
                String actualPath = website;
                if(website.IndexOf(@"://") == -1) {
                    actualPath = "http://" + website;
                }
                driver.Navigate().GoToUrl(actualPath);

                String usernameFieldName = "input"+Program.db.getUsernameFieldByWebsite(website);
                String passwordFieldName = "input"+ Program.db.getPasswordFieldByWebsite(website);
                IWebElement usernameField = null;
                IWebElement passwordField = null;
                try { 
                    usernameField = driver.FindElementByCssSelector(usernameFieldName);
                    passwordField = driver.FindElementByCssSelector(passwordFieldName);
                } catch (Exception E) {
                    Console.Out.WriteLine(E);
                }
                string u = Program.db.getUsername(account_id);
                string p = Program.db.getPassword(account_id);

                try { 
                    usernameField.SendKeys(u);
                    passwordField.SendKeys(p);
                    passwordField.SendKeys(Keys.Return);
                } catch (Exception E) {
                    Console.Out.WriteLine(E);
                }

            }  else {
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
                    while (!foundUsername)
                    {
                        //check if the element is an input
                        for (; source[i] != '"'; i--)
                        {
                            Debug.WriteLine("i = " + i + " char at i = " + source[i]);
                        }
                        for (j = i + 1; source[j] != '"'; j++) ;
                        uid = source.Substring(i + 1, j - (i + 1));
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
                                username.SendKeys(Program.db.getUsername(account_id)); 
                                foundUsername = true;
                            }
                        }
                        else if (idtype.Equals("name"))
                        {
                            var username = driver.FindElementByName(uid);
                            if (isInput(source, i))
                            {
                                username.SendKeys(Program.db.getUsername(account_id)); 
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
                    if (i == -1)
                    {
                        i = source.IndexOf("pw");
                    }
                    if (i != -1)
                    {
                        while (!foundPassword)
                        {
                            Debug.WriteLine(i);
                            for (; source[i] != '"'; i--) { Debug.WriteLine(i); };
                            for (j = i + 1; source[j] != '"'; j++) ;
                            pid = source.Substring(i + 1, j - (i + 1));
                            Debug.WriteLine(pid);
                            for (k = i; source[k] != ' '; k--) ;
                            String idtype = source.Substring(k + 1, (i - k) - 2);
                            Debug.WriteLine("k = " + k + " elemtype = " + idtype);


                            if (idtype.Equals("id"))
                            {
                                var username = driver.FindElementById(pid);
                                if (isInput(source, i))
                                {
                                    username.SendKeys(Program.db.getPassword(account_id)); 
                                    foundPassword = true;
                                }
                            }
                            else if (idtype.Equals("name"))
                            {
                                var username = driver.FindElementByName(pid);
                                if (isInput(source, i))
                                {
                                    username.SendKeys(Program.db.getPassword(account_id)); 
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
                if (foundPassword == true && foundUsername == true && founddata == true)
                {
                    String cleanPath = website.Substring(website.IndexOf("://")+3);
                    cleanPath = cleanPath.Substring(0,website.IndexOf("/"));
                    Program.db.addApp(cleanPath, "website", uid, pid, buttonId, website);
                    //Program.db.addAccount();
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
