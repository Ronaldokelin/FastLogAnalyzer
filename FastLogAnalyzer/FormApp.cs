﻿using System;
using System.IO;
using System.Windows.Forms;

namespace FastLogAnalyzer
{
    public partial class FormApp : Form
    {
        private static FormApp INSTANCE = null;
        SelectLog SL;
        ReadLog RL;
        string logTemp = string.Empty;

        public FormApp()
        {
            InitializeComponent();
            INSTANCE = this;
            createDirectory();
            ClassesInit();
        }
        private void createDirectory()
        {
            if (!Directory.Exists("temp"))
                Directory.CreateDirectory("temp");
        }
        private void ClassesInit()
        {
            SL = new SelectLog();
            RL = new ReadLog();
        }
        public static FormApp getInstance()
        {
            if (INSTANCE == null)
                INSTANCE = new FormApp();

            return INSTANCE;
        }

        private void buttonSelectLog_Click(object sender, EventArgs e)
        {
            string response = SL.SelectingLog();

            if (labelTrackIdNumber.Text != "" && SL.Extract(response))
            {
                labelTrackIdNumber.Text = response.Substring(0, 10);
                textBoxStatus.Text = Environment.NewLine + "File Imported Successfully!!!";
                reader(response);
            }

            if (textBoxAddressLog.Text != "")
                comboBoxFails.Enabled = true;
        }

        private void reader(string Folder)
        {
            string sDir = @".\temp\" + Folder + @"\prod\log\";
            string filePath = string.Empty;
            foreach (string f in Directory.GetFiles(sDir))
            {
                if (!f.Contains("startup") && f.Contains(".log"))
                    filePath = f;
            }
            logTemp = SL.AddingReaderToLog(filePath);
            RL.FillingComboBox(logTemp);
        }

        private void comboBoxFails_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxResult.Text = "";
            RL.ShowErrorOnTextBox(logTemp);
        }
    }
}
