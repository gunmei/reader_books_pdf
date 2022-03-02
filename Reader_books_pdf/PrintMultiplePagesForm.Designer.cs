namespace Reader_books_pdf
{
    partial class PrintMultiplePagesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintMultiplePagesForm));
            this.cancelButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.verticalOrientation = new System.Windows.Forms.RadioButton();
            this.horizontalOrientation = new System.Windows.Forms.RadioButton();
            this.margin = new System.Windows.Forms.TextBox();
            this.vertical = new System.Windows.Forms.TextBox();
            this.horizontal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(339, 173);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptButton.Location = new System.Drawing.Point(258, 173);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 7;
            this.acceptButton.Text = "Печать";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.verticalOrientation);
            this.groupBox1.Controls.Add(this.horizontalOrientation);
            this.groupBox1.Location = new System.Drawing.Point(106, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 70);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ориентация";
            // 
            // verticalOrientation
            // 
            this.verticalOrientation.AutoSize = true;
            this.verticalOrientation.Location = new System.Drawing.Point(13, 43);
            this.verticalOrientation.Name = "verticalOrientation";
            this.verticalOrientation.Size = new System.Drawing.Size(97, 17);
            this.verticalOrientation.TabIndex = 1;
            this.verticalOrientation.Text = "Вертикальная";
            this.verticalOrientation.UseVisualStyleBackColor = true;
            // 
            // horizontalOrientation
            // 
            this.horizontalOrientation.AutoSize = true;
            this.horizontalOrientation.Checked = true;
            this.horizontalOrientation.Location = new System.Drawing.Point(13, 20);
            this.horizontalOrientation.Name = "horizontalOrientation";
            this.horizontalOrientation.Size = new System.Drawing.Size(108, 17);
            this.horizontalOrientation.TabIndex = 0;
            this.horizontalOrientation.TabStop = true;
            this.horizontalOrientation.Text = "Горизонтальная";
            this.horizontalOrientation.UseVisualStyleBackColor = true;
            // 
            // margin
            // 
            this.margin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.margin.Location = new System.Drawing.Point(106, 64);
            this.margin.Name = "margin";
            this.margin.Size = new System.Drawing.Size(308, 20);
            this.margin.TabIndex = 5;
            this.margin.Text = "5";
            // 
            // vertical
            // 
            this.vertical.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vertical.Location = new System.Drawing.Point(106, 38);
            this.vertical.Name = "vertical";
            this.vertical.Size = new System.Drawing.Size(308, 20);
            this.vertical.TabIndex = 3;
            this.vertical.Text = "2";
            // 
            // horizontal
            // 
            this.horizontal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.horizontal.Location = new System.Drawing.Point(106, 12);
            this.horizontal.Name = "horizontal";
            this.horizontal.Size = new System.Drawing.Size(308, 20);
            this.horizontal.TabIndex = 1;
            this.horizontal.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Поле:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Вертикально:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Горизонтально:";
            // 
            // PrintMultiplePagesForm
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(445, 208);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.margin);
            this.Controls.Add(this.vertical);
            this.Controls.Add(this.horizontal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrintMultiplePagesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Печать нескольких страниц";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton verticalOrientation;
        private System.Windows.Forms.RadioButton horizontalOrientation;
        private System.Windows.Forms.TextBox margin;
        private System.Windows.Forms.TextBox vertical;
        private System.Windows.Forms.TextBox horizontal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}