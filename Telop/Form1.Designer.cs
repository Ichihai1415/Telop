namespace Telop
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
            this.components = new System.ComponentModel.Container();
            this.Title = new System.Windows.Forms.Label();
            this.MainText = new System.Windows.Forms.Label();
            this.Xml = new System.Windows.Forms.Timer(this.components);
            this.Time = new System.Windows.Forms.Timer(this.components);
            this.LabelMove = new System.Windows.Forms.Timer(this.components);
            this.TelopHide = new System.Windows.Forms.Label();
            this.NowTime = new System.Windows.Forms.Label();
            this.TimeCheck = new System.Windows.Forms.Timer(this.components);
            this.UserTextForced = new System.Windows.Forms.Timer(this.components);
            this.TextChange = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(200)))));
            this.Title.Font = new System.Drawing.Font("Koruri Regular", 18F);
            this.Title.Location = new System.Drawing.Point(0, 0);
            this.Title.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(1280, 32);
            this.Title.TabIndex = 0;
            // 
            // MainText
            // 
            this.MainText.AutoSize = true;
            this.MainText.BackColor = System.Drawing.Color.Blue;
            this.MainText.Font = new System.Drawing.Font("Koruri Regular", 20F);
            this.MainText.Location = new System.Drawing.Point(0, 32);
            this.MainText.Margin = new System.Windows.Forms.Padding(0);
            this.MainText.Name = "MainText";
            this.MainText.Size = new System.Drawing.Size(0, 48);
            this.MainText.TabIndex = 1;
            // 
            // Xml
            // 
            this.Xml.Enabled = true;
            this.Xml.Interval = 1000;
            this.Xml.Tick += new System.EventHandler(this.Xml_Tick);
            // 
            // Time
            // 
            this.Time.Interval = 1000;
            this.Time.Tick += new System.EventHandler(this.Time_Tick);
            // 
            // LabelMove
            // 
            this.LabelMove.Enabled = true;
            this.LabelMove.Interval = 50;
            this.LabelMove.Tick += new System.EventHandler(this.LabelMove_Tick);
            // 
            // TelopHide
            // 
            this.TelopHide.BackColor = System.Drawing.Color.Black;
            this.TelopHide.Location = new System.Drawing.Point(0, 0);
            this.TelopHide.Name = "TelopHide";
            this.TelopHide.Size = new System.Drawing.Size(1280, 72);
            this.TelopHide.TabIndex = 2;
            // 
            // NowTime
            // 
            this.NowTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(150)))));
            this.NowTime.Font = new System.Drawing.Font("Koruri Regular", 18F);
            this.NowTime.Location = new System.Drawing.Point(1030, 0);
            this.NowTime.Name = "NowTime";
            this.NowTime.Size = new System.Drawing.Size(250, 32);
            this.NowTime.TabIndex = 3;
            this.NowTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimeCheck
            // 
            this.TimeCheck.Enabled = true;
            this.TimeCheck.Interval = 1;
            this.TimeCheck.Tick += new System.EventHandler(this.TimeCheck_Tick);
            // 
            // UserTextForced
            // 
            this.UserTextForced.Enabled = true;
            this.UserTextForced.Interval = 10;
            this.UserTextForced.Tick += new System.EventHandler(this.UserTextForced_Tick);
            // 
            // TextChange
            // 
            this.TextChange.Enabled = true;
            this.TextChange.Interval = 9999;
            this.TextChange.Tick += new System.EventHandler(this.TextChange_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(29F, 70F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(1278, 64);
            this.Controls.Add(this.TelopHide);
            this.Controls.Add(this.NowTime);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.MainText);
            this.Font = new System.Drawing.Font("Koruri Regular", 30F);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(13);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1296, 111);
            this.MinimumSize = new System.Drawing.Size(1296, 111);
            this.Name = "Form1";
            this.Text = "Telop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label MainText;
        private System.Windows.Forms.Timer Xml;
        private System.Windows.Forms.Timer Time;
        private System.Windows.Forms.Timer LabelMove;
        private System.Windows.Forms.Label TelopHide;
        private System.Windows.Forms.Label NowTime;
        private System.Windows.Forms.Timer TimeCheck;
        private System.Windows.Forms.Timer UserTextForced;
        private System.Windows.Forms.Timer TextChange;
    }
}

