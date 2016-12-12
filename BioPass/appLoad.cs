using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioPass {
    class appLoad {
        public static void loadfromJson() {
            string[] dirs = Directory.GetFiles(Directory.GetCurrentDirectory()+"/default_applications");
            for(int i = 0; i < dirs.Length; i++) {
                string text = File.ReadAllText(dirs[i]);
                JObject o = JObject.Parse(text);
                if(Program.db.appExists((string)o["name"]).Length == 0) {
                    Debug.WriteLine("Adding " + dirs[i]);
                    Program.db.addApp((string)o["name"],(string)o["type"],
                        (string)o["usernameField"],(string)o["passwordField"],
                        (string)o["submitButton"],(string)o["loginPage"]);
                }
            }
        }
    }
}
