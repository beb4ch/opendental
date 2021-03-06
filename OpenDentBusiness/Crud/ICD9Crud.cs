//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class ICD9Crud {
		///<summary>Gets one ICD9 object from the database using the primary key.  Returns null if not found.</summary>
		public static ICD9 SelectOne(long iCD9Num){
			string command="SELECT * FROM icd9 "
				+"WHERE ICD9Num = "+POut.Long(iCD9Num);
			List<ICD9> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one ICD9 object from the database using a query.</summary>
		public static ICD9 SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ICD9> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of ICD9 objects from the database using a query.</summary>
		public static List<ICD9> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ICD9> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<ICD9> TableToList(DataTable table){
			List<ICD9> retVal=new List<ICD9>();
			ICD9 iCD9;
			for(int i=0;i<table.Rows.Count;i++) {
				iCD9=new ICD9();
				iCD9.ICD9Num    = PIn.Long  (table.Rows[i]["ICD9Num"].ToString());
				iCD9.ICD9Code   = PIn.String(table.Rows[i]["ICD9Code"].ToString());
				iCD9.Description= PIn.String(table.Rows[i]["Description"].ToString());
				iCD9.DateTStamp = PIn.DateT (table.Rows[i]["DateTStamp"].ToString());
				retVal.Add(iCD9);
			}
			return retVal;
		}

		///<summary>Inserts one ICD9 into the database.  Returns the new priKey.</summary>
		public static long Insert(ICD9 iCD9){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				iCD9.ICD9Num=DbHelper.GetNextOracleKey("icd9","ICD9Num");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(iCD9,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							iCD9.ICD9Num++;
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
				return Insert(iCD9,false);
			}
		}

		///<summary>Inserts one ICD9 into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(ICD9 iCD9,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				iCD9.ICD9Num=ReplicationServers.GetKey("icd9","ICD9Num");
			}
			string command="INSERT INTO icd9 (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="ICD9Num,";
			}
			command+="ICD9Code,Description) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(iCD9.ICD9Num)+",";
			}
			command+=
				 "'"+POut.String(iCD9.ICD9Code)+"',"
				+"'"+POut.String(iCD9.Description)+"')";
				//DateTStamp can only be set by MySQL
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				iCD9.ICD9Num=Db.NonQ(command,true);
			}
			return iCD9.ICD9Num;
		}

		///<summary>Updates one ICD9 in the database.</summary>
		public static void Update(ICD9 iCD9){
			string command="UPDATE icd9 SET "
				+"ICD9Code   = '"+POut.String(iCD9.ICD9Code)+"', "
				+"Description= '"+POut.String(iCD9.Description)+"' "
				//DateTStamp can only be set by MySQL
				+"WHERE ICD9Num = "+POut.Long(iCD9.ICD9Num);
			Db.NonQ(command);
		}

		///<summary>Updates one ICD9 in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		public static void Update(ICD9 iCD9,ICD9 oldICD9){
			string command="";
			if(iCD9.ICD9Code != oldICD9.ICD9Code) {
				if(command!=""){ command+=",";}
				command+="ICD9Code = '"+POut.String(iCD9.ICD9Code)+"'";
			}
			if(iCD9.Description != oldICD9.Description) {
				if(command!=""){ command+=",";}
				command+="Description = '"+POut.String(iCD9.Description)+"'";
			}
			//DateTStamp can only be set by MySQL
			if(command==""){
				return;
			}
			command="UPDATE icd9 SET "+command
				+" WHERE ICD9Num = "+POut.Long(iCD9.ICD9Num);
			Db.NonQ(command);
		}

		///<summary>Deletes one ICD9 from the database.</summary>
		public static void Delete(long iCD9Num){
			string command="DELETE FROM icd9 "
				+"WHERE ICD9Num = "+POut.Long(iCD9Num);
			Db.NonQ(command);
		}

	}
}