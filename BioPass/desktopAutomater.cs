using System;
using System.IO;
using AutoHotkey.Interop; //requires autohotkey.interop.dll reference, dll is found in Login Scripts folder

namespace BioPass
{
    class desktopAutomater
    {

        /*
         * Parses automation script with credentials, runs script, then removes credentials
         * ~tested~
        */
        public void parseNrun(String User, String pw, String file)
        {
            if (file == @"Login Scripts\Excel13PWProtectedWorkBook.ahk")
            {
                setPath(@"Login Scripts\Excel13PWProtectedWorkBook.ahk", "Pw Protected Excel File");
            }
            string text = File.ReadAllText(file);
            text = text.Replace("User=", "User=" + User);
            text = text.Replace("pw=", "pw=" + pw);
            File.WriteAllText(file, text);
            var ahk = AutoHotkeyEngine.Instance; //create ahk object from dll
            ahk.LoadFile(file); //run ahk script
            text = File.ReadAllText(file); //reset  to default script skeleton
            text = text.Replace("User=" + User, "User="); //reset  to default script skeleton
            text = text.Replace("pw=" + pw, "pw="); //reset  to default script skeleton
            File.WriteAllText(file, text); //reset  to default script skeleton
        }

        /*
         * Parses automation script with credentials, runs script, then removes credentials
         * ~not yet tested~
        */
        public void parseNrun1(String User, String pw, String script)
        {
            parseCreds("", User, "", pw, script); //insert credentials to automation script
            var ahk = AutoHotkeyEngine.Instance; //create ahk object from dll
            ahk.LoadFile(script); //run ahk script
            parseCreds(User, "", pw, "", script); //reset to default script skeleton
        }

        /*
         * Sets exe path for specified app in the automation script
        */
        public void setPath(String path, String App)
        {
            switch (App)
            {
                case "Steam":
                    parsePath(path, @"Login Scripts\SteamLogin.ahk");
                    break;
                case "Battle.Net":
                    parsePath(path, @"Login Scripts\BnetLogin.ahk");
                    break;
                case "Outlook":
                    parsePath(path, @"Login Scripts\Outlook13Login.ahk");
                    break;
                case "Discord":
                    parsePath(path, @"Login Scripts\DiscordLogin.ahk");
                    break;
                case "MSWord":
                    parsePath(path, @"Login Scripts\Word13Login.ahk");
                    break;
                case "Excel":
                    parsePath(path, @"Login Scripts\Excel13Login.ahk");
                    break;
                case "Access":
                    parsePath(path, @"Login Scripts\Access13Login.ahk");
                    break;
                case "Pw Protected Excel File":
                    promptPath();
                    break;
            }

        }

        /*
        * Parses exe path for specified app in the automation script
        */
        public void parsePath(String path, String script)
        {
            var lines = File.ReadAllLines(script);
            lines[8] = "run, " + path;
            File.WriteAllLines(script, lines);
        }

        /*
        * Parses user credentials for specified app in the automation script
        */
        private void parseCreds(String User0, String User1, String pw0, String pw1, String script)
        {
            string text = File.ReadAllText(script);
            text = text.Replace("User=" + User0, "User=" + User1);
            text = text.Replace("pw=" + pw0, "pw=" + pw1);
            File.WriteAllText(script, text);
        }

        /*
         * For excel password protected document, user will be prompted to select path of file. Then this path
         * is input to automation script.
         */
        private void promptPath()
        {
            pathPrompt excelPath = new pathPrompt();
            excelPath.ShowDialog();
                
        }

    }
}
