﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDental.DataAccess;


namespace OpenDentBusiness{
	///<summary>A list of Anesthetic Medications</summary>
	public class AnesthMedsGivens{

        public bool IsNew;

		///<summary></summary> 
		public static DataTable RefreshCache(int anestheticRecordNum){
			int ARNum = anestheticRecordNum;
			string c="SELECT * FROM anesthmedsgiven WHERE AnestheticRecordNum ='" + anestheticRecordNum+ "'" + "ORDER BY DoseTimeStamp DESC"; //most recent at top of list
			DataTable table=General.GetTable(c);
			table.TableName="AnesthMedsGiven";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			AnestheticMedsGivenC.Listt=new List<AnestheticMedsGiven>();
			AnestheticMedsGiven medCur;
			for(int i=0;i<table.Rows.Count;i++){
				medCur=new AnestheticMedsGiven();
				medCur.IsNew = false;
				medCur.AnestheticMedNum  = PIn.PInt(table.Rows[i][0].ToString());
				medCur.AnestheticRecordNum   = PIn.PString(table.Rows[i][1].ToString());
				medCur.AnesthMedName         = PIn.PString(table.Rows[i][2].ToString());
				medCur.QtyGiven       = PIn.PString(table.Rows[i][3].ToString());
				medCur.QtyWasted           = PIn.PString(table.Rows[i][4].ToString());
				medCur.DoseTimeStamp = PIn.PString(table.Rows[i][5].ToString());
				medCur.AnesthMedNum = PIn.PString(table.Rows[i][6].ToString());
				AnestheticMedsGivenC.Listt.Add(medCur);
			}
		}

		///<Summary>Gets one Anesthetic Medication Given from the database.</Summary>
		public static AnestheticMedsGiven CreateObject(int AnestheticMedNum){
			return DataObjectFactory<AnestheticMedsGiven>.CreateObject(AnestheticMedNum);
		}

		public static List<AnestheticMedsGiven> GetAnesthMedsGiven(int[] AnestheticMedNums){
			Collection<AnestheticMedsGiven> collectState=DataObjectFactory<AnestheticMedsGiven>.CreateObjects(AnestheticMedNums);
			return new List<AnestheticMedsGiven>(collectState);		
		}

		///<summary></summary>
		public static void WriteObject(AnestheticMedsGiven AnesthMedName){
			DataObjectFactory<AnestheticMedsGiven>.WriteObject(AnesthMedName);
		}

		///<summary></summary>
		public static void DeleteObject(AnestheticMedsGiven AnesthMedName){

			DataObjectFactory<AnestheticMedsGiven>.DeleteObject(AnesthMedName);
		}

		public static string GetAnestheticMedName(int AnestheticMedNum){
			if(AnestheticMedNum==0){
				return "";
			}
			for(int i=0;i<AnestheticMedsGivenC.Listt.Count;i++){
				if(AnestheticMedsGivenC.Listt[i].AnestheticMedNum==AnestheticMedNum){
					return AnestheticMedsGivenC.Listt[i].AnesthMedName;
				}
			}
			return "";
		}

		}
}


