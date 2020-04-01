namespace MyDatabaser
{
    partial class Editor
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
            this.commonLabel = new System.Windows.Forms.Label();
            this.executeButton = new System.Windows.Forms.Button();
            this.queryTextBox = new System.Windows.Forms.TextBox();
            this.informRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // commonLabel
            // 
            this.commonLabel.AutoSize = true;
            this.commonLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commonLabel.Location = new System.Drawing.Point(26, 19);
            this.commonLabel.Name = "commonLabel";
            this.commonLabel.Size = new System.Drawing.Size(143, 25);
            this.commonLabel.TabIndex = 0;
            this.commonLabel.Text = "commonLabel";
            // 
            // executeButton
            // 
            this.executeButton.Location = new System.Drawing.Point(1328, 700);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(150, 97);
            this.executeButton.TabIndex = 3;
            this.executeButton.Text = "Execute";
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // queryTextBox
            // 
            this.queryTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.queryTextBox.Location = new System.Drawing.Point(30, 65);
            this.queryTextBox.Multiline = true;
            this.queryTextBox.Name = "queryTextBox";
            this.queryTextBox.Size = new System.Drawing.Size(1252, 257);
            this.queryTextBox.TabIndex = 4;
            // 
            // informRichTextBox
            // 
            this.informRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.informRichTextBox.Location = new System.Drawing.Point(30, 341);
            this.informRichTextBox.Name = "informRichTextBox";
            this.informRichTextBox.Size = new System.Drawing.Size(1252, 456);
            this.informRichTextBox.TabIndex = 5;
            this.informRichTextBox.Text = "";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1490, 809);
            this.Controls.Add(this.informRichTextBox);
            this.Controls.Add(this.queryTextBox);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.commonLabel);
            this.Name = "Editor";
            this.Text = "Editor";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label commonLabel;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.TextBox queryTextBox;
        private System.Windows.Forms.RichTextBox informRichTextBox;
    }
}