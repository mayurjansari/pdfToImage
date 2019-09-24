using BrighterTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pdfToImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "pdf";
            openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                string appDomPath = AppDomain.CurrentDomain.BaseDirectory;
                string GhostScriptFolder = appDomPath;
                string InputFilename = filename;
                string OutputFolder = appDomPath;
                string TempFolder = Path.Combine(appDomPath, "tmp");
                if (!Directory.Exists(TempFolder))
                    Directory.CreateDirectory(TempFolder);
                int OutputResolution = 300;
                List<string> OutputFilenames;

                lock (typeof(BrighterTools.GhostScript))
                {
                    GC.Collect();

                    // Select output device  
                    BrighterTools.GhostScript.OutputDevice outputDevice = GhostScript.OutputDevice.jpeg;
                    // Select output device options
                    BrighterTools.GhostScript.DeviceOption[] deviceOptions = BrighterTools.GhostScript.DeviceOptions.jpg(100);

                    using (BrighterTools.GhostScript ghostScript = new BrighterTools.GhostScript(GhostScriptFolder))
                    {
                        OutputFilenames = ghostScript.Convert(outputDevice, deviceOptions, InputFilename, OutputFolder, String.Empty, TempFolder, OutputResolution);
                    }
                }

            }

        }
    }
}
