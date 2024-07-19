namespace Dotmaker
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            picOriginalImage = new PictureBox();
            picConvertedImage = new PictureBox();
            btnBrowse = new Button();
            txtFileName = new TextBox();
            trackBarColorCount = new TrackBar();
            lblColorCount = new Label();
            btnConvert = new Button();
            btnSave = new Button();
            lblDotSize = new Label();
            cmbDotSize = new ComboBox();
            btnCopy = new Button();
            ((System.ComponentModel.ISupportInitialize)picOriginalImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picConvertedImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarColorCount).BeginInit();
            SuspendLayout();
            // 
            // picOriginalImage
            // 
            picOriginalImage.BorderStyle = BorderStyle.FixedSingle;
            picOriginalImage.Location = new Point(12, 147);
            picOriginalImage.Name = "picOriginalImage";
            picOriginalImage.Size = new Size(250, 250);
            picOriginalImage.SizeMode = PictureBoxSizeMode.Zoom;
            picOriginalImage.TabIndex = 0;
            picOriginalImage.TabStop = false;
            // 
            // picConvertedImage
            // 
            picConvertedImage.BorderStyle = BorderStyle.FixedSingle;
            picConvertedImage.Location = new Point(282, 147);
            picConvertedImage.Name = "picConvertedImage";
            picConvertedImage.Size = new Size(250, 250);
            picConvertedImage.SizeMode = PictureBoxSizeMode.Zoom;
            picConvertedImage.TabIndex = 1;
            picConvertedImage.TabStop = false;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(457, 14);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "参照";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtFileName
            // 
            txtFileName.Location = new Point(12, 14);
            txtFileName.Name = "txtFileName";
            txtFileName.ReadOnly = true;
            txtFileName.Size = new Size(439, 23);
            txtFileName.TabIndex = 3;
            // 
            // trackBarColorCount
            // 
            trackBarColorCount.Location = new Point(12, 96);
            trackBarColorCount.Maximum = 100;
            trackBarColorCount.Minimum = 2;
            trackBarColorCount.Name = "trackBarColorCount";
            trackBarColorCount.Size = new Size(520, 45);
            trackBarColorCount.TabIndex = 4;
            trackBarColorCount.Value = 2;
            trackBarColorCount.Scroll += trackBarColorCount_Scroll;
            // 
            // lblColorCount
            // 
            lblColorCount.AutoSize = true;
            lblColorCount.Location = new Point(12, 78);
            lblColorCount.Name = "lblColorCount";
            lblColorCount.Size = new Size(43, 15);
            lblColorCount.TabIndex = 5;
            lblColorCount.Text = "色数: 2";
            // 
            // btnConvert
            // 
            btnConvert.Enabled = false;
            btnConvert.Location = new Point(220, 403);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(100, 36);
            btnConvert.TabIndex = 6;
            btnConvert.Text = "変換";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += btnConvert_Click;
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Location = new Point(432, 403);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 36);
            btnSave.TabIndex = 7;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblDotSize
            // 
            lblDotSize.AutoSize = true;
            lblDotSize.Location = new Point(12, 50);
            lblDotSize.Name = "lblDotSize";
            lblDotSize.Size = new Size(59, 15);
            lblDotSize.TabIndex = 9;
            lblDotSize.Text = "ドットサイズ";
            // 
            // cmbDotSize
            // 
            cmbDotSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDotSize.FormattingEnabled = true;
            cmbDotSize.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            cmbDotSize.Location = new Point(77, 47);
            cmbDotSize.Name = "cmbDotSize";
            cmbDotSize.Size = new Size(121, 23);
            cmbDotSize.TabIndex = 10;
            // 
            // btnCopy
            // 
            btnCopy.Enabled = false;
            btnCopy.Location = new Point(326, 403);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(100, 36);
            btnCopy.TabIndex = 11;
            btnCopy.Text = "コピー";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(544, 451);
            Controls.Add(btnCopy);
            Controls.Add(cmbDotSize);
            Controls.Add(lblDotSize);
            Controls.Add(btnSave);
            Controls.Add(btnConvert);
            Controls.Add(lblColorCount);
            Controls.Add(trackBarColorCount);
            Controls.Add(txtFileName);
            Controls.Add(btnBrowse);
            Controls.Add(picConvertedImage);
            Controls.Add(picOriginalImage);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            Text = "DotMaker";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)picOriginalImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)picConvertedImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarColorCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picOriginalImage;
        private PictureBox picConvertedImage;
        private Button btnBrowse;
        private TextBox txtFileName;
        private TrackBar trackBarColorCount;
        private Label lblColorCount;
        private Button btnConvert;
        private Button btnSave;
        private Label lblDotSize;
        private ComboBox cmbDotSize;
        private Button btnCopy;
    }
}
