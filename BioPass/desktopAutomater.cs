using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHotkey.Interop; //requires autohotkey.interop.dll reference, dll isfound in Login Scripts folder

namespace BioPass
{
    class desktopAutomater
    {

        /*
         * Parses automation script with credentials, runs script, then removes credentials
         */
        private void parseNrun(String User, String pw, String file)
        {
            string text = File.ReadAllText(file);
            text = text.Replace("User=", "User=" + User);
            text = text.Replace("pw=", "pw=" + pw);
            File.WriteAllText(file, text);
            var ahk = AutoHotkeyEngine.Instance; //create ahk object from dll
            ahk.LoadFile(file); //run ahk script
            text = File.ReadAllText(file); //reset  to default script skeleton
            text = text.Replace("User=" + User, "User="); //reset  to default script skeleton
            text = text.Replace("pw=" + pw, "pw="); //reset  to default script skeleton
            File.WriteAllText(file, text); //reset  to default script skeletoN
        }

        /*
         * Sets exe path for specified app in the automation script
         */
        private void setPath(String path, String App)
        {
            switch (App)
            {
                case "Steam":
                    parse(path,@"Login Scripts\SteamLogin.ahk");
                    break;
                case "Battle.Net":
                    parse(path, @"Login Scripts\BnetLogin.ahk");
                    break;
                case "Outlook":
                    parse(path, @"Login Scripts\OutlookLogin.ahk");
                    break;
            }

        }

        private void parse(String path, String script)
        {
            var lines = File.ReadAllLines(script);
            lines[9] = "run,"+path;
            File.WriteAllLines(script, lines);

        }
    }
}
