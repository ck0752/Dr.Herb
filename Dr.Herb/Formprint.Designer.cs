namespace Dr.Herb
{
    partial class Formprint
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GVPowder2 = new System.Windows.Forms.DataGridView();
            this.GVPowderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVPowderWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVPowderUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVPowderRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVLinquor2 = new System.Windows.Forms.DataGridView();
            this.GVLinquorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVLinquorWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVLinquorUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVLinquorRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVHerb2 = new System.Windows.Forms.DataGridView();
            this.ColHerb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.button3 = new System.Windows.Forms.Button();
            this.btnDGVprinter = new System.Windows.Forms.Button();
            this.btnDGVpreivew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GVPowder2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVLinquor2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVHerb2)).BeginInit();
            this.SuspendLayout();
            // 
            // GVPowder2
            // 
            this.GVPowder2.AllowUserToAddRows = false;
            this.GVPowder2.AllowUserToOrderColumns = true;
            this.GVPowder2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GVPowder2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GVPowder2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVPowder2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GVPowderName,
            this.GVPowderWeight,
            this.GVPowderUnit,
            this.GVPowderRate});
            this.GVPowder2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GVPowder2.Location = new System.Drawing.Point(28, 41);
            this.GVPowder2.Name = "GVPowder2";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVPowder2.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GVPowder2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GVPowder2.RowTemplate.Height = 24;
            this.GVPowder2.Size = new System.Drawing.Size(302, 430);
            this.GVPowder2.TabIndex = 14;
            // 
            // GVPowderName
            // 
            this.GVPowderName.HeaderText = "藥粉";
            this.GVPowderName.Name = "GVPowderName";
            // 
            // GVPowderWeight
            // 
            this.GVPowderWeight.HeaderText = "重量";
            this.GVPowderWeight.Name = "GVPowderWeight";
            // 
            // GVPowderUnit
            // 
            this.GVPowderUnit.HeaderText = "單位";
            this.GVPowderUnit.Name = "GVPowderUnit";
            // 
            // GVPowderRate
            // 
            this.GVPowderRate.HeaderText = "比例";
            this.GVPowderRate.Name = "GVPowderRate";
            // 
            // GVLinquor2
            // 
            this.GVLinquor2.AllowUserToAddRows = false;
            this.GVLinquor2.AllowUserToOrderColumns = true;
            this.GVLinquor2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GVLinquor2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GVLinquor2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVLinquor2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GVLinquorName,
            this.GVLinquorWeight,
            this.GVLinquorUnit,
            this.GVLinquorRate});
            this.GVLinquor2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GVLinquor2.Location = new System.Drawing.Point(336, 41);
            this.GVLinquor2.Name = "GVLinquor2";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVLinquor2.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GVLinquor2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GVLinquor2.RowTemplate.Height = 24;
            this.GVLinquor2.Size = new System.Drawing.Size(297, 430);
            this.GVLinquor2.TabIndex = 15;
            // 
            // GVLinquorName
            // 
            this.GVLinquorName.HeaderText = "藥酒";
            this.GVLinquorName.Name = "GVLinquorName";
            // 
            // GVLinquorWeight
            // 
            this.GVLinquorWeight.HeaderText = "重量";
            this.GVLinquorWeight.Name = "GVLinquorWeight";
            // 
            // GVLinquorUnit
            // 
            this.GVLinquorUnit.HeaderText = "單位";
            this.GVLinquorUnit.Name = "GVLinquorUnit";
            // 
            // GVLinquorRate
            // 
            this.GVLinquorRate.HeaderText = "比例";
            this.GVLinquorRate.Name = "GVLinquorRate";
            // 
            // GVHerb2
            // 
            this.GVHerb2.AllowUserToAddRows = false;
            this.GVHerb2.AllowUserToOrderColumns = true;
            this.GVHerb2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GVHerb2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GVHerb2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVHerb2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColHerb,
            this.ColWeight,
            this.ColUnit});
            this.GVHerb2.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GVHerb2.Location = new System.Drawing.Point(639, 41);
            this.GVHerb2.Name = "GVHerb2";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVHerb2.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GVHerb2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GVHerb2.RowTemplate.Height = 24;
            this.GVHerb2.Size = new System.Drawing.Size(261, 430);
            this.GVHerb2.TabIndex = 16;
            // 
            // ColHerb
            // 
            this.ColHerb.HeaderText = "藥草";
            this.ColHerb.Name = "ColHerb";
            // 
            // ColWeight
            // 
            this.ColWeight.HeaderText = "重量";
            this.ColWeight.Name = "ColWeight";
            // 
            // ColUnit
            // 
            this.ColUnit.HeaderText = "單位";
            this.ColUnit.Name = "ColUnit";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "CopyGV";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(109, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "預覽列印";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // printDocument2
            // 
            this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(190, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "列印";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDGVprinter
            // 
            this.btnDGVprinter.Location = new System.Drawing.Point(432, 11);
            this.btnDGVprinter.Name = "btnDGVprinter";
            this.btnDGVprinter.Size = new System.Drawing.Size(75, 23);
            this.btnDGVprinter.TabIndex = 20;
            this.btnDGVprinter.Text = "DGVprinter";
            this.btnDGVprinter.UseVisualStyleBackColor = true;
            this.btnDGVprinter.Click += new System.EventHandler(this.btnDGVprinter_Click);
            // 
            // btnDGVpreivew
            // 
            this.btnDGVpreivew.Location = new System.Drawing.Point(548, 11);
            this.btnDGVpreivew.Name = "btnDGVpreivew";
            this.btnDGVpreivew.Size = new System.Drawing.Size(75, 23);
            this.btnDGVpreivew.TabIndex = 21;
            this.btnDGVpreivew.Text = "DGVpreivew";
            this.btnDGVpreivew.UseVisualStyleBackColor = true;
            this.btnDGVpreivew.Click += new System.EventHandler(this.btnDGVpreivew_Click);
            // 
            // Formprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 537);
            this.Controls.Add(this.btnDGVpreivew);
            this.Controls.Add(this.btnDGVprinter);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.GVHerb2);
            this.Controls.Add(this.GVLinquor2);
            this.Controls.Add(this.GVPowder2);
            this.Name = "Formprint";
            this.Text = "FormPrint";
            this.Load += new System.EventHandler(this.Formprint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GVPowder2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVLinquor2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVHerb2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GVPowder2;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVPowderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVPowderWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVPowderUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVPowderRate;
        private System.Windows.Forms.DataGridView GVLinquor2;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVLinquorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVLinquorWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVLinquorUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVLinquorRate;
        private System.Windows.Forms.DataGridView GVHerb2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColHerb;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUnit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Drawing.Printing.PrintDocument printDocument2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnDGVprinter;
        private System.Windows.Forms.Button btnDGVpreivew;
    }
}