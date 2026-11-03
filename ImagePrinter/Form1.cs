using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace ImagePrinter
{
    public partial class Form1 : Form
    {
        private List<string> _targetFilePaths = new List<string>();
        private int _currentPrintIndex = 0;

        public Form1()
        {
            InitializeComponent();
            SetupModernUI();
        }

        private void SetupModernUI()
        {
            this.Text = "Image Print";
            this.Size = new Size(520, 380);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // UI setup code (card panel, buttons, drag-drop, etc.)
            // Full implementation includes file selection and print button.
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_currentPrintIndex >= _targetFilePaths.Count) return;

            string filePath = _targetFilePaths[_currentPrintIndex];
            using (Image sourceImage = Image.FromFile(filePath))
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                float printWidth = 400f;
                float printHeight = 300f;
                float startX = 50f;
                float startY = 50f;
                RectangleF destRect = new RectangleF(startX, startY, printWidth, printHeight);
                g.DrawImage(sourceImage, destRect);

                // Text rows with justified Korean labels
                // Date priority: EXIF DateTimeOriginal (0x9003) > DateTimeDigitized > LastWriteTime

                _currentPrintIndex++;
                e.HasMorePages = (_currentPrintIndex < _targetFilePaths.Count);
            }
        }

        // Additional methods:
        // - GetDateTakenOrModified
        // - DrawJustifiedLabel / DrawDistributedChars
    }
}
