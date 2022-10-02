namespace Telop
{
    partial class Main
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
            this.LabelMove = new System.Windows.Forms.Timer(this.components);
            this.TelopHide = new System.Windows.Forms.Label();
            this.NowTime = new System.Windows.Forms.Label();
            this.TimeChangeSocketCheck = new System.Windows.Forms.Timer(this.components);
            this.TextChangeTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(200)))));
            this.Title.Font = new System.Drawing.Font("Koruri Regular", 19F);
            this.Title.Location = new System.Drawing.Point(0, 0);
            this.Title.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(147, 44);
            this.Title.TabIndex = 0;
            this.Title.Text = "おしらせ";
            // 
            // MainText
            // 
            this.MainText.AutoSize = true;
            this.MainText.BackColor = System.Drawing.Color.Blue;
            this.MainText.Font = new System.Drawing.Font("Koruri Regular", 19F);
            this.MainText.Location = new System.Drawing.Point(0, 0);
            this.MainText.Margin = new System.Windows.Forms.Padding(0);
            this.MainText.Name = "MainText";
            this.MainText.Size = new System.Drawing.Size(0, 44);
            this.MainText.TabIndex = 1;
            // 
            // Xml
            // 
            this.Xml.Enabled = true;
            this.Xml.Interval = 1000;
            this.Xml.Tick += new System.EventHandler(this.Xml_Tick);
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
            this.TelopHide.Size = new System.Drawing.Size(1280, 50);
            this.TelopHide.TabIndex = 2;
            // 
            // NowTime
            // 
            this.NowTime.AutoSize = true;
            this.NowTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(150)))));
            this.NowTime.Font = new System.Drawing.Font("Koruri Regular", 19F);
            this.NowTime.Location = new System.Drawing.Point(0, -62);
            this.NowTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NowTime.Name = "NowTime";
            this.NowTime.Size = new System.Drawing.Size(321, 44);
            this.NowTime.TabIndex = 3;
            this.NowTime.Text = "0000/00/00 00:00:00";
            this.NowTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimeChangeSocketCheck
            // 
            this.TimeChangeSocketCheck.Enabled = true;
            this.TimeChangeSocketCheck.Interval = 500;
            this.TimeChangeSocketCheck.Tick += new System.EventHandler(this.TimeChangeSocketCheck_Tick);
            // 
            // TextChangeTimer
            // 
            this.TextChangeTimer.Enabled = true;
            this.TextChangeTimer.Interval = 1000;
            this.TextChangeTimer.Tick += new System.EventHandler(this.TextChangeTimer_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 44F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(1280, 36);
            this.Controls.Add(this.TelopHide);
            this.Controls.Add(this.NowTime);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.MainText);
            this.Font = new System.Drawing.Font("Koruri Regular", 19F);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(8);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Telop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label MainText;
        private System.Windows.Forms.Timer Xml;
        private System.Windows.Forms.Timer LabelMove;
        private System.Windows.Forms.Label TelopHide;
        private System.Windows.Forms.Label NowTime;
        private System.Windows.Forms.Timer TimeChangeSocketCheck;
        private System.Windows.Forms.Timer TextChangeTimer;
    }
}

