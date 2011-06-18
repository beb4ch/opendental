//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class MedicationPatCrud {
		///<summary>Gets one MedicationPat object from the database using the primary key.  Returns null if not found.</summary>
		internal static MedicationPat SelectOne(long medicationPatNum){
			string command="SELECT * FROM medicationpat "
				+"WHERE MedicationPatNum = "+POut.Long(medicationPatNum);
			List<MedicationPat> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one MedicationPat object from the database using a query.</summary>
		internal static MedicationPat SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<MedicationPat> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of MedicationPat objects from the database using a query.</summary>
		internal static List<MedicationPat> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<MedicationPat> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<MedicationPat> TableToList(DataTable table){
			List<MedicationPat> retVal=new List<MedicationPat>();
			MedicationPat medicationPat;
			for(int i=0;i<table.Rows.Count;i++) {
				medicationPat=new MedicationPat();
				medicationPat.MedicationPatNum= PIn.Long  (table.Rows[i]["MedicationPatNum"].ToString());
				medicationPat.PatNum          = PIn.Long  (table.Rows[i]["PatNum"].ToString());
				medicationPat.MedicationNum   = PIn.Long  (table.Rows[i]["MedicationNum"].ToString());
				medicationPat.PatNote         = PIn.String(table.Rows[i]["PatNote"].ToString());
				medicationPat.DateTStamp      = PIn.DateT (table.Rows[i]["DateTStamp"].ToString());
				medicationPat.DateStart       = PIn.Date  (table.Rows[i]["DateStart"].ToString());
				medicationPat.DateStop        = PIn.Date  (table.Rows[i]["DateStop"].ToString());
				medicationPat.ProvNum         = PIn.Long  (table.Rows[i]["ProvNum"].ToString());
				retVal.Add(medicationPat);
			}
			return retVal;
		}

		///<summary>Inserts one MedicationPat into the database.  Returns the new priKey.</summary>
		internal static long Insert(MedicationPat medicationPat){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				medicationPat.MedicationPatNum=DbHelper.GetNextOracleKey("medicationpat","MedicationPatNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(medicationPat,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							medicationPat.MedicationPatNum++;
							loopcount++;
						}
						else{
							throw ex;
						}
					}
				}
				throw new ApplicationException("Insert failed.  Could not generate primary key.");
			}
			else {
				return Insert(medicationPat,false);
			}
		}

		///<summary>Inserts one MedicationPat into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(MedicationPat medicationPat,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				medicationPat.MedicationPatNum=ReplicationServers.GetKey("medicationpat","MedicationPatNum");
			}
			string command="INSERT INTO medicationpat (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="MedicationPatNum,";
			}
			command+="PatNum,MedicationNum,PatNote,DateStart,DateStop,ProvNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(medicationPat.MedicationPatNum)+",";
			}
			command+=
				     POut.Long  (medicationPat.PatNum)+","
				+    POut.Long  (medicationPat.MedicationNum)+","
				+"'"+POut.String(medicationPat.PatNote)+"',"
				//DateTStamp can only be set by MySQL
				+    POut.Date  (medicationPat.DateStart)+","
				+    POut.Date  (medicationPat.DateStop)+","
				+    POut.Long  (medicationPat.ProvNum)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				medicationPat.MedicationPatNum=Db.NonQ(command,true);
			}
			return medicationPat.MedicationPatNum;
		}

		///<summary>Updates one MedicationPat in the database.</summary>
		internal static void Update(MedicationPat medicationPat){
			string command="UPDATE medicationpat SET "
				+"PatNum          =  "+POut.Long  (medicationPat.PatNum)+", "
				+"MedicationNum   =  "+POut.Long  (medicationPat.MedicationNum)+", "
				+"PatNote         = '"+POut.String(medicationPat.PatNote)+"', "
				//DateTStamp can only be set by MySQL
				+"DateStart       =  "+POut.Date  (medicationPat.DateStart)+", "
				+"DateStop        =  "+POut.Date  (medicationPat.DateStop)+", "
				+"ProvNum         =  "+POut.Long  (medicationPat.ProvNum)+" "
				+"WHERE MedicationPatNum = "+POut.Long(medicationPat.MedicationPatNum);
			Db.NonQ(command);
		}

		///<summary>Updates one MedicationPat in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(MedicationPat medicationPat,MedicationPat oldMedicationPat){
			string command="";
			if(medicationPat.PatNum != oldMedicationPat.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(medicationPat.PatNum)+"";
			}
			if(medicationPat.MedicationNum != oldMedicationPat.MedicationNum) {
				if(command!=""){ command+=",";}
				command+="MedicationNum = "+POut.Long(medicationPat.MedicationNum)+"";
			}
			if(medicationPat.PatNote != oldMedicationPat.PatNote) {
				if(command!=""){ command+=",";}
				command+="PatNote = '"+POut.String(medicationPat.PatNote)+"'";
			}
			//DateTStamp can only be set by MySQL
			if(medicationPat.DateStart != oldMedicationPat.DateStart) {
				if(command!=""){ command+=",";}
				command+="DateStart = "+POut.Date(medicationPat.DateStart)+"";
			}
			if(medicationPat.DateStop != oldMedicationPat.DateStop) {
				if(command!=""){ command+=",";}
				command+="DateStop = "+POut.Date(medicationPat.DateStop)+"";
			}
			if(medicationPat.ProvNum != oldMedicationPat.ProvNum) {
				if(command!=""){ command+=",";}
				command+="ProvNum = "+POut.Long(medicationPat.ProvNum)+"";
			}
			if(command==""){
				return;
			}
			command="UPDATE medicationpat SET "+command
				+" WHERE MedicationPatNum = "+POut.Long(medicationPat.MedicationPatNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one MedicationPat from the database.</summary>
		internal static void Delete(long medicationPatNum){
			string command="DELETE FROM medicationpat "
				+"WHERE MedicationPatNum = "+POut.Long(medicationPatNum);
			Db.NonQ(command);
		}

	}
}