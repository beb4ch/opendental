﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormPatientPortal:Form {
		public Patient PatCur;
		///<summary>A copy of the original patient object, as it was when this form was first opened.</summary>
		private Patient PatOld;
		///<summary>Keeps track if the user printed that patient's information out.  If the password changed and the user didn't print, a reminder will show.</summary>
		private bool WasPrinted;

		public FormPatientPortal() {
			InitializeComponent();
		}

		private void FormPatientPortal_Load(object sender,EventArgs e) {
			PatOld=PatCur.Copy();
			textOnlineUsername.Text=PatCur.FName+PatCur.PatNum;
			textOnlinePassword.Text="";
			if(PatCur.OnlinePassword!="") {//if a password was already filled in
				butGiveAccess.Text="Remove Online Access";
				//We do not want to show the password hash that is stored in the database so we will leave the password box blank but editable.
				textOnlinePassword.ReadOnly=false;
			}
			textPatientPortalURL.Text=PrefC.GetString(PrefName.PatientPortalURL);
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormPatientPortalSetup formPPS=new FormPatientPortalSetup();
			formPPS.ShowDialog();
			if(formPPS.DialogResult==DialogResult.OK) {
				textPatientPortalURL.Text=PrefC.GetString(PrefName.PatientPortalURL);
			}
		}

		private void butGiveAccess_Click(object sender,EventArgs e) {
			if(butGiveAccess.Text=="Provide Online Access") {//When form open opens with a blank password
				Cursor=Cursors.WaitCursor;
				//1. Fill password.
				string passwordGenerated=GenerateRandomPassword(8,10);
				textOnlinePassword.Text=passwordGenerated;
				//2. Make the password editable in case they want to change it.
				textOnlinePassword.ReadOnly=false;
				//3. Save password to db.
				// We only save the hash of the generated password.
				string passwordHashed=Userods.EncryptPassword(passwordGenerated,false);
				PatCur.OnlinePassword=passwordHashed;
				Patients.Update(PatCur,PatOld);
				PatOld.OnlinePassword=passwordHashed;//Update PatOld in case the user changes password manually.
				//4. Insert EhrMeasureEvent
				EhrMeasureEvent newMeasureEvent = new EhrMeasureEvent();
				newMeasureEvent.DateTEvent=DateTime.Now;
				newMeasureEvent.EventType=EhrMeasureEventType.OnlineAccessProvided;
				newMeasureEvent.PatNum=PatCur.PatNum;
				newMeasureEvent.MoreInfo="";
				EhrMeasureEvents.Insert(newMeasureEvent);
				//5. Rename button
				butGiveAccess.Text="Remove Online Access";
				Cursor=Cursors.Default;
			}
			else {//remove access
				Cursor=Cursors.WaitCursor;
				//1. Clear password
				textOnlinePassword.Text="";
				//2. Make in uneditable
				textOnlinePassword.ReadOnly=true;
				//3. Save password to db
				PatCur.OnlinePassword=textOnlinePassword.Text;
				Patients.Update(PatCur,PatOld);
				PatOld.OnlinePassword=textOnlinePassword.Text;//Update PatOld in case the user changes password manually.
				//4. Rename button
				butGiveAccess.Text="Provide Online Access";
				Cursor=Cursors.Default;
			}
		}

		private void butOpen_Click(object sender,EventArgs e) {
			if(textPatientPortalURL.Text=="") {
				MessageBox.Show("Please use Setup to set the Online Access Link first.");
				return;
			}
			try {
				System.Diagnostics.Process.Start(textPatientPortalURL.Text);
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		
		private void butGenerate_Click(object sender,EventArgs e) {
			if(textOnlinePassword.ReadOnly) {
				MessageBox.Show("Please use the Provide Online Access button first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			string passwordGenerated=GenerateRandomPassword(8,10);
			textOnlinePassword.Text=passwordGenerated;
			// We only save the hash of the generated password.
			string passwordHashed=Userods.EncryptPassword(passwordGenerated,false);
			PatCur.OnlinePassword=passwordHashed;
			Patients.Update(PatCur,PatOld);
			PatOld.OnlinePassword=passwordHashed;//Update PatOld in case the user changes password manually.
			Cursor=Cursors.Default;
		}

		///<summary>Should generate a reasonably random password at least 8 char long and containing at least one uppercase, one lowercase, and one number.</summary>
		private static string GenerateRandomPassword(int minLength,int maxLength) {
			// chracters like o(letter 0), 0 (Zero),l (letter l),1(one) etc are avoided because they can be ambigious
			string PASSWORD_CHARS_LCASE="abcdefgijkmnopqrstwxyz";
			string PASSWORD_CHARS_UCASE="ABCDEFGHJKLMNPQRSTWXYZ";
			string PASSWORD_CHARS_NUMERIC="23456789";
			string PASSWORD_CHARS_SPECIAL="*$-?_=!{}/";//&, +, and % are specifically excluded because they are known to cause failure on the mobile version.
			// Make sure that input parameters are valid.
			if(minLength <=0 || maxLength <=0 || minLength > maxLength)
				return null;
			// Create a local array containing supported password characters
			// grouped by types. You can remove character groups from this
			// array, but doing so will weaken the password strength.
			char[][] charGroups=new char[][]{
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
            PASSWORD_CHARS_SPECIAL.ToCharArray()};
			// Use this array to track the number of unused characters in each
			// character group.
			int[] charsLeftInGroup=new int[charGroups.Length];
			// Initially, all characters in each group are not used.
			for(int i=0;i<charsLeftInGroup.Length;i++) {
				charsLeftInGroup[i]=charGroups[i].Length;
			}
			// Use this array to track (iterate through) unused character groups.
			int[] leftGroupsOrder=new int[charGroups.Length];
			// Initially, all character groups are not used.
			for(int i=0;i<leftGroupsOrder.Length;i++) {
				leftGroupsOrder[i]=i;
			}
			// Because we cannot use the default randomizer, which is based on the
			// current time (it will produce the same "random" number within a
			// second), we will use a random number generator to seed the
			// randomizer.
			// Use a 4-byte array to fill it with random bytes and convert it then
			// to an integer value.
			byte[] randomBytes=new byte[4];
			// Generate 4 random bytes.
			RNGCryptoServiceProvider rng=new RNGCryptoServiceProvider();
			rng.GetBytes(randomBytes);
			// Convert 4 bytes into a 32-bit integer value.
			int seed=(randomBytes[0] & 0x7f) << 24 |
				randomBytes[1]         << 16 |
				randomBytes[2]         <<  8 |
				randomBytes[3];
			// Now, this is real randomization.
			Random random=new Random(seed);
			// This array will hold password characters.
			char[] password=null;
			// Allocate appropriate memory for the password.
			if(minLength < maxLength) {
				password=new char[random.Next(minLength,maxLength+1)];
			}
			else {
				password=new char[minLength];
			}
			// Index of the next character to be added to password.
			int nextCharIdx;
			// Index of the next character group to be processed.
			int nextGroupIdx;
			// Index which will be used to track not processed character groups.
			int nextLeftGroupsOrderIdx;
			// Index of the last non-processed character in a group.
			int lastCharIdx;
			// Index of the last non-processed group.
			int lastLeftGroupsOrderIdx=leftGroupsOrder.Length - 1;
			// Generate password characters one at a time.
			for(int i=0;i<password.Length;i++) {
				// If only one character group remained unprocessed, process it;
				// otherwise, pick a random character group from the unprocessed
				// group list. To allow a special character to appear in the
				// first position, increment the second parameter of the Next
				// function call by one, i.e. lastLeftGroupsOrderIdx + 1.
				if(lastLeftGroupsOrderIdx==0) {
					nextLeftGroupsOrderIdx=0;
				}
				else {
					nextLeftGroupsOrderIdx=random.Next(0,lastLeftGroupsOrderIdx);
				}
				// Get the actual index of the character group, from which we will
				// pick the next character.
				nextGroupIdx=leftGroupsOrder[nextLeftGroupsOrderIdx];
				// Get the index of the last unprocessed characters in this group.
				lastCharIdx=charsLeftInGroup[nextGroupIdx] - 1;
				// If only one unprocessed character is left, pick it; otherwise,
				// get a random character from the unused character list.
				if(lastCharIdx==0) {
					nextCharIdx=0;
				}
				else {
					nextCharIdx=random.Next(0,lastCharIdx+1);
				}
				// Add this character to the password.
				password[i]=charGroups[nextGroupIdx][nextCharIdx];
				// If we processed the last character in this group, start over.
				if(lastCharIdx==0) {
					charsLeftInGroup[nextGroupIdx]=charGroups[nextGroupIdx].Length;
					// There are more unprocessed characters left.
				}
				else {
					// Swap processed character with the last unprocessed character
					// so that we don't pick it until we process all characters in
					// this group.
					if(lastCharIdx !=nextCharIdx) {
						char temp=charGroups[nextGroupIdx][lastCharIdx];
						charGroups[nextGroupIdx][lastCharIdx]=charGroups[nextGroupIdx][nextCharIdx];
						charGroups[nextGroupIdx][nextCharIdx]=temp;
					}
					// Decrement the number of unprocessed characters in
					// this group.
					charsLeftInGroup[nextGroupIdx]--;
				}
				// If we processed the last group, start all over.
				if(lastLeftGroupsOrderIdx==0) {
					lastLeftGroupsOrderIdx=leftGroupsOrder.Length - 1;
					// There are more unprocessed groups left.
				}
				else {
					// Swap processed group with the last unprocessed group
					// so that we don't pick it until we process all groups.
					if(lastLeftGroupsOrderIdx !=nextLeftGroupsOrderIdx) {
						int temp=leftGroupsOrder[lastLeftGroupsOrderIdx];
						leftGroupsOrder[lastLeftGroupsOrderIdx]=
                                leftGroupsOrder[nextLeftGroupsOrderIdx];
						leftGroupsOrder[nextLeftGroupsOrderIdx]=temp;
					}
					// Decrement the number of unprocessed groups.
					lastLeftGroupsOrderIdx--;
				}
			}
			// Convert password characters into a string and return the result.
			return new string(password);
		}

		private void butPrint_Click(object sender,EventArgs e) {
			if(textPatientPortalURL.Text=="") {
				MsgBox.Show(this,"Online Access Link required. Please use Setup to set the Online Access Link first.");
				return;
			}
			if(textOnlinePassword.Text==""){
				MessageBox.Show("Password required. Please generate a new password.");
				return;
			}
			if(!PasswordIsValid()) {//this gives a messagebox if invalid
				return;
			}
			WasPrinted=true;
			//Then, print the info that the patient will be given in order for them to log in online.
			PrintDocument pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			if(pd.DefaultPageSettings.PrintableArea.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			try {
				#if DEBUG
					FormRpPrintPreview pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd;
					pView.ShowDialog();
				#else
						if(PrinterL.SetPrinter(pd,PrintSituation.Default)) {
							pd.Print();
						}
				#endif
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
			//new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font font=new Font("Arial",10,FontStyle.Regular);
			int yPos=bounds.Top+100;
			int center=bounds.X+bounds.Width/2;
			text="Online Access";
			g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,font).Width/2,yPos);
			yPos+=50;
			text="Website: "+textPatientPortalURL.Text;
			g.DrawString(text,font,Brushes.Black,bounds.Left+100,yPos);
			yPos+=25;
			text="Username: "+textOnlineUsername.Text;
			g.DrawString(text,font,Brushes.Black,bounds.Left+100,yPos);
			yPos+=25;
			text="Password: "+textOnlinePassword.Text;
			g.DrawString(text,font,Brushes.Black,bounds.Left+100,yPos);
			g.Dispose();
			e.HasMorePages=false;
		}

		///<summary>Used when click OK, and also when the Synch or Print buttons are clicked.</summary>
		private bool PasswordIsValid() {
			if(textOnlinePassword.Text.Length<8) {
				MessageBox.Show(this,"Password must be at least 8 characters long.");
				return false;
			}
			if(!Regex.IsMatch(textOnlinePassword.Text,"[A-Z]+")) {
				MessageBox.Show(this,"Password must contain an uppercase letter.");
				return false;
			}
			if(!Regex.IsMatch(textOnlinePassword.Text,"[a-z]+")) {
				MessageBox.Show(this,"Password must contain an lowercase letter.");
				return false;
			}
			if(!Regex.IsMatch(textOnlinePassword.Text,"[0-9]+")) {
				MessageBox.Show(this,"Password must contain a number.");
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textOnlinePassword.Text!="") {
				if(!PasswordIsValid()) {
					return;
				}
				if(!WasPrinted && MsgBox.Show(this,MsgBoxButtons.YesNo,"Online Password changed but was not printed.  It will never be visible again.\r\nContinue anyway?")) {
					return;
				}
				PatCur.OnlinePassword=Userods.EncryptPassword(textOnlinePassword.Text,false);
				Patients.Update(PatCur,PatOld);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		

		

		

	

	}
}