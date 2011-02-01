using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormCreditCardManage:Form {
		private Patient PatCur;
		private List<CreditCard> creditCards;

		public FormCreditCardManage(Patient pat) {
			InitializeComponent();
			Lan.F(this);
			PatCur=pat;
		}
		
		private void FormCreditCardManage_Load(object sender,EventArgs e) {
			RefreshCardList();
			if(creditCards.Count>0) {
				listCreditCards.SelectedIndex=0;
			}
		}

		private void RefreshCardList() {
			listCreditCards.Items.Clear();
			CreditCards.FillCache(CreditCards.RefreshCache());
			creditCards=CreditCards.Refresh(PatCur.PatNum);
			for(int i=0;i<creditCards.Count;i++) {
				listCreditCards.Items.Add(creditCards[i].CCNumberMasked);
			}
		}

		private void listCreditCards_MouseDoubleClick(object sender,MouseEventArgs e) {
			if(listCreditCards.SelectedIndex==-1) {
				return;
			}
			FormCreditCardEdit FormCCE=new FormCreditCardEdit(PatCur);
			FormCCE.CreditCardCur=creditCards[listCreditCards.SelectedIndex];
			FormCCE.ShowDialog();
			RefreshCardList();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			bool remember=false;
			int placement=listCreditCards.SelectedIndex;
			if(placement!=-1) {
				remember=true;
			}
			FormCreditCardEdit FormCCE=new FormCreditCardEdit(PatCur);
			FormCCE.CreditCardCur=new CreditCard();
			FormCCE.CreditCardCur.IsNew=true;
			FormCCE.ShowDialog();
			RefreshCardList();
			if(remember) {//in case they canceled and had one selected
				listCreditCards.SelectedIndex=placement;
			}
			if(FormCCE.DialogResult==DialogResult.OK) {
				listCreditCards.SelectedIndex=0;
			}
		}

		private void butUp_Click(object sender,EventArgs e) {
			int placement=listCreditCards.SelectedIndex;
			if(placement==-1) {
				MsgBox.Show(this,"Please select a card first.");
				return;
			}
			if(placement==0) {
				return;//can't move up any more
			}
			int oldIdx;
			int newIdx;
			CreditCard oldItem;
			CreditCard newItem;
			oldIdx=creditCards[placement].ItemOrder;
			newIdx=oldIdx+1; 
			for(int i=0;i<CreditCards.Listt.Count;i++) {
				if(CreditCards.Listt[i].ItemOrder==oldIdx) {
					oldItem=CreditCards.Listt[i];
					newItem=CreditCards.Listt[i-1];
					oldItem.ItemOrder=newItem.ItemOrder;
					newItem.ItemOrder-=1;
					CreditCards.Update(oldItem);
					CreditCards.Update(newItem);
				}
			}
			RefreshCardList();
			listCreditCards.SetSelected(placement-1,true);
		}

		private void butDown_Click(object sender,EventArgs e) {
			int placement=listCreditCards.SelectedIndex;
			if(placement==-1) {
				MsgBox.Show(this,"Please select a card first.");
				return;
			}
			if(placement==creditCards.Count-1) {
				return;//can't move down any more
			}
			int oldIdx;
			int newIdx;
			CreditCard oldItem;
			CreditCard newItem;
			oldIdx=creditCards[placement].ItemOrder;
			newIdx=oldIdx-1;
			for(int i=0;i<CreditCards.Listt.Count;i++) {
				if(CreditCards.Listt[i].ItemOrder==newIdx) {
					newItem=CreditCards.Listt[i];
					oldItem=CreditCards.Listt[i-1];
					newItem.ItemOrder=oldItem.ItemOrder;
					oldItem.ItemOrder-=1;
					CreditCards.Update(oldItem);
					CreditCards.Update(newItem);
				}
			}
			RefreshCardList();
			listCreditCards.SetSelected(placement+1,true);
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}