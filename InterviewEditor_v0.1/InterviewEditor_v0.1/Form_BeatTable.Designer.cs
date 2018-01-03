namespace InterviewEditor_v0._1
{
    partial class Form_BeatTable
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_beatTable = new System.Windows.Forms.DataGridView();
            this.column_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_mainText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_backgroundImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_characterImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_narrativeNextID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_choiceText1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_choiceNextID1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_choiceText2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_choiceNextID2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_choiceText3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_choiceNextID3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_beatTable)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_beatTable
            // 
            this.dataGridView_beatTable.AllowUserToAddRows = false;
            this.dataGridView_beatTable.AllowUserToDeleteRows = false;
            this.dataGridView_beatTable.AllowUserToOrderColumns = true;
            this.dataGridView_beatTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_beatTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_beatTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_beatTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column_ID,
            this.column_Type,
            this.column_mainText,
            this.column_backgroundImage,
            this.column_characterImage,
            this.column_narrativeNextID,
            this.column_choiceText1,
            this.column_choiceNextID1,
            this.column_choiceText2,
            this.column_choiceNextID2,
            this.column_choiceText3,
            this.column_choiceNextID3});
            this.dataGridView_beatTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_beatTable.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_beatTable.MinimumSize = new System.Drawing.Size(250, 250);
            this.dataGridView_beatTable.Name = "dataGridView_beatTable";
            this.dataGridView_beatTable.ReadOnly = true;
            this.dataGridView_beatTable.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView_beatTable.RowHeadersVisible = false;
            this.dataGridView_beatTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_beatTable.ShowCellErrors = false;
            this.dataGridView_beatTable.ShowEditingIcon = false;
            this.dataGridView_beatTable.ShowRowErrors = false;
            this.dataGridView_beatTable.Size = new System.Drawing.Size(770, 327);
            this.dataGridView_beatTable.TabIndex = 0;
            // 
            // column_ID
            // 
            this.column_ID.HeaderText = "ID";
            this.column_ID.Name = "column_ID";
            this.column_ID.ReadOnly = true;
            this.column_ID.Width = 50;
            // 
            // column_Type
            // 
            this.column_Type.HeaderText = "Type";
            this.column_Type.Name = "column_Type";
            this.column_Type.ReadOnly = true;
            this.column_Type.Width = 65;
            // 
            // column_mainText
            // 
            this.column_mainText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.column_mainText.HeaderText = "Main Text";
            this.column_mainText.Name = "column_mainText";
            this.column_mainText.ReadOnly = true;
            // 
            // column_backgroundImage
            // 
            this.column_backgroundImage.HeaderText = "Background Image";
            this.column_backgroundImage.Name = "column_backgroundImage";
            this.column_backgroundImage.ReadOnly = true;
            this.column_backgroundImage.Width = 75;
            // 
            // column_characterImage
            // 
            this.column_characterImage.HeaderText = "Character Image";
            this.column_characterImage.Name = "column_characterImage";
            this.column_characterImage.ReadOnly = true;
            this.column_characterImage.Width = 68;
            // 
            // column_narrativeNextID
            // 
            this.column_narrativeNextID.HeaderText = "(Narrative) Next ID";
            this.column_narrativeNextID.Name = "column_narrativeNextID";
            this.column_narrativeNextID.ReadOnly = true;
            this.column_narrativeNextID.Width = 60;
            // 
            // column_choiceText1
            // 
            this.column_choiceText1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.column_choiceText1.HeaderText = "(Choice) Text 1";
            this.column_choiceText1.Name = "column_choiceText1";
            this.column_choiceText1.ReadOnly = true;
            // 
            // column_choiceNextID1
            // 
            this.column_choiceNextID1.HeaderText = "(Choice) Next ID 1";
            this.column_choiceNextID1.Name = "column_choiceNextID1";
            this.column_choiceNextID1.ReadOnly = true;
            this.column_choiceNextID1.Width = 50;
            // 
            // column_choiceText2
            // 
            this.column_choiceText2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.column_choiceText2.HeaderText = "(Choice) Text 2";
            this.column_choiceText2.Name = "column_choiceText2";
            this.column_choiceText2.ReadOnly = true;
            // 
            // column_choiceNextID2
            // 
            this.column_choiceNextID2.HeaderText = "(Choice) Next ID 2";
            this.column_choiceNextID2.Name = "column_choiceNextID2";
            this.column_choiceNextID2.ReadOnly = true;
            this.column_choiceNextID2.Width = 50;
            // 
            // column_choiceText3
            // 
            this.column_choiceText3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.column_choiceText3.HeaderText = "(Choice) Text 3";
            this.column_choiceText3.Name = "column_choiceText3";
            this.column_choiceText3.ReadOnly = true;
            // 
            // column_choiceNextID3
            // 
            this.column_choiceNextID3.HeaderText = "(Choice) Next ID 3";
            this.column_choiceNextID3.Name = "column_choiceNextID3";
            this.column_choiceNextID3.ReadOnly = true;
            this.column_choiceNextID3.Width = 50;
            // 
            // Form_BeatTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 327);
            this.Controls.Add(this.dataGridView_beatTable);
            this.MinimumSize = new System.Drawing.Size(365, 365);
            this.Name = "Form_BeatTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interview Editor - Beat Table";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_beatTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_beatTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_mainText;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_backgroundImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_characterImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_narrativeNextID;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_choiceText1;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_choiceNextID1;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_choiceText2;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_choiceNextID2;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_choiceText3;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_choiceNextID3;
    }
}