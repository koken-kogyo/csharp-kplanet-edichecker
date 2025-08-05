namespace K_PLANET_EDIChecker
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.listBoxFolders = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.識別 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._94 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._80 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._76 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._81 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._84 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._87 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._89 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._91 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._98 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(13, 13);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(402, 27);
            this.pathTextBox.TabIndex = 0;
            this.pathTextBox.Text = "\\\\kmtsvr\\共有SVEM02\\『クボタデータ』";
            this.pathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pathTextBox_KeyDown);
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(181, 44);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.Size = new System.Drawing.Size(616, 164);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView1_DrawColumnHeader);
            this.listView1.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView1_DrawItem);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 17, 0);
            this.statusStrip1.Size = new System.Drawing.Size(805, 32);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(162, 26);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(423, 9);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(92, 31);
            this.btnSelectFolder.TabIndex = 4;
            this.btnSelectFolder.Text = "参照 (F2)";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // listBoxFolders
            // 
            this.listBoxFolders.FormattingEnabled = true;
            this.listBoxFolders.ItemHeight = 20;
            this.listBoxFolders.Location = new System.Drawing.Point(11, 44);
            this.listBoxFolders.Name = "listBoxFolders";
            this.listBoxFolders.Size = new System.Drawing.Size(163, 144);
            this.listBoxFolders.TabIndex = 5;
            this.listBoxFolders.SelectedIndexChanged += new System.EventHandler(this.listBoxFolders_SelectedIndexChanged);
            this.listBoxFolders.DoubleClick += new System.EventHandler(this.listBoxFolders_DoubleClick);
            this.listBoxFolders.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listBoxFolders_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.識別,
            this._21,
            this._23,
            this._26,
            this._28,
            this._29,
            this._94,
            this._80,
            this._76,
            this._81,
            this._84,
            this._87,
            this._89,
            this._91,
            this._98});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.Location = new System.Drawing.Point(11, 216);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(786, 176);
            this.dataGridView1.TabIndex = 6;
            // 
            // 識別
            // 
            this.識別.HeaderText = "識別";
            this.識別.MinimumWidth = 6;
            this.識別.Name = "識別";
            this.識別.ReadOnly = true;
            this.識別.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.識別.Width = 65;
            // 
            // _21
            // 
            dataGridViewCellStyle6.NullValue = null;
            this._21.DefaultCellStyle = dataGridViewCellStyle6;
            this._21.HeaderText = "21堺";
            this._21.MinimumWidth = 6;
            this._21.Name = "_21";
            this._21.ReadOnly = true;
            this._21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._21.ToolTipText = "堺";
            this._21.Width = 50;
            // 
            // _23
            // 
            dataGridViewCellStyle7.NullValue = null;
            this._23.DefaultCellStyle = dataGridViewCellStyle7;
            this._23.HeaderText = "23枚";
            this._23.MinimumWidth = 6;
            this._23.Name = "_23";
            this._23.ReadOnly = true;
            this._23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._23.ToolTipText = "枚方";
            this._23.Width = 50;
            // 
            // _26
            // 
            this._26.HeaderText = "26宇";
            this._26.MinimumWidth = 6;
            this._26.Name = "_26";
            this._26.ReadOnly = true;
            this._26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._26.ToolTipText = "宇都宮";
            this._26.Width = 50;
            // 
            // _28
            // 
            this._28.HeaderText = "28筑";
            this._28.MinimumWidth = 6;
            this._28.Name = "_28";
            this._28.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._28.ToolTipText = "筑波";
            this._28.Width = 50;
            // 
            // _29
            // 
            this._29.HeaderText = "29臨";
            this._29.MinimumWidth = 6;
            this._29.Name = "_29";
            this._29.ReadOnly = true;
            this._29.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._29.ToolTipText = "臨海";
            this._29.Width = 50;
            // 
            // _94
            // 
            this._94.HeaderText = "94精";
            this._94.MinimumWidth = 6;
            this._94.Name = "_94";
            this._94.ReadOnly = true;
            this._94.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._94.ToolTipText = "クボタ精機";
            this._94.Width = 50;
            // 
            // _80
            // 
            this._80.HeaderText = "80試";
            this._80.MinimumWidth = 6;
            this._80.Name = "_80";
            this._80.ReadOnly = true;
            this._80.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._80.Width = 50;
            // 
            // _76
            // 
            this._76.HeaderText = "76GP";
            this._76.MinimumWidth = 6;
            this._76.Name = "_76";
            this._76.ReadOnly = true;
            this._76.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._76.ToolTipText = "GPM";
            this._76.Width = 50;
            // 
            // _81
            // 
            this._81.HeaderText = "81SK";
            this._81.MinimumWidth = 6;
            this._81.Name = "_81";
            this._81.ReadOnly = true;
            this._81.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._81.ToolTipText = "SKT";
            this._81.Width = 50;
            // 
            // _84
            // 
            this._84.HeaderText = "84KC";
            this._84.MinimumWidth = 6;
            this._84.Name = "_84";
            this._84.ReadOnly = true;
            this._84.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._84.ToolTipText = "KCW";
            this._84.Width = 50;
            // 
            // _87
            // 
            this._87.HeaderText = "87KE";
            this._87.MinimumWidth = 6;
            this._87.Name = "_87";
            this._87.ReadOnly = true;
            this._87.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._87.ToolTipText = "KET";
            this._87.Width = 50;
            // 
            // _89
            // 
            this._89.HeaderText = "89KE";
            this._89.MinimumWidth = 6;
            this._89.Name = "_89";
            this._89.ReadOnly = true;
            this._89.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._89.ToolTipText = "KEW";
            this._89.Width = 50;
            // 
            // _91
            // 
            this._91.HeaderText = "91KM";
            this._91.MinimumWidth = 6;
            this._91.Name = "_91";
            this._91.ReadOnly = true;
            this._91.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._91.ToolTipText = "KMT";
            this._91.Width = 50;
            // 
            // _98
            // 
            this._98.HeaderText = "98KA";
            this._98.MinimumWidth = 6;
            this._98.Name = "_98";
            this._98.ReadOnly = true;
            this._98.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._98.ToolTipText = "KAMS";
            this._98.Width = 50;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox1.Location = new System.Drawing.Point(721, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(93, 19);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "識別全て";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox2.Location = new System.Drawing.Point(631, 18);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(109, 19);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "全てのﾌｧｲﾙ";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox3.Location = new System.Drawing.Point(541, 18);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(109, 19);
            this.checkBox3.TabIndex = 10;
            this.checkBox3.Text = "EM件数確認";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("ＭＳ ゴシック", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(10, 186);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "上のフォルダに移動";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 421);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.listBoxFolders);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.pathTextBox);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "K-PLANET EDI 件数チェッカー";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.ListBox listBoxFolders;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 識別;
        private System.Windows.Forms.DataGridViewTextBoxColumn _21;
        private System.Windows.Forms.DataGridViewTextBoxColumn _23;
        private System.Windows.Forms.DataGridViewTextBoxColumn _26;
        private System.Windows.Forms.DataGridViewTextBoxColumn _28;
        private System.Windows.Forms.DataGridViewTextBoxColumn _29;
        private System.Windows.Forms.DataGridViewTextBoxColumn _94;
        private System.Windows.Forms.DataGridViewTextBoxColumn _80;
        private System.Windows.Forms.DataGridViewTextBoxColumn _76;
        private System.Windows.Forms.DataGridViewTextBoxColumn _81;
        private System.Windows.Forms.DataGridViewTextBoxColumn _84;
        private System.Windows.Forms.DataGridViewTextBoxColumn _87;
        private System.Windows.Forms.DataGridViewTextBoxColumn _89;
        private System.Windows.Forms.DataGridViewTextBoxColumn _91;
        private System.Windows.Forms.DataGridViewTextBoxColumn _98;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button1;
    }
}

