namespace OpenDental{
	partial class FormWiki {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWiki));
			this.imageListMain = new System.Windows.Forms.ImageList(this.components);
			this.ToolBarMain = new OpenDental.UI.ODToolBar();
			this.webBrowserWiki = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// imageListMain
			// 
			this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
			this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListMain.Images.SetKeyName(0,"Left.gif");
			this.imageListMain.Images.SetKeyName(1,"Manage22.gif");
			this.imageListMain.Images.SetKeyName(2,"home.gif");
			this.imageListMain.Images.SetKeyName(3,"editPencil.gif");
			this.imageListMain.Images.SetKeyName(4,"rename.gif");
			this.imageListMain.Images.SetKeyName(5,"deleteX.gif");
			this.imageListMain.Images.SetKeyName(6,"history.gif");
			this.imageListMain.Images.SetKeyName(7,"incoming.gif");
			this.imageListMain.Images.SetKeyName(8,"Add.gif");
			this.imageListMain.Images.SetKeyName(9,"allpages.gif");
			this.imageListMain.Images.SetKeyName(10,"search.gif");
			// 
			// ToolBarMain
			// 
			this.ToolBarMain.Dock = System.Windows.Forms.DockStyle.Top;
			this.ToolBarMain.ImageList = this.imageListMain;
			this.ToolBarMain.Location = new System.Drawing.Point(0,0);
			this.ToolBarMain.Name = "ToolBarMain";
			this.ToolBarMain.Size = new System.Drawing.Size(944,25);
			this.ToolBarMain.TabIndex = 72;
			this.ToolBarMain.ButtonClick += new OpenDental.UI.ODToolBarButtonClickEventHandler(this.ToolBarMain_ButtonClick);
			// 
			// webBrowserWiki
			// 
			this.webBrowserWiki.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowserWiki.Location = new System.Drawing.Point(0,31);
			this.webBrowserWiki.MinimumSize = new System.Drawing.Size(20,20);
			this.webBrowserWiki.Name = "webBrowserWiki";
			this.webBrowserWiki.Size = new System.Drawing.Size(944,674);
			this.webBrowserWiki.TabIndex = 0;
			this.webBrowserWiki.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowserWiki_Navigating);
			// 
			// FormWiki
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(944,704);
			this.Controls.Add(this.ToolBarMain);
			this.Controls.Add(this.webBrowserWiki);
			this.Name = "FormWiki";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Wiki";
			this.Load += new System.EventHandler(this.FormWiki_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser webBrowserWiki;
		private UI.ODToolBar ToolBarMain;
		private System.Windows.Forms.ImageList imageListMain;

	}
}