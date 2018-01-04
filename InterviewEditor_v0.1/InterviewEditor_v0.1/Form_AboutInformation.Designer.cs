namespace InterviewEditor_v0._1
{
    partial class Form_AboutInformation
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
            this.label_title_Version = new System.Windows.Forms.Label();
            this.label_title_Creator = new System.Windows.Forms.Label();
            this.label_title_Git = new System.Windows.Forms.Label();
            this.label_title_Wiki = new System.Windows.Forms.Label();
            this.label_link_Git = new System.Windows.Forms.LinkLabel();
            this.label_link_Wiki = new System.Windows.Forms.LinkLabel();
            this.button_Wiki_Copy = new System.Windows.Forms.Button();
            this.button_Wiki_Open = new System.Windows.Forms.Button();
            this.button_Git_Open = new System.Windows.Forms.Button();
            this.button_Git_Copy = new System.Windows.Forms.Button();
            this.label_value_Version = new System.Windows.Forms.Label();
            this.label_value_CreatorName = new System.Windows.Forms.Label();
            this.button_Creator_Open = new System.Windows.Forms.Button();
            this.button_Creator_Email = new System.Windows.Forms.Button();
            this.label_link_CreatorWebsite = new System.Windows.Forms.LinkLabel();
            this.label_link_CreatorEmail = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label_title_Version
            // 
            this.label_title_Version.AutoSize = true;
            this.label_title_Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title_Version.Location = new System.Drawing.Point(8, 8);
            this.label_title_Version.Name = "label_title_Version";
            this.label_title_Version.Size = new System.Drawing.Size(53, 13);
            this.label_title_Version.TabIndex = 0;
            this.label_title_Version.Text = "Version:";
            // 
            // label_title_Creator
            // 
            this.label_title_Creator.AutoSize = true;
            this.label_title_Creator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title_Creator.Location = new System.Drawing.Point(8, 40);
            this.label_title_Creator.Name = "label_title_Creator";
            this.label_title_Creator.Size = new System.Drawing.Size(52, 13);
            this.label_title_Creator.TabIndex = 1;
            this.label_title_Creator.Text = "Creator:";
            // 
            // label_title_Git
            // 
            this.label_title_Git.AutoSize = true;
            this.label_title_Git.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title_Git.Location = new System.Drawing.Point(12, 114);
            this.label_title_Git.Name = "label_title_Git";
            this.label_title_Git.Size = new System.Drawing.Size(27, 13);
            this.label_title_Git.TabIndex = 2;
            this.label_title_Git.Text = "Git:";
            // 
            // label_title_Wiki
            // 
            this.label_title_Wiki.AutoSize = true;
            this.label_title_Wiki.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title_Wiki.Location = new System.Drawing.Point(8, 172);
            this.label_title_Wiki.Name = "label_title_Wiki";
            this.label_title_Wiki.Size = new System.Drawing.Size(36, 13);
            this.label_title_Wiki.TabIndex = 3;
            this.label_title_Wiki.Text = "Wiki:";
            // 
            // label_link_Git
            // 
            this.label_link_Git.AutoSize = true;
            this.label_link_Git.Location = new System.Drawing.Point(44, 114);
            this.label_link_Git.Name = "label_link_Git";
            this.label_link_Git.Size = new System.Drawing.Size(233, 13);
            this.label_link_Git.TabIndex = 4;
            this.label_link_Git.TabStop = true;
            this.label_link_Git.Text = "https://github.com/aliciayeargin/InterviewEditor";
            this.label_link_Git.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label_link_Git_LinkClicked);
            // 
            // label_link_Wiki
            // 
            this.label_link_Wiki.AutoSize = true;
            this.label_link_Wiki.Location = new System.Drawing.Point(44, 172);
            this.label_link_Wiki.Name = "label_link_Wiki";
            this.label_link_Wiki.Size = new System.Drawing.Size(256, 13);
            this.label_link_Wiki.TabIndex = 5;
            this.label_link_Wiki.TabStop = true;
            this.label_link_Wiki.Text = "https://github.com/aliciayeargin/InterviewEditor/wiki";
            this.label_link_Wiki.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label_link_Wiki_LinkClicked);
            // 
            // button_Wiki_Copy
            // 
            this.button_Wiki_Copy.Location = new System.Drawing.Point(44, 192);
            this.button_Wiki_Copy.Name = "button_Wiki_Copy";
            this.button_Wiki_Copy.Size = new System.Drawing.Size(48, 23);
            this.button_Wiki_Copy.TabIndex = 6;
            this.button_Wiki_Copy.Text = "Copy";
            this.button_Wiki_Copy.UseVisualStyleBackColor = true;
            this.button_Wiki_Copy.Click += new System.EventHandler(this.button_Wiki_Copy_Click);
            // 
            // button_Wiki_Open
            // 
            this.button_Wiki_Open.Location = new System.Drawing.Point(100, 192);
            this.button_Wiki_Open.Name = "button_Wiki_Open";
            this.button_Wiki_Open.Size = new System.Drawing.Size(48, 23);
            this.button_Wiki_Open.TabIndex = 7;
            this.button_Wiki_Open.Text = "Open";
            this.button_Wiki_Open.UseVisualStyleBackColor = true;
            this.button_Wiki_Open.Click += new System.EventHandler(this.button_Wiki_Open_Click);
            // 
            // button_Git_Open
            // 
            this.button_Git_Open.Location = new System.Drawing.Point(100, 132);
            this.button_Git_Open.Name = "button_Git_Open";
            this.button_Git_Open.Size = new System.Drawing.Size(48, 23);
            this.button_Git_Open.TabIndex = 9;
            this.button_Git_Open.Text = "Open";
            this.button_Git_Open.UseVisualStyleBackColor = true;
            this.button_Git_Open.Click += new System.EventHandler(this.button_Git_Open_Click);
            // 
            // button_Git_Copy
            // 
            this.button_Git_Copy.Location = new System.Drawing.Point(44, 132);
            this.button_Git_Copy.Name = "button_Git_Copy";
            this.button_Git_Copy.Size = new System.Drawing.Size(48, 23);
            this.button_Git_Copy.TabIndex = 8;
            this.button_Git_Copy.Text = "Copy";
            this.button_Git_Copy.UseVisualStyleBackColor = true;
            this.button_Git_Copy.Click += new System.EventHandler(this.button_Git_Copy_Click);
            // 
            // label_value_Version
            // 
            this.label_value_Version.AutoSize = true;
            this.label_value_Version.Location = new System.Drawing.Point(60, 8);
            this.label_value_Version.Name = "label_value_Version";
            this.label_value_Version.Size = new System.Drawing.Size(22, 13);
            this.label_value_Version.TabIndex = 10;
            this.label_value_Version.Text = "0.1";
            // 
            // label_value_CreatorName
            // 
            this.label_value_CreatorName.AutoSize = true;
            this.label_value_CreatorName.Location = new System.Drawing.Point(60, 40);
            this.label_value_CreatorName.Name = "label_value_CreatorName";
            this.label_value_CreatorName.Size = new System.Drawing.Size(71, 13);
            this.label_value_CreatorName.TabIndex = 11;
            this.label_value_CreatorName.Text = "Alicia Yeargin";
            // 
            // button_Creator_Open
            // 
            this.button_Creator_Open.Location = new System.Drawing.Point(252, 40);
            this.button_Creator_Open.Name = "button_Creator_Open";
            this.button_Creator_Open.Size = new System.Drawing.Size(48, 23);
            this.button_Creator_Open.TabIndex = 13;
            this.button_Creator_Open.Text = "Open";
            this.button_Creator_Open.UseVisualStyleBackColor = true;
            this.button_Creator_Open.Click += new System.EventHandler(this.button_Creator_Open_Click);
            // 
            // button_Creator_Email
            // 
            this.button_Creator_Email.Location = new System.Drawing.Point(252, 66);
            this.button_Creator_Email.Name = "button_Creator_Email";
            this.button_Creator_Email.Size = new System.Drawing.Size(48, 23);
            this.button_Creator_Email.TabIndex = 15;
            this.button_Creator_Email.Text = "Email";
            this.button_Creator_Email.UseVisualStyleBackColor = true;
            this.button_Creator_Email.Click += new System.EventHandler(this.button_Creator_Email_Click);
            // 
            // label_link_CreatorWebsite
            // 
            this.label_link_CreatorWebsite.AutoSize = true;
            this.label_link_CreatorWebsite.Location = new System.Drawing.Point(60, 58);
            this.label_link_CreatorWebsite.Name = "label_link_CreatorWebsite";
            this.label_link_CreatorWebsite.Size = new System.Drawing.Size(151, 13);
            this.label_link_CreatorWebsite.TabIndex = 16;
            this.label_link_CreatorWebsite.TabStop = true;
            this.label_link_CreatorWebsite.Text = "https://www.aliciayeargin.com";
            this.label_link_CreatorWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label_link_CreatorWebsite_LinkClicked);
            // 
            // label_link_CreatorEmail
            // 
            this.label_link_CreatorEmail.AutoSize = true;
            this.label_link_CreatorEmail.Location = new System.Drawing.Point(60, 76);
            this.label_link_CreatorEmail.Name = "label_link_CreatorEmail";
            this.label_link_CreatorEmail.Size = new System.Drawing.Size(123, 13);
            this.label_link_CreatorEmail.TabIndex = 17;
            this.label_link_CreatorEmail.TabStop = true;
            this.label_link_CreatorEmail.Text = "aliciayeargin@gmail.com";
            this.label_link_CreatorEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.label_link_CreatorEmail_LinkClicked);
            // 
            // Form_AboutInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 221);
            this.Controls.Add(this.label_link_CreatorEmail);
            this.Controls.Add(this.label_link_CreatorWebsite);
            this.Controls.Add(this.button_Creator_Email);
            this.Controls.Add(this.button_Creator_Open);
            this.Controls.Add(this.label_value_CreatorName);
            this.Controls.Add(this.label_value_Version);
            this.Controls.Add(this.button_Git_Open);
            this.Controls.Add(this.button_Git_Copy);
            this.Controls.Add(this.button_Wiki_Open);
            this.Controls.Add(this.button_Wiki_Copy);
            this.Controls.Add(this.label_link_Wiki);
            this.Controls.Add(this.label_link_Git);
            this.Controls.Add(this.label_title_Wiki);
            this.Controls.Add(this.label_title_Git);
            this.Controls.Add(this.label_title_Creator);
            this.Controls.Add(this.label_title_Version);
            this.Name = "Form_AboutInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interview Editor - Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title_Version;
        private System.Windows.Forms.Label label_title_Creator;
        private System.Windows.Forms.Label label_title_Git;
        private System.Windows.Forms.Label label_title_Wiki;
        private System.Windows.Forms.LinkLabel label_link_Git;
        private System.Windows.Forms.LinkLabel label_link_Wiki;
        private System.Windows.Forms.Button button_Wiki_Copy;
        private System.Windows.Forms.Button button_Wiki_Open;
        private System.Windows.Forms.Button button_Git_Open;
        private System.Windows.Forms.Button button_Git_Copy;
        private System.Windows.Forms.Label label_value_Version;
        private System.Windows.Forms.Label label_value_CreatorName;
        private System.Windows.Forms.Button button_Creator_Open;
        private System.Windows.Forms.Button button_Creator_Email;
        private System.Windows.Forms.LinkLabel label_link_CreatorWebsite;
        private System.Windows.Forms.LinkLabel label_link_CreatorEmail;
    }
}