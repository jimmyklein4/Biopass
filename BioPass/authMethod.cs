using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioPass {
    interface authMethod {
        int IdentifyUser(object A);
        Boolean VerifyUser(object A, int user_id);
    }
}
