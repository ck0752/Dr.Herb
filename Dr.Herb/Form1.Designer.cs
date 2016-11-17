namespace Dr.Herb
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtherb = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtweight = new System.Windows.Forms.TextBox();
            this.ddlweight = new System.Windows.Forms.ComboBox();
            this.btnComfirm = new System.Windows.Forms.Button();
            this.lvHerb = new System.Windows.Forms.ListView();
            this.GVHerb = new System.Windows.Forms.DataGridView();
            this.ColHerb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrePrint = new System.Windows.Forms.Button();
            this.PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GVHerb)).BeginInit();
            this.SuspendLayout();
            // 
            // txtherb
            // 
            this.txtherb.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtherb.Location = new System.Drawing.Point(79, 12);
            this.txtherb.Name = "txtherb";
            this.txtherb.Size = new System.Drawing.Size(174, 33);
            this.txtherb.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Location = new System.Drawing.Point(25, 72);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(228, 388);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label1.Location = new System.Drawing.Point(21, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(52, 24);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "草藥:";
            // 
            // txtweight
            // 
            this.txtweight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtweight.Location = new System.Drawing.Point(269, 12);
            this.txtweight.Name = "txtweight";
            this.txtweight.Size = new System.Drawing.Size(36, 33);
            this.txtweight.TabIndex = 3;
            // 
            // ddlweight
            // 
            this.ddlweight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ddlweight.FormattingEnabled = true;
            this.ddlweight.Items.AddRange(new object[] {
            "錢",
            "兩",
            "片"});
            this.ddlweight.Location = new System.Drawing.Point(311, 12);
            this.ddlweight.Name = "ddlweight";
            this.ddlweight.Size = new System.Drawing.Size(49, 32);
            this.ddlweight.TabIndex = 4;
            // 
            // btnComfirm
            // 
            this.btnComfirm.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnComfirm.Location = new System.Drawing.Point(383, 11);
            this.btnComfirm.Name = "btnComfirm";
            this.btnComfirm.Size = new System.Drawing.Size(70, 33);
            this.btnComfirm.TabIndex = 5;
            this.btnComfirm.Text = "加入";
            this.btnComfirm.UseVisualStyleBackColor = true;
            this.btnComfirm.Click += new System.EventHandler(this.button1_Click);
            // 
            // lvHerb
            // 
            this.lvHerb.BackColor = System.Drawing.SystemColors.Window;
            this.lvHerb.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lvHerb.Location = new System.Drawing.Point(269, 72);
            this.lvHerb.Name = "lvHerb";
            this.lvHerb.Size = new System.Drawing.Size(256, 388);
            this.lvHerb.TabIndex = 6;
            this.lvHerb.UseCompatibleStateImageBehavior = false;
            // 
            // GVHerb
            // 
            this.GVHerb.AllowUserToOrderColumns = true;
            this.GVHerb.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GVHerb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVHerb.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColHerb,
            this.ColWeight,
            this.ColUnit});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GVHerb.DefaultCellStyle = dataGridViewCellStyle5;
            this.GVHerb.Location = new System.Drawing.Point(543, 72);
            this.GVHerb.Name = "GVHerb";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVHerb.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.GVHerb.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GVHerb.RowTemplate.Height = 24;
            this.GVHerb.Size = new System.Drawing.Size(265, 388);
            this.GVHerb.TabIndex = 7;
            // 
            // ColHerb
            // 
            this.ColHerb.HeaderText = "草藥";
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
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPrint.Location = new System.Drawing.Point(733, 13);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "列印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrePrint
            // 
            this.btnPrePrint.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPrePrint.Location = new System.Drawing.Point(623, 13);
            this.btnPrePrint.Name = "btnPrePrint";
            this.btnPrePrint.Size = new System.Drawing.Size(104, 30);
            this.btnPrePrint.TabIndex = 9;
            this.btnPrePrint.Text = "預覽列印";
            this.btnPrePrint.UseVisualStyleBackColor = true;
            this.btnPrePrint.Click += new System.EventHandler(this.btnPrePrint_Click);
            // 
            // PrintDocument1
            // 
            this.PrintDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.PrintDocument1_BeginPrint);
            this.PrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "請由下方選單選取 或 直接輸入";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 472);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPrePrint);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.GVHerb);
            this.Controls.Add(this.lvHerb);
            this.Controls.Add(this.btnComfirm);
            this.Controls.Add(this.ddlweight);
            this.Controls.Add(this.txtweight);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtherb);
            this.Name = "Form1";
            this.Text = "Dr. Herb";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GVHerb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtherb;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox txtweight;
        private System.Windows.Forms.ComboBox ddlweight;
        private System.Windows.Forms.Button btnComfirm;
        private System.Windows.Forms.ListView lvHerb;
        private System.Windows.Forms.DataGridView GVHerb;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColHerb;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUnit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrePrint;
        private System.Drawing.Printing.PrintDocument PrintDocument1;
        private System.Windows.Forms.Label label2;
    }
}

