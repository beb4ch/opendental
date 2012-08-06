using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	/// <summary></summary>
	public partial class FormHL7DefEdit:System.Windows.Forms.Form {
		public HL7Def HL7DefCur;

		///<summary></summary>
		public FormHL7DefEdit() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		private void FormHL7DefEdit_Load(object sender,EventArgs e) {
			FillGrid();
			for(int i=0;i<Enum.GetNames(typeof(ModeTxHL7)).Length;i++) {
				comboModeTx.Items.Add(Lan.g("enumModeTxHL7",Enum.GetName(typeof(ModeTxHL7),i).ToString()));
				if((int)HL7DefCur.ModeTx==i){
					comboModeTx.SelectedIndex=i;
				}
			}
			textDescription.Text=HL7DefCur.Description;
			checkInternal.Checked=HL7DefCur.IsInternal;
			checkEnabled.Checked=HL7DefCur.IsEnabled;
			textInternalType.Text=HL7DefCur.InternalType;
			textInternalTypeVersion.Text=HL7DefCur.InternalTypeVersion;
			textInPort.Text=HL7DefCur.IncomingPort;
			textInPath.Text=HL7DefCur.IncomingFolder;
			textOutPort.Text=HL7DefCur.OutgoingIpPort;
			textOutPath.Text=HL7DefCur.OutgoingFolder;
			textFieldSep.Text=HL7DefCur.FieldSeparator;
			textRepSep.Text=HL7DefCur.RepetitionSeparator;
			textCompSep.Text=HL7DefCur.ComponentSeparator;
			textSubcompSep.Text=HL7DefCur.SubcomponentSeparator;
			textEscChar.Text=HL7DefCur.EscapeCharacter;
			textNote.Text=HL7DefCur.Note;
			if(HL7DefCur.IsInternal) {
				if(!HL7DefCur.IsEnabled) {
					textDescription.ReadOnly=true;
					textInPath.ReadOnly=true;
					textOutPath.ReadOnly=true;
					textInPort.ReadOnly=true;
					textOutPort.ReadOnly=true;
					butBrowseIn.Enabled=false;
					butBrowseOut.Enabled=false;
					textFieldSep.ReadOnly=true;
					textRepSep.ReadOnly=true;
					textCompSep.ReadOnly=true;
					textSubcompSep.ReadOnly=true;
					textEscChar.ReadOnly=true;
				}
				butAdd.Enabled=false;
				butDelete.Enabled=false;
				labelDelete.Visible=true;
			}
		}

		private void FillGrid() {
			if(!HL7DefCur.IsInternal && !HL7DefCur.IsNew) {
				HL7DefCur.hl7DefMessages=HL7DefMessages.GetDeepForDef(HL7DefCur.HL7DefNum);
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Message"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Seg"),35);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			if(HL7DefCur!=null && HL7DefCur.hl7DefMessages!=null) {
				for(int i=0;i<HL7DefCur.hl7DefMessages.Count;i++) {
					ODGridRow row=new ODGridRow();
					row.Cells.Add(HL7DefCur.hl7DefMessages[i].MessageType.ToString()+"-"+HL7DefCur.hl7DefMessages[i].EventType.ToString()+", "+HL7DefCur.hl7DefMessages[i].InOrOut.ToString());
					row.Cells.Add("");
					row.Cells.Add(HL7DefCur.hl7DefMessages[i].Note);
					row.Tag=HL7DefCur.hl7DefMessages[i];
					gridMain.Rows.Add(row);
					if(HL7DefCur.hl7DefMessages[i].hl7DefSegments!=null) {
						for(int j=0;j<HL7DefCur.hl7DefMessages[i].hl7DefSegments.Count;j++) {
							row=new ODGridRow();
							row.Cells.Add("");
							row.Cells.Add(HL7DefCur.hl7DefMessages[i].hl7DefSegments[j].SegmentName.ToString());
							row.Cells.Add(HL7DefCur.hl7DefMessages[i].hl7DefSegments[j].Note);
							row.Tag=HL7DefCur.hl7DefMessages[i];
							gridMain.Rows.Add(row);
						}
					}
				}
			}
			gridMain.EndUpdate();
		}

		private void butBrowseIn_Click(object sender,EventArgs e) {
			FolderBrowserDialog dlg=new FolderBrowserDialog();
			dlg.SelectedPath=textInPath.Text;
			if(dlg.ShowDialog()==DialogResult.OK) {
				textInPath.Text=dlg.SelectedPath;
			}
		}

		private void butBrowseOut_Click(object sender,EventArgs e) {
			FolderBrowserDialog dlg=new FolderBrowserDialog();
			dlg.SelectedPath=textOutPath.Text;
			if(dlg.ShowDialog()==DialogResult.OK) {
				textOutPath.Text=dlg.SelectedPath;
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormHL7DefMessageEdit FormS=new FormHL7DefMessageEdit();
			FormS.HL7DefMesCur=(HL7DefMessage)gridMain.Rows[e.Row].Tag;
			FormS.IsHL7DefInternal=HL7DefCur.IsInternal;
			FormS.ShowDialog();
			FillGrid();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			for(int i=0;i<gridMain.Rows.Count;i++) {
				if(gridMain.Rows[i].Tag==gridMain.Rows[e.Row].Tag) {
					gridMain.Rows[i].ColorText=Color.Red;
				}
				else {
					gridMain.Rows[i].ColorText=Color.Black;
				}
			}
			gridMain.Invalidate();
		}

		private void checkEnabled_CheckedChanged(object sender,EventArgs e) {
			if(checkEnabled.Checked) {
				butBrowseIn.Enabled=true;
				butBrowseOut.Enabled=true;
				textInPath.ReadOnly=false;
				textInPort.ReadOnly=false;
				textOutPath.ReadOnly=false;
				textOutPort.ReadOnly=false;
				textDescription.ReadOnly=false;
				textFieldSep.ReadOnly=false;
				textRepSep.ReadOnly=false;
				textCompSep.ReadOnly=false;
				textSubcompSep.ReadOnly=false;
				textEscChar.ReadOnly=false;
				//textNote.ReadOnly=false;
			}
			else {
				butBrowseIn.Enabled=false;
				butBrowseOut.Enabled=false;
				textInPath.ReadOnly=true;
				textInPort.ReadOnly=true;
				textOutPath.ReadOnly=true;
				textOutPort.ReadOnly=true;
				textDescription.ReadOnly=true;
				textFieldSep.ReadOnly=true;
				textRepSep.ReadOnly=true;
				textCompSep.ReadOnly=true;
				textSubcompSep.ReadOnly=true;
				textEscChar.ReadOnly=true;
				//textNote.ReadOnly=true;
			}
		}

		private void comboModeTx_SelectedIndexChanged(object sender,System.EventArgs e) {
			if(comboModeTx.SelectedIndex==0) {
				textInPort.Visible=false;
				textOutPort.Visible=false;
				labelInPort.Visible=false;
				labelInPortEx.Visible=false;
				labelOutPort.Visible=false;
				labelOutPortEx.Visible=false;
				textInPath.Visible=true;
				textOutPath.Visible=true;
				labelInPath.Visible=true;
				butBrowseIn.Visible=true;
				labelOutPath.Visible=true;
				butBrowseOut.Visible=true;
				textInPort.TabStop=false;
				textOutPort.TabStop=false;
				butBrowseIn.TabStop=true;
				butBrowseOut.TabStop=true;
			}
			else if(comboModeTx.SelectedIndex==1) {
				comboModeTx.SelectedIndex=1;
				textInPort.Visible=true;
				textOutPort.Visible=true;
				labelInPort.Visible=true;
				labelInPortEx.Visible=true;
				labelOutPort.Visible=true;
				labelOutPortEx.Visible=true;
				textInPath.Visible=false;
				textOutPath.Visible=false;
				labelInPath.Visible=false;
				butBrowseIn.Visible=false;
				labelOutPath.Visible=false;
				butBrowseOut.Visible=false;
				textInPort.TabStop=true;
				textOutPort.TabStop=true;
				butBrowseIn.TabStop=false;
				butBrowseOut.TabStop=false;
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			//This button is only enabled if this is a custom def.
			if(MessageBox.Show(Lan.g(this,"Delete entire HL7Def?"),"",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
				return;
			}
			for(int m=0;m<HL7DefCur.hl7DefMessages.Count;m++) {
				for(int s=0;s<HL7DefCur.hl7DefMessages[m].hl7DefSegments.Count;s++) {
					for(int f=0;f<HL7DefCur.hl7DefMessages[m].hl7DefSegments[s].hl7DefFields.Count;f++) {
						HL7DefFields.Delete(HL7DefCur.hl7DefMessages[m].hl7DefSegments[s].hl7DefFields[f].HL7DefFieldNum);
					}
					HL7DefSegments.Delete(HL7DefCur.hl7DefMessages[m].hl7DefSegments[s].HL7DefSegmentNum);
				}
				HL7DefMessages.Delete(HL7DefCur.hl7DefMessages[m].HL7DefMessageNum);
			}
			HL7Defs.Delete(HL7DefCur.HL7DefNum);
			DataValid.SetInvalid(InvalidType.HL7Defs);
			DialogResult=DialogResult.OK;			
		}

		private void butAdd_Click(object sender,EventArgs e) {
			//This button is only enabled if this is a custom def.
			//if(HL7DefCur.IsNew) {//never happens
			//	HL7Defs.Insert(HL7DefCur);
			//	HL7DefCur.IsNew=false;
			//}
			FormHL7DefMessageEdit FormS=new FormHL7DefMessageEdit();
			FormS.HL7DefMesCur=new HL7DefMessage();
			FormS.HL7DefMesCur.HL7DefNum=HL7DefCur.HL7DefNum;
			FormS.HL7DefMesCur.IsNew=true;
			FormS.IsHL7DefInternal=false;
			FormS.ShowDialog();
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//validation
			if(checkEnabled.Checked) {
				if(comboModeTx.SelectedIndex==(int)ModeTxHL7.File) {
					if(textInPath.Text=="") {
						MessageBox.Show(Lan.g(this,"The path for Incoming Folder is empty."),"",MessageBoxButtons.OK);
						return;
					}
					if(!Directory.Exists(textInPath.Text)) {
						MessageBox.Show(Lan.g(this,"The path for Incoming Folder is invalid."),"",MessageBoxButtons.OK);
						return;
					}
					if(textOutPath.Text=="") {
						MessageBox.Show(Lan.g(this,"The path for Outgoing Folder is empty."),"",MessageBoxButtons.OK);
						return;
					}
					if(!Directory.Exists(textOutPath.Text)) {
						MessageBox.Show(Lan.g(this,"The path for Outgoing Folder is invalid."),"",MessageBoxButtons.OK);
						return;
					}
				}
				else {//TcpIp mode
					if(textInPort.Text=="") {
						MessageBox.Show(Lan.g(this,"The Incoming Port is empty."),"",MessageBoxButtons.OK);
						return;
					}
					if(textOutPort.Text=="") {
						MessageBox.Show(Lan.g(this,"The Outgoing Port is empty."),"",MessageBoxButtons.OK);
						return;
					}
				}
			}

			//save
			if(checkEnabled.Checked) {
				HL7DefCur.IsEnabled=true;
				HL7DefCur.IsInternal=checkInternal.Checked;
				HL7DefCur.InternalType=textInternalType.Text;
				HL7DefCur.InternalTypeVersion=textInternalTypeVersion.Text;
				HL7DefCur.Description=textDescription.Text;
				HL7DefCur.FieldSeparator=textFieldSep.Text;
				HL7DefCur.RepetitionSeparator=textRepSep.Text;
				HL7DefCur.ComponentSeparator=textCompSep.Text;
				HL7DefCur.SubcomponentSeparator=textSubcompSep.Text;
				HL7DefCur.EscapeCharacter=textEscChar.Text;
				HL7DefCur.Note=textNote.Text;
				HL7DefCur.ModeTx=(ModeTxHL7)comboModeTx.SelectedIndex;
				if(comboModeTx.SelectedIndex==(int)ModeTxHL7.File) {
					HL7DefCur.IncomingFolder=textInPath.Text;
					HL7DefCur.OutgoingFolder=textOutPath.Text;
					HL7DefCur.IncomingPort="";
					HL7DefCur.OutgoingIpPort="";
				}
				else {//TcpIp mode
					HL7DefCur.IncomingPort=textInPort.Text;
					HL7DefCur.OutgoingIpPort=textOutPort.Text;
					HL7DefCur.IncomingFolder="";
					HL7DefCur.OutgoingFolder="";
				}
				if(checkInternal.Checked){
					if(HL7Defs.GetInternalFromDb(HL7DefCur.InternalType)==null){ //it's not in the database.
						HL7Defs.Insert(HL7DefCur);//The user wants to enable this, so we will need to save this def to the db.
					}
					else {
						HL7Defs.Update(HL7DefCur);
					}
				}
				else {//all custom defs are already in the db.
					HL7Defs.Update(HL7DefCur);
				}
			}
			else {//IsEnabled check box is not checked
				if(HL7DefCur.IsEnabled) {//If def was enabled but user wants to disable
					if(HL7DefCur.IsInternal) {
						if(MessageBox.Show(Lan.g(this,"Disable HL7Def? Changes made will be lost."),"",MessageBoxButtons.YesNo)==DialogResult.Yes) {
							HL7Defs.Delete(HL7DefCur.HL7DefNum);
						}
						else {//user selected No
							return;
						}
					}
					else {//custom
						//Disable the custom def
						HL7DefCur.IsEnabled=false;
						HL7Defs.Update(HL7DefCur);
					}
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}