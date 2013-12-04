namespace OpenDental{
	partial class FormWebMailMessageEdit {
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
			this.components = new System.ComponentModel.Container();
			this.textBoxTo = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxFrom = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxSubject = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.labelNotification = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemSetup = new System.Windows.Forms.MenuItem();
			this.butPreview = new OpenDental.UI.Button();
			this.textBoxBody = new OpenDental.ODtextBox();
			this.butSend = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textBoxTo
			// 
			this.textBoxTo.Location = new System.Drawing.Point(96, 25);
			this.textBoxTo.Name = "textBoxTo";
			this.textBoxTo.ReadOnly = true;
			this.textBoxTo.Size = new System.Drawing.Size(328, 20);
			this.textBoxTo.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(3, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 14);
			this.label1.TabIndex = 11;
			this.label1.Text = "To:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxFrom
			// 
			this.textBoxFrom.Location = new System.Drawing.Point(96, 51);
			this.textBoxFrom.Name = "textBoxFrom";
			this.textBoxFrom.ReadOnly = true;
			this.textBoxFrom.Size = new System.Drawing.Size(328, 20);
			this.textBoxFrom.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(3, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92, 14);
			this.label3.TabIndex = 13;
			this.label3.Text = "From:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 14);
			this.label2.TabIndex = 13;
			this.label2.Text = "Message:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxSubject
			// 
			this.textBoxSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSubject.Location = new System.Drawing.Point(96, 84);
			this.textBoxSubject.Name = "textBoxSubject";
			this.textBoxSubject.Size = new System.Drawing.Size(619, 20);
			this.textBoxSubject.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(3, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(92, 14);
			this.label4.TabIndex = 16;
			this.label4.Text = "Subject:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelNotification
			// 
			this.labelNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.labelNotification.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelNotification.Location = new System.Drawing.Point(12, 475);
			this.labelNotification.Name = "labelNotification";
			this.labelNotification.Size = new System.Drawing.Size(541, 14);
			this.labelNotification.TabIndex = 17;
			this.labelNotification.Text = "Warning: Patient email is not setup properly. No notification email will be sent " +
    "to this patient.";
			this.labelNotification.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSetup});
			// 
			// menuItemSetup
			// 
			this.menuItemSetup.Index = 0;
			this.menuItemSetup.Text = "Setup";
			this.menuItemSetup.Click += new System.EventHandler(this.menuItemSetup_Click);
			// 
			// butPreview
			// 
			this.butPreview.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPreview.Autosize = true;
			this.butPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPreview.CornerRadius = 4F;
			this.butPreview.Image = global::OpenDental.Properties.Resources.butPreview;
			this.butPreview.Location = new System.Drawing.Point(21, 426);
			this.butPreview.Name = "butPreview";
			this.butPreview.Size = new System.Drawing.Size(69, 24);
			this.butPreview.TabIndex = 18;
			this.butPreview.Text = "&Cancel";
			this.butPreview.Click += new System.EventHandler(this.butPreview_Click);
			// 
			// textBoxBody
			// 
			this.textBoxBody.AcceptsTab = true;
			this.textBoxBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxBody.DetectUrls = false;
			this.textBoxBody.Location = new System.Drawing.Point(96, 112);
			this.textBoxBody.Name = "textBoxBody";
			this.textBoxBody.QuickPasteType = OpenDentBusiness.QuickPasteType.Email;
			this.textBoxBody.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.textBoxBody.Size = new System.Drawing.Size(619, 338);
			this.textBoxBody.TabIndex = 3;
			this.textBoxBody.Text = "";
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Location = new System.Drawing.Point(559, 470);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75, 24);
			this.butSend.TabIndex = 4;
			this.butSend.Text = "&Send";
			this.butSend.Click += new System.EventHandler(this.butSend_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(640, 470);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormWebMailMessageEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(744, 506);
			this.Controls.Add(this.butPreview);
			this.Controls.Add(this.labelNotification);
			this.Controls.Add(this.textBoxSubject);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxBody);
			this.Controls.Add(this.textBoxFrom);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxTo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.butCancel);
			this.Menu = this.mainMenu1;
			this.Name = "FormWebMailMessageEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Web Mail Message Edit";
			this.Load += new System.EventHandler(this.FormWebMailMessageEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butSend;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textBoxTo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxFrom;
		private System.Windows.Forms.Label label3;
		private ODtextBox textBoxBody;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxSubject;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelNotification;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItemSetup;
		private UI.Button butPreview;
	}
}