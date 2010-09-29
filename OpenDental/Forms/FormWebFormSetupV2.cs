using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {

	/// <summary>
	/// This Form is for the 'next' version of the Webforms.
	/// This Form is primarily used by the dental office to set various UI parameters of a webform: eg. border colors and heading text.
	/// </summary>
	public partial class FormWebFormSetupV2:Form {

		private string WebFormAddress="";

		public FormWebFormSetupV2() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWebFormSetupV2_Load(object sender,EventArgs e) {

			this.backgroundWorker1.RunWorkerAsync();
			

			#if DEBUG
				IgnoreCertificateErrors();// used with faulty certificates only while debugging.
			#endif
			//TestSheetUpload();


			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Sheet Def Name"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Web Form Address"),42);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Update Status"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),210);
			gridMain.Columns.Add(col);
			
			gridMain.Rows.Clear();

			ODGridRow row=new ODGridRow();
			long patNum = 3;
			row.Cells.Add("a");
			row.Cells.Add("a");
			row.Tag=patNum;
			row.Cells.Add("a");
			row.Cells.Add("a");
			row.Cells.Add("a");
			gridMain.Rows.Add(row);
			gridMain.EndUpdate();


		

		}

		private void butWebformBorderColor_Click(object sender,EventArgs e) {
			ShowColorDialog();
		}

		private void butChange_Click(object sender,EventArgs e) {
			ShowColorDialog();
		}

		private void ShowColorDialog(){
			colorDialog1.Color=butWebformBorderColor.BackColor;
			if(colorDialog1.ShowDialog()!=DialogResult.OK) {
				return;
			}
			butWebformBorderColor.BackColor=colorDialog1.Color;
		}

		/// <summary>
		///  This method is used only for testing with security certificates that has problems.
		/// </summary>
		private void IgnoreCertificateErrors() {
			///the line below will allow the code to continue by not throwing an exception.
			///It will accept the security certificate if there is a problem with the security certificate.

			System.Net.ServicePointManager.ServerCertificateValidationCallback+=
			delegate(object sender,System.Security.Cryptography.X509Certificates.X509Certificate certificate,
									System.Security.Cryptography.X509Certificates.X509Chain chain,
									System.Net.Security.SslPolicyErrors sslPolicyErrors) {
				///do stuff here and return true or false accordingly.
				///In this particular case it always returns true i.e accepts any certificate.
				/* sample code 
				if(sslPolicyErrors==System.Net.Security.SslPolicyErrors.None) return true;
				// the sample below allows expired certificates
				foreach(X509ChainStatus s in chain.ChainStatus) {
					// allows expired certificates
					if(string.Equals(s.Status.ToString(),"NotTimeValid",
						StringComparison.OrdinalIgnoreCase)) {
						return true;
					}						
				}*/
				return true;
			};
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {
			textBoxWebFormAddress.Text=WebFormAddress; //the textbox is set here because it will thow an error if put under _Dowork
		}

		private void backgroundWorker1_DoWork(object sender,DoWorkEventArgs e) {
			GetWebFormAddress();
		}

		/// <summary>Only called from worker thread.</summary>
		private void GetWebFormAddress() {
			try{
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				if(wh.CheckRegistrationKey(RegistrationKey)==false) {
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				WebFormAddress=wh.GetWebFormAddress(RegistrationKey);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		
		/// <summary>
		/// Ignore this method - this is for the 'next' version of the Webforms.
		/// Here sheetDef can be uploaded to the web form Open Dental
		/// </summary>
		private void TestSheetUpload() {
			try {
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				SheetDef sheetDef=SheetDefs.GetSheetDef(5);
				SheetDef sheetDef1=SheetDefs.GetSheetDef(8);
				List<SheetDef> sheetDefList = new List<SheetDef>();
				sheetDefList.Add(sheetDef);
				sheetDefList.Add(sheetDef1);
				/* for this line to compile one must modify the Reference.cs file in to the Web references folder. The SheetDef and related classes with namespaces of WebHostSync must be removed so that the SheetDef Class of OpenDentBusiness is used
	*/
				//List<String> WebFormAddressList=wh.UpdateSheetDef(RegistrationKey,sheetDefList.ToArray());

				//if(WebFormAddressList.Count ==0) {
					// message= WebFormAddresses.Count + " sheet defs have been updated"
				//}
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try {
				// update preferences on server
				string RegistrationKey=PrefC.GetString(PrefName.RegistrationKey);
				WebHostSynch.WebHostSynch wh=new WebHostSynch.WebHostSynch();
				wh.Url=PrefC.GetString(PrefName.WebHostSynchServerURL);
				if(wh.CheckRegistrationKey(RegistrationKey)==false) {
					Cursor=Cursors.Default;
					MsgBox.Show(this,"Registration key provided by the dental office is incorrect");
					return;
				}
				bool PrefSet=true;
				/*bool PrefSet= wh.SetPreferences(RegistrationKey,PrefC.GetColor(PrefName.WebFormsBorderColor).ToArgb(),PrefC.GetStringSilent(PrefName.WebFormsHeading1),PrefC.GetStringSilent(PrefName.WebFormsHeading2));*/
				if(PrefSet==false) {
				MsgBox.Show(this,"Preferences could not be set on the server");
				}
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}






	}
}