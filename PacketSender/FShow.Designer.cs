namespace PacketSender
{
    partial class FShow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.Choose_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Debug_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Show_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pShow = new System.Windows.Forms.Panel();
            this.lbShow = new System.Windows.Forms.ListBox();
            this.lShow = new System.Windows.Forms.Label();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Choose_ToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(835, 24);
            this.Menu.TabIndex = 0;
            this.Menu.Text = "Выбор режима работы";
            // 
            // Choose_ToolStripMenuItem
            // 
            this.Choose_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Debug_ToolStripMenuItem,
            this.Show_ToolStripMenuItem});
            this.Choose_ToolStripMenuItem.Name = "Choose_ToolStripMenuItem";
            this.Choose_ToolStripMenuItem.Size = new System.Drawing.Size(147, 20);
            this.Choose_ToolStripMenuItem.Text = "Выбор режима работы";
            // 
            // Debug_ToolStripMenuItem
            // 
            this.Debug_ToolStripMenuItem.Name = "Debug_ToolStripMenuItem";
            this.Debug_ToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.Debug_ToolStripMenuItem.Text = "Режим работы для отладки";
            this.Debug_ToolStripMenuItem.Click += new System.EventHandler(this.Debug_ToolStripMenuItem_Click);
            // 
            // Show_ToolStripMenuItem
            // 
            this.Show_ToolStripMenuItem.Name = "Show_ToolStripMenuItem";
            this.Show_ToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.Show_ToolStripMenuItem.Text = "Режим работы для показа";
            // 
            // pShow
            // 
            this.pShow.Location = new System.Drawing.Point(12, 53);
            this.pShow.Name = "pShow";
            this.pShow.Size = new System.Drawing.Size(207, 356);
            this.pShow.TabIndex = 2;
            this.pShow.Paint += new System.Windows.Forms.PaintEventHandler(this.pShow_Paint);
            // 
            // lbShow
            // 
            this.lbShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbShow.FormattingEnabled = true;
            this.lbShow.ItemHeight = 16;
            this.lbShow.Location = new System.Drawing.Point(234, 53);
            this.lbShow.Name = "lbShow";
            this.lbShow.Size = new System.Drawing.Size(589, 356);
            this.lbShow.TabIndex = 3;
            this.lbShow.SelectedIndexChanged += new System.EventHandler(this.lbShow_SelectedIndexChanged);
            this.lbShow.DoubleClick += new System.EventHandler(this.lbShow_DoubleClick);
            // 
            // lShow
            // 
            this.lShow.AutoSize = true;
            this.lShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lShow.Location = new System.Drawing.Point(503, 33);
            this.lShow.Name = "lShow";
            this.lShow.Size = new System.Drawing.Size(57, 17);
            this.lShow.TabIndex = 4;
            this.lShow.Text = "ПРИЕМ";
            // 
            // FShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 427);
            this.Controls.Add(this.lShow);
            this.Controls.Add(this.lbShow);
            this.Controls.Add(this.pShow);
            this.Controls.Add(this.Menu);
            this.MainMenuStrip = this.Menu;
            this.Name = "FShow";
            this.Text = "НАИМЕНОВАНИЕ ПРОГРАММЫ";
            this.Load += new System.EventHandler(this.FShow_Load);
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem Choose_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Debug_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Show_ToolStripMenuItem;
        private System.Windows.Forms.Panel pShow;
        private System.Windows.Forms.ListBox lbShow;
        private System.Windows.Forms.Label lShow;
    }
}

