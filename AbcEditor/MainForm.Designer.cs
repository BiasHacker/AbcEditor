namespace AbcEditor
{
    partial class MainForm
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
            this.CefSharpPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // CefSharpPanel
            // 
            this.CefSharpPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CefSharpPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CefSharpPanel.Location = new System.Drawing.Point(0, 0);
            this.CefSharpPanel.Name = "CefSharpPanel";
            this.CefSharpPanel.Size = new System.Drawing.Size(1147, 728);
            this.CefSharpPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 728);
            this.Controls.Add(this.CefSharpPanel);
            this.Name = "MainForm";
            this.Text = "TypeShoot";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel CefSharpPanel;
    }
}

