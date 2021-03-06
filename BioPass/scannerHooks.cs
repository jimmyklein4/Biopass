﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioPass {
    partial class FaceForm {

        int FMatchType;
        int fpcHandle;
        string sRegTemplate, sRegTemplate10;

        private void init_fingerprint() {
             axZKFPEngX1.FakeFunOn = 0;

            if (axZKFPEngX1.InitEngine() == 0) {
                axZKFPEngX1.FPEngineVersion = "10";
                fpcHandle = axZKFPEngX1.CreateFPCacheDB();
                DataTable fingerprints = Program.db.getAllUsersFP();
                if (fingerprints != null) { 
                    foreach (DataRow dr in fingerprints.Rows) {
                        if (!dr.IsNull("fingerprint")) {
                            //Debug.WriteLine(int.Parse(dr["user_id"].ToString()));
                            String fp = System.Text.Encoding.UTF8.GetString((byte[])dr["fingerprint"]);
                            //Debug.WriteLine(fp);
                            axZKFPEngX1.AddRegTemplateStrToFPCacheDB(fpcHandle, int.Parse(dr["fp_id"].ToString()), fp);
                        }
                    }
                }
                statusBar.Text = "Sensor Connected!";
                Debug.WriteLine("FP init success!", "Information");                
            }
            else
            {
                axZKFPEngX1.EndEngine();
                Debug.WriteLine("FP init failed!", "Error"); 
            }
            FMatchType = 2;
        }

        public void beginFPRegistration() {
            if (axZKFPEngX1.IsRegister)
            {
                axZKFPEngX1.CancelEnroll();
            }
            axZKFPEngX1.EnrollCount = 3;
            axZKFPEngX1.BeginEnroll();
            statusBar.Text = "Place your finger on the scanner. (0/3)";
        }
 
        private void axZKFPEngX1_OnCapture(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEvent e) {
            //TODO, call FaceForm.recieveCapture() with array of data for Identification method below                                     
            if (FMatchType == 2) {
                int score = 8;
                int processedNum = 1;
                int ID = axZKFPEngX1.IdentificationInFPCacheDB(fpcHandle, e.aTemplate, ref score, ref processedNum);
                if (ID == -1) { 
                    MessageBox.Show("Failed to identify user by fingerprint.");
                } else { 
                    string[] array = Program.db.getUserArrayByFinger(ID);
                    string user_name = array[0];
                    if (user_name != null && user_name.Length > 0) {
                        long user_id = Int64.Parse(array[1]);
                        MessageBox.Show("User identified as: " + user_name + ". Checking other biometrics...");
                        compileUserData(user_id);
                        statusBar.Text = string.Format("Identify Succeed ID = {0} Score = {1}  Processed Number = {2}", user_id, score, processedNum);
                        //postAuth(user_id);
                    }
                }
            }
        }

        private void axZKFPEngX1_OnEnroll(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEvent e) {
            if (!e.actionResult) {
                MessageBox.Show("Register Failed!", "error!");
            } else {
                sRegTemplate = axZKFPEngX1.GetTemplateAsStringEx("9");
		        sRegTemplate10 = axZKFPEngX1.GetTemplateAsStringEx("10");

                int score = 8;
                int processedNum = 1;
                int ID = axZKFPEngX1.IdentificationInFPCacheDB(fpcHandle, e.aTemplate, ref score, ref processedNum);
                if (ID != -1) {
                    MessageBox.Show("You're already registered this fingerprint as: " + Program.db.getUserNameByFinger(ID));
                    Reset();
                    return;
                }

		        if(sRegTemplate.Length > 0) {

                    if (sRegTemplate10.Length > 0) { 
                        Debug.WriteLine("Adding FP " + Program.targetFingerprintID + " to FPCacheDB");

                        object pTemplate;
                        pTemplate = axZKFPEngX1.DecodeTemplate1(sRegTemplate10);

                        // Note: 10.0Template can not be compressed (±»Ñ¹Ëõ)
                        axZKFPEngX1.SetTemplateLen(ref pTemplate, 602);
                        String encodedTemplate = axZKFPEngX1.EncodeTemplate1(pTemplate);
                        long fpid = Program.db.registerUserFP(Program.targetFingerUserID, encodedTemplate, Program.targetFingerprintName);

                        axZKFPEngX1.AddRegTemplateStrToFPCacheDBEx(fpcHandle, (int)fpid, sRegTemplate, sRegTemplate10);

                        //axZKFPEngX1.SaveTemplate("fingerprint.tpl", pTemplate);
                        MessageBox.Show("Register Succeed", "Information!");
                        Program.fingerprintForm.PopulateDataGridView();


                    } else { 
                        MessageBox.Show("Register 10.0 failed, template length is zero", "error!");
                    }
                } else {
                    MessageBox.Show("Register Failed, template length is zero", "error!");
		        };
            }            
        }
        
        private void axZKFPEngX1_OnImageReceived(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEvent e) {
            fpPicture.Visible = true;
            Graphics g = fpPicture.CreateGraphics();
            int dc = g.GetHdc().ToInt32();
            axZKFPEngX1.PrintImageAt(dc, 0, 0, fpPicture.Width, fpPicture.Height);

        }

        private void axZKFPEngX1_OnFeatureInfo(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEvent e)
        {
            Debug.WriteLine("Scanning finger.");
            string sTemp = "";
            if (axZKFPEngX1.IsRegister)
                sTemp = "Register: Place your finger on the scanner. (" + (axZKFPEngX1.EnrollCount-axZKFPEngX1.EnrollIndex+1) + "/3)";

            sTemp = sTemp + " Fingerprint quality";
            int lastq = axZKFPEngX1.LastQuality;
            if (e.aQuality == -1)
                sTemp = sTemp + " not good, Suspicious fingerprints, quality=" + lastq.ToString();
            else if (e.aQuality != 0)
                sTemp = sTemp + " not good, quality=" + lastq.ToString();
            else
                sTemp = sTemp + " good, quality=" + lastq.ToString();
            statusBar.Text = sTemp;
        }

        private void axZKFPEngX1_OnFingerTouching(object sender, EventArgs e) {

        }

    }
}

