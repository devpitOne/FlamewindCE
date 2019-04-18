namespace ConversationEditorGui
{
    partial class TokenSelect
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewTokens = new System.Windows.Forms.ListView();
            this.Token = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewTokens
            // 
            this.listViewTokens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Token});
            this.listViewTokens.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listViewTokens.Dock = System.Windows.Forms.DockStyle.Left;
            this.listViewTokens.FullRowSelect = true;
            this.listViewTokens.HideSelection = false;
            this.listViewTokens.HoverSelection = true;
            this.listViewTokens.LabelWrap = false;
            this.listViewTokens.Location = new System.Drawing.Point(0, 0);
            this.listViewTokens.MultiSelect = false;
            this.listViewTokens.Name = "listViewTokens";
            this.listViewTokens.Size = new System.Drawing.Size(123, 626);
            this.listViewTokens.TabIndex = 0;
            this.listViewTokens.UseCompatibleStateImageBehavior = false;
            this.listViewTokens.View = System.Windows.Forms.View.Details;
            this.listViewTokens.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Item_DblClick);
            // 
            // Token
            // 
            this.Token.Text = "Token";
            this.Token.Width = 80;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(133, 557);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.Ok_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(133, 591);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // TokenSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 626);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.listViewTokens);
            this.Name = "TokenSelect";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewTokens;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ColumnHeader Token;
    }
}
