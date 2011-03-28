namespace OpenDental{
	partial class FormAllergyDefEdit {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.butOK = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.labelAllergy = new System.Windows.Forms.Label();
			this.textAllergy = new System.Windows.Forms.TextBox();
			this.checkHidden = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(351,152);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Location = new System.Drawing.Point(15,152);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 2;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// labelAllergy
			// 
			this.labelAllergy.Location = new System.Drawing.Point(5,25);
			this.labelAllergy.Name = "labelAllergy";
			this.labelAllergy.Size = new System.Drawing.Size(85,20);
			this.labelAllergy.TabIndex = 6;
			this.labelAllergy.Text = "Allergy";
			this.labelAllergy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textAllergy
			// 
			this.textAllergy.Location = new System.Drawing.Point(96,25);
			this.textAllergy.Name = "textAllergy";
			this.textAllergy.Size = new System.Drawing.Size(276,20);
			this.textAllergy.TabIndex = 7;
			// 
			// checkHidden
			// 
			this.checkHidden.Location = new System.Drawing.Point(8,67);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkHidden.Size = new System.Drawing.Size(104,24);
			this.checkHidden.TabIndex = 8;
			this.checkHidden.Text = "Is Hidden";
			this.checkHidden.UseVisualStyleBackColor = true;
			// 
			// FormAllergyDefEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(438,188);
			this.Controls.Add(this.checkHidden);
			this.Controls.Add(this.textAllergy);
			this.Controls.Add(this.labelAllergy);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butDelete);
			this.Name = "FormAllergyDefEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Allergy Def Edit";
			this.Load += new System.EventHandler(this.FormAllergyEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label labelAllergy;
		private System.Windows.Forms.TextBox textAllergy;
		private System.Windows.Forms.CheckBox checkHidden;
	}
}