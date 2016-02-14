namespace PostalDove
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.OptionsStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.testSendingStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.subjTxtBox = new System.Windows.Forms.TextBox();
            this.bodyTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.htmlCheckBox = new System.Windows.Forms.CheckBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsStrip,
            this.testSendingStrip,
            this.AboutStrip});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(343, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // OptionsStrip
            // 
            this.OptionsStrip.Name = "OptionsStrip";
            this.OptionsStrip.Size = new System.Drawing.Size(79, 20);
            this.OptionsStrip.Text = "Настройки";
            // 
            // testSendingStrip
            // 
            this.testSendingStrip.Name = "testSendingStrip";
            this.testSendingStrip.Size = new System.Drawing.Size(121, 20);
            this.testSendingStrip.Text = "Тестовая отправка";
            // 
            // AboutStrip
            // 
            this.AboutStrip.Name = "AboutStrip";
            this.AboutStrip.Size = new System.Drawing.Size(94, 20);
            this.AboutStrip.Text = "О программе";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тема письма:";
            // 
            // subjTxtBox
            // 
            this.subjTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subjTxtBox.Location = new System.Drawing.Point(15, 61);
            this.subjTxtBox.Name = "subjTxtBox";
            this.subjTxtBox.Size = new System.Drawing.Size(310, 20);
            this.subjTxtBox.TabIndex = 2;
            // 
            // bodyTxtBox
            // 
            this.bodyTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bodyTxtBox.Location = new System.Drawing.Point(15, 114);
            this.bodyTxtBox.Multiline = true;
            this.bodyTxtBox.Name = "bodyTxtBox";
            this.bodyTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.bodyTxtBox.Size = new System.Drawing.Size(310, 193);
            this.bodyTxtBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Текст письма:";
            // 
            // htmlCheckBox
            // 
            this.htmlCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.htmlCheckBox.AutoSize = true;
            this.htmlCheckBox.Location = new System.Drawing.Point(15, 324);
            this.htmlCheckBox.Name = "htmlCheckBox";
            this.htmlCheckBox.Size = new System.Drawing.Size(135, 17);
            this.htmlCheckBox.TabIndex = 5;
            this.htmlCheckBox.Text = "Текст в формате html";
            this.htmlCheckBox.UseVisualStyleBackColor = true;
            // 
            // sendBtn
            // 
            this.sendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendBtn.Location = new System.Drawing.Point(237, 350);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(88, 33);
            this.sendBtn.TabIndex = 6;
            this.sendBtn.Text = "Отправить";
            this.sendBtn.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 395);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.htmlCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bodyTxtBox);
            this.Controls.Add(this.subjTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(359, 434);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Postal Dove";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem OptionsStrip;
        private System.Windows.Forms.ToolStripMenuItem AboutStrip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox subjTxtBox;
        private System.Windows.Forms.TextBox bodyTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem testSendingStrip;
        private System.Windows.Forms.CheckBox htmlCheckBox;
        private System.Windows.Forms.Button sendBtn;
    }
}

