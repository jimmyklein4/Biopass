using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioPass {
    class pinAuth : authMethod {
        public int IdentifyUser(object A) {
            String pin = (String)A;
            // This will then look user up in the DB by pin
            return 0;
        }

        public bool VerifyUser(object A, int user_id) {
            String pin = (String)A;
            // Look user up by pin in the DB and compare returned UID to user_id provided - need DB
            return false;
        }
    }
}
