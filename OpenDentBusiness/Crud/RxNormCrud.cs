//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class RxNormCrud {
		///<summary>Gets one RxNorm object from the database using the primary key.  Returns null if not found.</summary>
		internal static RxNorm SelectOne(long rxNormNum){
			string command="SELECT * FROM rxnorm "
				+"WHERE RxNormNum = "+POut.Long(rxNormNum);
			List<RxNorm> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one RxNorm object from the database using a query.</summary>
		internal static RxNorm SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<RxNorm> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of RxNorm objects from the database using a query.</summary>
		internal static List<RxNorm> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<RxNorm> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<RxNorm> TableToList(DataTable table){
			List<RxNorm> retVal=new List<RxNorm>();
			RxNorm rxNorm;
			for(int i=0;i<table.Rows.Count;i++) {
				rxNorm=new RxNorm();
				rxNorm.RxNormNum  = PIn.Long  (table.Rows[i]["RxNormNum"].ToString());
				rxNorm.RxCui      = PIn.String(table.Rows[i]["RxCui"].ToString());
				rxNorm.MmslCode   = PIn.String(table.Rows[i]["MmslCode"].ToString());
				rxNorm.Description= PIn.String(table.Rows[i]["Description"].ToString());
				retVal.Add(rxNorm);
			}
			return retVal;
		}

		///<summary>Inserts one RxNorm into the database.  Returns the new priKey.</summary>
		internal static long Insert(RxNorm rxNorm){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				rxNorm.RxNormNum=DbHelper.GetNextOracleKey("rxnorm","RxNormNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(rxNorm,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							rxNorm.RxNormNum++;
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
				return Insert(rxNorm,false);
			}
		}

		///<summary>Inserts one RxNorm into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(RxNorm rxNorm,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				rxNorm.RxNormNum=ReplicationServers.GetKey("rxnorm","RxNormNum");
			}
			string command="INSERT INTO rxnorm (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="RxNormNum,";
			}
			command+="RxCui,MmslCode,Description) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(rxNorm.RxNormNum)+",";
			}
			command+=
				 "'"+POut.String(rxNorm.RxCui)+"',"
				+"'"+POut.String(rxNorm.MmslCode)+"',"
				+"'"+POut.String(rxNorm.Description)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				rxNorm.RxNormNum=Db.NonQ(command,true);
			}
			return rxNorm.RxNormNum;
		}

		///<summary>Updates one RxNorm in the database.</summary>
		internal static void Update(RxNorm rxNorm){
			string command="UPDATE rxnorm SET "
				+"RxCui      = '"+POut.String(rxNorm.RxCui)+"', "
				+"MmslCode   = '"+POut.String(rxNorm.MmslCode)+"', "
				+"Description= '"+POut.String(rxNorm.Description)+"' "
				+"WHERE RxNormNum = "+POut.Long(rxNorm.RxNormNum);
			Db.NonQ(command);
		}

		///<summary>Updates one RxNorm in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(RxNorm rxNorm,RxNorm oldRxNorm){
			string command="";
			if(rxNorm.RxCui != oldRxNorm.RxCui) {
				if(command!=""){ command+=",";}
				command+="RxCui = '"+POut.String(rxNorm.RxCui)+"'";
			}
			if(rxNorm.MmslCode != oldRxNorm.MmslCode) {
				if(command!=""){ command+=",";}
				command+="MmslCode = '"+POut.String(rxNorm.MmslCode)+"'";
			}
			if(rxNorm.Description != oldRxNorm.Description) {
				if(command!=""){ command+=",";}
				command+="Description = '"+POut.String(rxNorm.Description)+"'";
			}
			if(command==""){
				return;
			}
			command="UPDATE rxnorm SET "+command
				+" WHERE RxNormNum = "+POut.Long(rxNorm.RxNormNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one RxNorm from the database.</summary>
		internal static void Delete(long rxNormNum){
			string command="DELETE FROM rxnorm "
				+"WHERE RxNormNum = "+POut.Long(rxNormNum);
			Db.NonQ(command);
		}

	}
}