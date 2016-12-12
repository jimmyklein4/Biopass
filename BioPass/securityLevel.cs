using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioPass {
    public partial class securityLevel : Form {
        long target = -1;
        String[] levels = {
@"Level 0 requires only one form of biometric recognition and a pin. 
Any of the combinations work:
Finger + Pin
Face + Pin",
@"Level 1 requires two forms of biometric recognition.
Finger + Pin",
@"Level 2 requires two forms of biometric recognition and a pin OR an iris scan. 
Any of the combinations work:
Face + Finger + Pin
Face + Finger + Iris
",

        };
        public securityLevel(long _target) {
            InitializeComponent();
            target = _target;
        }

        private void doneBtn_Click(object sender, EventArgs e) {
            Program.db.updateSecurityLevel(target, trackBar.Value);
            this.Close();
        }

        private void trackBar_Scroll(object sender, EventArgs e) {
            sliderLabel.Text = levels[trackBar.Value];
            //0 = Face + Pin OR Finger + Pin
            //1 = Face + Finger
            //2 = Face + Finger + Pin OR  Face + Finger + Iris
        }

        private void securityLevel_Load(object sender, EventArgs e) {
            Debug.WriteLine("User security level: " + Program.db.getUserSecurityLevel(target));
            int securityLevel = Program.db.getUserSecurityLevel(target);
            trackBar.Value = securityLevel;
            sliderLabel.Text = levels[securityLevel];
        }
    }
}
