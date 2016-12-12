using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
@"Level 2 requires two forms of biometric recognition and a pin. 
Face + Finger + Pin",
@"Level 3 requires three forms of biometric recognition. 
Face + Finger + Iris",
@"Level 4 requires three forms of biometric recognition and a pin. 
Face + Finger + Iris + Pin "
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
            //2 = Face + Finger + Pin
            //3 = Face + Finger + Iris
            //4 = Face + Finger + Pin + Iris
        }

        private void securityLevel_Load(object sender, EventArgs e) {
            sliderLabel.Text = levels[trackBar.Value];
        }
    }
}
