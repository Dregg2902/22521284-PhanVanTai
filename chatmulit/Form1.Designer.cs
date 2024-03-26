namespace chatmulit
{
    partial class Form1
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
            textBox1 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            richTextBox1 = new RichTextBox();
            listBox1 = new ListBox();
            button2 = new Button();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(56, 8);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(632, 27);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 7);
            label1.Name = "label1";
            label1.Size = new Size(28, 28);
            label1.TabIndex = 1;
            label1.Text = "IP";
            // 
            // button1
            // 
            button1.Location = new Point(694, 10);
            button1.Name = "button1";
            button1.Size = new Size(184, 29);
            button1.TabIndex = 2;
            button1.Text = "start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(56, 41);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(632, 272);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(694, 51);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(184, 264);
            listBox1.TabIndex = 4;
            // 
            // button2
            // 
            button2.Location = new Point(694, 350);
            button2.Name = "button2";
            button2.Size = new Size(184, 29);
            button2.TabIndex = 5;
            button2.Text = "send";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(56, 350);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(632, 27);
            textBox2.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(890, 450);
            Controls.Add(textBox2);
            Controls.Add(button2);
            Controls.Add(listBox1);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Button button1;
        private RichTextBox richTextBox1;
        private ListBox listBox1;
        private Button button2;
        private TextBox textBox2;
    }
}
