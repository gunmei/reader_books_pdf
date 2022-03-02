namespace Reader_books_pdf
{
    partial class SearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            this.findNext = new System.Windows.Forms.Button();
            this.findPrevious = new System.Windows.Forms.Button();
            this.highlightAll = new System.Windows.Forms.CheckBox();
            this.matchWholeWord = new System.Windows.Forms.CheckBox();
            this.matchCase = new System.Windows.Forms.CheckBox();
            this.find = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // findNext
            // 
            this.findNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.findNext.Location = new System.Drawing.Point(322, 128);
            this.findNext.Name = "findNext";
            this.findNext.Size = new System.Drawing.Size(97, 23);
            this.findNext.TabIndex = 6;
            this.findNext.Text = "Найти далее";
            this.findNext.UseVisualStyleBackColor = true;
            this.findNext.Click += new System.EventHandler(this.findNext_Click);
            // 
            // findPrevious
            // 
            this.findPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.findPrevious.Location = new System.Drawing.Point(187, 128);
            this.findPrevious.Name = "findPrevious";
            this.findPrevious.Size = new System.Drawing.Size(118, 23);
            this.findPrevious.TabIndex = 5;
            this.findPrevious.Text = "Найти предыдущий";
            this.findPrevious.UseVisualStyleBackColor = true;
            this.findPrevious.Click += new System.EventHandler(this.findPrevious_Click);
            // 
            // highlightAll
            // 
            this.highlightAll.AutoSize = true;
            this.highlightAll.Location = new System.Drawing.Point(91, 95);
            this.highlightAll.Name = "highlightAll";
            this.highlightAll.Size = new System.Drawing.Size(160, 17);
            this.highlightAll.TabIndex = 4;
            this.highlightAll.Text = "Выделить все совпадения";
            this.highlightAll.UseVisualStyleBackColor = true;
            this.highlightAll.CheckedChanged += new System.EventHandler(this.HighlightAll_CheckedChanged);
            // 
            // matchWholeWord
            // 
            this.matchWholeWord.AutoSize = true;
            this.matchWholeWord.Location = new System.Drawing.Point(91, 72);
            this.matchWholeWord.Name = "matchWholeWord";
            this.matchWholeWord.Size = new System.Drawing.Size(104, 17);
            this.matchWholeWord.TabIndex = 3;
            this.matchWholeWord.Text = "Слово целиком";
            this.matchWholeWord.UseVisualStyleBackColor = true;
            this.matchWholeWord.CheckedChanged += new System.EventHandler(this.MatchWholeWord_CheckedChanged);
            // 
            // matchCase
            // 
            this.matchCase.AutoSize = true;
            this.matchCase.Location = new System.Drawing.Point(91, 49);
            this.matchCase.Name = "matchCase";
            this.matchCase.Size = new System.Drawing.Size(124, 17);
            this.matchCase.TabIndex = 2;
            this.matchCase.Text = "Учитывать регистр";
            this.matchCase.UseVisualStyleBackColor = true;
            this.matchCase.CheckedChanged += new System.EventHandler(this.MatchCase_CheckedChanged);
            // 
            // find
            // 
            this.find.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.find.Location = new System.Drawing.Point(91, 23);
            this.find.Name = "find";
            this.find.Size = new System.Drawing.Size(297, 20);
            this.find.TabIndex = 1;
            this.find.TextChanged += new System.EventHandler(this.find_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Найти:";
            // 
            // SearchForm
            // 
            this.AcceptButton = this.findNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 174);
            this.Controls.Add(this.findNext);
            this.Controls.Add(this.findPrevious);
            this.Controls.Add(this.highlightAll);
            this.Controls.Add(this.matchWholeWord);
            this.Controls.Add(this.matchCase);
            this.Controls.Add(this.find);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchForm";
            this.Text = "Поисковая форма";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button findNext;
        private System.Windows.Forms.Button findPrevious;
        private System.Windows.Forms.CheckBox highlightAll;
        private System.Windows.Forms.CheckBox matchWholeWord;
        private System.Windows.Forms.CheckBox matchCase;
        private System.Windows.Forms.TextBox find;
        private System.Windows.Forms.Label label1;
    }
}