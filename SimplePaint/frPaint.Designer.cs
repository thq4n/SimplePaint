namespace SimplePaint
{
    partial class frPaint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frPaint));
            this.boxFill = new System.Windows.Forms.ComboBox();
            this.boxDash = new System.Windows.Forms.ComboBox();
            this.boxShape = new System.Windows.Forms.ComboBox();
            this.butUngroup = new System.Windows.Forms.Button();
            this.butClear = new System.Windows.Forms.Button();
            this.butDelete = new System.Windows.Forms.Button();
            this.butGroup = new System.Windows.Forms.Button();
            this.barWidth = new System.Windows.Forms.TrackBar();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.butLColor = new System.Windows.Forms.Button();
            this.butSelect = new System.Windows.Forms.Button();
            this.butFColor = new System.Windows.Forms.Button();
            this.lblFColor = new System.Windows.Forms.Label();
            this.pnlMain = new SimplePaint.MyPanel();
            ((System.ComponentModel.ISupportInitialize)(this.barWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // boxFill
            // 
            this.boxFill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxFill.Items.AddRange(new object[] {
            "Fill Shape",
            "No Fill Shape"});
            this.boxFill.Location = new System.Drawing.Point(13, 12);
            this.boxFill.Name = "boxFill";
            this.boxFill.Size = new System.Drawing.Size(152, 24);
            this.boxFill.Sorted = true;
            this.boxFill.TabIndex = 3;
            this.boxFill.SelectedIndexChanged += new System.EventHandler(this.boxFill_SelectedIndexChanged);
            // 
            // boxDash
            // 
            this.boxDash.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxDash.Items.AddRange(new object[] {
            "Dash",
            "Dash Dot",
            "Dash Dot Dot",
            "Dot",
            "Solid"});
            this.boxDash.Location = new System.Drawing.Point(13, 72);
            this.boxDash.Name = "boxDash";
            this.boxDash.Size = new System.Drawing.Size(152, 24);
            this.boxDash.Sorted = true;
            this.boxDash.TabIndex = 4;
            this.boxDash.SelectedIndexChanged += new System.EventHandler(this.boxDash_SelectedIndexChanged);
            // 
            // boxShape
            // 
            this.boxShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxShape.Items.AddRange(new object[] {
            "Bezier",
            "Circle",
            "Curve",
            "Elipse",
            "Line",
            "Polygon",
            "Rectangle",
            "Square"});
            this.boxShape.Location = new System.Drawing.Point(13, 42);
            this.boxShape.Name = "boxShape";
            this.boxShape.Size = new System.Drawing.Size(152, 24);
            this.boxShape.Sorted = true;
            this.boxShape.TabIndex = 5;
            this.boxShape.SelectedIndexChanged += new System.EventHandler(this.boxShape_SelectedIndexChanged);
            // 
            // butUngroup
            // 
            this.butUngroup.Enabled = false;
            this.butUngroup.ForeColor = System.Drawing.Color.Black;
            this.butUngroup.Location = new System.Drawing.Point(16, 299);
            this.butUngroup.Name = "butUngroup";
            this.butUngroup.Size = new System.Drawing.Size(72, 38);
            this.butUngroup.TabIndex = 12;
            this.butUngroup.Text = "Ungroup";
            this.butUngroup.UseVisualStyleBackColor = true;
            this.butUngroup.Click += new System.EventHandler(this.butUngroup_Click);
            // 
            // butClear
            // 
            this.butClear.ForeColor = System.Drawing.Color.Black;
            this.butClear.Location = new System.Drawing.Point(17, 343);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(72, 38);
            this.butClear.TabIndex = 13;
            this.butClear.Text = "Clear";
            this.butClear.UseVisualStyleBackColor = true;
            this.butClear.Click += new System.EventHandler(this.butClear_Click);
            // 
            // butDelete
            // 
            this.butDelete.Enabled = false;
            this.butDelete.ForeColor = System.Drawing.Color.Black;
            this.butDelete.Location = new System.Drawing.Point(17, 387);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(72, 38);
            this.butDelete.TabIndex = 14;
            this.butDelete.Text = "Delete";
            this.butDelete.UseVisualStyleBackColor = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butGroup
            // 
            this.butGroup.Enabled = false;
            this.butGroup.ForeColor = System.Drawing.Color.Black;
            this.butGroup.Location = new System.Drawing.Point(17, 255);
            this.butGroup.Name = "butGroup";
            this.butGroup.Size = new System.Drawing.Size(72, 38);
            this.butGroup.TabIndex = 15;
            this.butGroup.Text = "Group";
            this.butGroup.UseVisualStyleBackColor = true;
            this.butGroup.Click += new System.EventHandler(this.butGroup_Click);
            // 
            // barWidth
            // 
            this.barWidth.Location = new System.Drawing.Point(2, 213);
            this.barWidth.Minimum = 1;
            this.barWidth.Name = "barWidth";
            this.barWidth.Size = new System.Drawing.Size(172, 56);
            this.barWidth.TabIndex = 11;
            this.barWidth.Value = 1;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidth.ForeColor = System.Drawing.Color.White;
            this.lblWidth.Location = new System.Drawing.Point(12, 190);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(99, 20);
            this.lblWidth.TabIndex = 9;
            this.lblWidth.Text = "Line Width";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.ForeColor = System.Drawing.Color.White;
            this.lblColor.Location = new System.Drawing.Point(13, 114);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(96, 20);
            this.lblColor.TabIndex = 10;
            this.lblColor.Text = "Line Color";
            // 
            // butLColor
            // 
            this.butLColor.BackColor = System.Drawing.Color.Black;
            this.butLColor.ForeColor = System.Drawing.Color.White;
            this.butLColor.Location = new System.Drawing.Point(124, 107);
            this.butLColor.Name = "butLColor";
            this.butLColor.Size = new System.Drawing.Size(41, 34);
            this.butLColor.TabIndex = 8;
            this.butLColor.UseVisualStyleBackColor = false;
            this.butLColor.Click += new System.EventHandler(this.butLColor_Click);
            // 
            // butSelect
            // 
            this.butSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.butSelect.ForeColor = System.Drawing.Color.Black;
            this.butSelect.Location = new System.Drawing.Point(95, 255);
            this.butSelect.Name = "butSelect";
            this.butSelect.Size = new System.Drawing.Size(70, 170);
            this.butSelect.TabIndex = 15;
            this.butSelect.Text = "Select";
            this.butSelect.UseVisualStyleBackColor = false;
            this.butSelect.Click += new System.EventHandler(this.butSelect_Click);
            // 
            // butFColor
            // 
            this.butFColor.BackColor = System.Drawing.Color.Black;
            this.butFColor.Enabled = false;
            this.butFColor.ForeColor = System.Drawing.Color.White;
            this.butFColor.Location = new System.Drawing.Point(124, 147);
            this.butFColor.Name = "butFColor";
            this.butFColor.Size = new System.Drawing.Size(41, 34);
            this.butFColor.TabIndex = 8;
            this.butFColor.UseVisualStyleBackColor = false;
            this.butFColor.Click += new System.EventHandler(this.butFColor_Click);
            // 
            // lblFColor
            // 
            this.lblFColor.AutoSize = true;
            this.lblFColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFColor.ForeColor = System.Drawing.Color.White;
            this.lblFColor.Location = new System.Drawing.Point(13, 154);
            this.lblFColor.Name = "lblFColor";
            this.lblFColor.Size = new System.Drawing.Size(86, 20);
            this.lblFColor.TabIndex = 10;
            this.lblFColor.Text = "Fill Color";
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pnlMain.Location = new System.Drawing.Point(181, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(692, 436);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseDown);
            this.pnlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseMove);
            this.pnlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseUp);
            // 
            // frPaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(873, 437);
            this.Controls.Add(this.butSelect);
            this.Controls.Add(this.butUngroup);
            this.Controls.Add(this.butClear);
            this.Controls.Add(this.butDelete);
            this.Controls.Add(this.butGroup);
            this.Controls.Add(this.barWidth);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.lblFColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.butFColor);
            this.Controls.Add(this.butLColor);
            this.Controls.Add(this.boxFill);
            this.Controls.Add(this.boxDash);
            this.Controls.Add(this.boxShape);
            this.Controls.Add(this.pnlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frPaint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Paint";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.barWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyPanel pnlMain;
        private System.Windows.Forms.ComboBox boxFill;
        private System.Windows.Forms.ComboBox boxDash;
        private System.Windows.Forms.ComboBox boxShape;
        private System.Windows.Forms.Button butUngroup;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.Button butDelete;
        private System.Windows.Forms.Button butGroup;
        private System.Windows.Forms.TrackBar barWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button butLColor;
        private System.Windows.Forms.Button butSelect;
        private System.Windows.Forms.Button butFColor;
        private System.Windows.Forms.Label lblFColor;
    }
}

