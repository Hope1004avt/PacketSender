namespace PacketSender
{
    partial class FDebug
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bDelete = new System.Windows.Forms.Button();
            this.pButton = new System.Windows.Forms.Panel();
            this.hexString = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lName = new System.Windows.Forms.Label();
            this.lReceive = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.bSaveFile = new System.Windows.Forms.Button();
            this.lPort = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.lAddress = new System.Windows.Forms.Label();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.bSend = new System.Windows.Forms.Button();
            this.tbHexString = new System.Windows.Forms.TextBox();
            this.lTransmit = new System.Windows.Forms.Label();
            this.lbTransmit = new System.Windows.Forms.ListBox();
            this.lbReceive = new System.Windows.Forms.ListBox();
            this.Menu1 = new System.Windows.Forms.MenuStrip();
            this.ModeWorkOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.DebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bDelete
            // 
            this.bDelete.Location = new System.Drawing.Point(923, 88);
            this.bDelete.Margin = new System.Windows.Forms.Padding(5);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(77, 22);
            this.bDelete.TabIndex = 159;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // pButton
            // 
            this.pButton.AutoScroll = true;
            this.pButton.Location = new System.Drawing.Point(1, 157);
            this.pButton.Margin = new System.Windows.Forms.Padding(0);
            this.pButton.Name = "pButton";
            this.pButton.Size = new System.Drawing.Size(241, 258);
            this.pButton.TabIndex = 158;
            // 
            // hexString
            // 
            this.hexString.AutoSize = true;
            this.hexString.Location = new System.Drawing.Point(8, 61);
            this.hexString.Name = "hexString";
            this.hexString.Size = new System.Drawing.Size(29, 13);
            this.hexString.TabIndex = 157;
            this.hexString.Text = "HEX";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbName.Location = new System.Drawing.Point(51, 28);
            this.tbName.Margin = new System.Windows.Forms.Padding(5);
            this.tbName.Multiline = true;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(615, 20);
            this.tbName.TabIndex = 156;
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(8, 28);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(35, 13);
            this.lName.TabIndex = 155;
            this.lName.Text = "Name";
            // 
            // lReceive
            // 
            this.lReceive.AutoSize = true;
            this.lReceive.Location = new System.Drawing.Point(255, 259);
            this.lReceive.Margin = new System.Windows.Forms.Padding(5);
            this.lReceive.Name = "lReceive";
            this.lReceive.Size = new System.Drawing.Size(46, 13);
            this.lReceive.TabIndex = 145;
            this.lReceive.Text = "ПРИЕМ";
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(749, 88);
            this.bSave.Margin = new System.Windows.Forms.Padding(5);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(77, 22);
            this.bSave.TabIndex = 154;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bSaveFile
            // 
            this.bSaveFile.Location = new System.Drawing.Point(883, 422);
            this.bSaveFile.Margin = new System.Windows.Forms.Padding(5);
            this.bSaveFile.Name = "bSaveFile";
            this.bSaveFile.Size = new System.Drawing.Size(117, 26);
            this.bSaveFile.TabIndex = 153;
            this.bSaveFile.Text = "Сохранить в файл";
            this.bSaveFile.UseVisualStyleBackColor = true;
            this.bSaveFile.Click += new System.EventHandler(this.bSaveFile_Click);
            // 
            // lPort
            // 
            this.lPort.AutoSize = true;
            this.lPort.Location = new System.Drawing.Point(184, 93);
            this.lPort.Name = "lPort";
            this.lPort.Size = new System.Drawing.Size(26, 13);
            this.lPort.TabIndex = 152;
            this.lPort.Text = "Port";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(216, 90);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(80, 20);
            this.tbPort.TabIndex = 151;
            this.tbPort.Text = "161";
            this.tbPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPort_KeyPress);
            // 
            // lAddress
            // 
            this.lAddress.AutoSize = true;
            this.lAddress.Location = new System.Drawing.Point(8, 93);
            this.lAddress.Name = "lAddress";
            this.lAddress.Size = new System.Drawing.Size(45, 13);
            this.lAddress.TabIndex = 150;
            this.lAddress.Text = "Address";
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(59, 90);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(101, 20);
            this.tbAddress.TabIndex = 149;
            this.tbAddress.Text = "127.0.0.1";
            this.tbAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbAddress_KeyPress);
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(836, 88);
            this.bSend.Margin = new System.Windows.Forms.Padding(5);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(77, 22);
            this.bSend.TabIndex = 148;
            this.bSend.Text = "Send";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // tbHexString
            // 
            this.tbHexString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHexString.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbHexString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbHexString.Location = new System.Drawing.Point(51, 58);
            this.tbHexString.Margin = new System.Windows.Forms.Padding(5);
            this.tbHexString.Multiline = true;
            this.tbHexString.Name = "tbHexString";
            this.tbHexString.Size = new System.Drawing.Size(949, 20);
            this.tbHexString.TabIndex = 147;
            this.tbHexString.TextChanged += new System.EventHandler(this.tbHexString_TextChanged);
            this.tbHexString.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbHexString_KeyPress);
            // 
            // lTransmit
            // 
            this.lTransmit.AutoSize = true;
            this.lTransmit.Location = new System.Drawing.Point(255, 135);
            this.lTransmit.Margin = new System.Windows.Forms.Padding(5);
            this.lTransmit.Name = "lTransmit";
            this.lTransmit.Size = new System.Drawing.Size(67, 13);
            this.lTransmit.TabIndex = 146;
            this.lTransmit.Text = "ПЕРЕДАЧА";
            // 
            // lbTransmit
            // 
            this.lbTransmit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTransmit.FormattingEnabled = true;
            this.lbTransmit.HorizontalScrollbar = true;
            this.lbTransmit.Location = new System.Drawing.Point(258, 156);
            this.lbTransmit.Name = "lbTransmit";
            this.lbTransmit.Size = new System.Drawing.Size(747, 95);
            this.lbTransmit.TabIndex = 143;
            this.lbTransmit.DoubleClick += new System.EventHandler(this.lbTransmit_DoubleClick);
            // 
            // lbReceive
            // 
            this.lbReceive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbReceive.FormattingEnabled = true;
            this.lbReceive.HorizontalScrollbar = true;
            this.lbReceive.Location = new System.Drawing.Point(258, 280);
            this.lbReceive.Name = "lbReceive";
            this.lbReceive.Size = new System.Drawing.Size(747, 134);
            this.lbReceive.TabIndex = 144;
            this.lbReceive.DoubleClick += new System.EventHandler(this.lbTransmit_DoubleClick);
            // 
            // Menu1
            // 
            this.Menu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModeWorkOpen});
            this.Menu1.Location = new System.Drawing.Point(0, 0);
            this.Menu1.Name = "Menu1";
            this.Menu1.Size = new System.Drawing.Size(1014, 24);
            this.Menu1.TabIndex = 160;
            this.Menu1.Text = "Menu";
            // 
            // ModeWorkOpen
            // 
            this.ModeWorkOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DebugToolStripMenuItem,
            this.ShowToolStripMenuItem});
            this.ModeWorkOpen.Name = "ModeWorkOpen";
            this.ModeWorkOpen.Size = new System.Drawing.Size(147, 20);
            this.ModeWorkOpen.Text = "Выбор режима работы";
            // 
            // DebugToolStripMenuItem
            // 
            this.DebugToolStripMenuItem.Name = "DebugToolStripMenuItem";
            this.DebugToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.DebugToolStripMenuItem.Text = "Режим работы для отладки";
            this.DebugToolStripMenuItem.Click += new System.EventHandler(this.OpenModeToolStripMenuItem_Click);
            // 
            // ShowToolStripMenuItem
            // 
            this.ShowToolStripMenuItem.Name = "ShowToolStripMenuItem";
            this.ShowToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.ShowToolStripMenuItem.Text = "Режим работы для показа";
            this.ShowToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // FDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 459);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.pButton);
            this.Controls.Add(this.hexString);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lName);
            this.Controls.Add(this.lReceive);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bSaveFile);
            this.Controls.Add(this.lPort);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.lAddress);
            this.Controls.Add(this.tbAddress);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.tbHexString);
            this.Controls.Add(this.lTransmit);
            this.Controls.Add(this.lbTransmit);
            this.Controls.Add(this.lbReceive);
            this.Controls.Add(this.Menu1);
            this.Name = "FDebug";
            this.Text = "НАИМЕНОВАНИЕ ПРОГРАММЫ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FDebug_FormClosing);
            this.Load += new System.EventHandler(this.FDebug_Load);
            this.Click += new System.EventHandler(this.bSave_Click);
            this.Menu1.ResumeLayout(false);
            this.Menu1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Panel pButton;
        private System.Windows.Forms.Label hexString;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lName;
        private System.Windows.Forms.Label lReceive;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bSaveFile;
        private System.Windows.Forms.Label lPort;
        public System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label lAddress;
        public System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.TextBox tbHexString;
        private System.Windows.Forms.Label lTransmit;
        public System.Windows.Forms.ListBox lbTransmit;
        public System.Windows.Forms.ListBox lbReceive;
        private System.Windows.Forms.MenuStrip Menu1;
        private System.Windows.Forms.ToolStripMenuItem ModeWorkOpen;
        private System.Windows.Forms.ToolStripMenuItem DebugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowToolStripMenuItem;
    }
}