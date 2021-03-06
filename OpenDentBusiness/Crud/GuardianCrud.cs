//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class GuardianCrud {
		///<summary>Gets one Guardian object from the database using the primary key.  Returns null if not found.</summary>
		public static Guardian SelectOne(long guardianNum){
			string command="SELECT * FROM guardian "
				+"WHERE GuardianNum = "+POut.Long(guardianNum);
			List<Guardian> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Guardian object from the database using a query.</summary>
		public static Guardian SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Guardian> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Guardian objects from the database using a query.</summary>
		public static List<Guardian> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Guardian> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Guardian> TableToList(DataTable table){
			List<Guardian> retVal=new List<Guardian>();
			Guardian guardian;
			for(int i=0;i<table.Rows.Count;i++) {
				guardian=new Guardian();
				guardian.GuardianNum   = PIn.Long  (table.Rows[i]["GuardianNum"].ToString());
				guardian.PatNumChild   = PIn.Long  (table.Rows[i]["PatNumChild"].ToString());
				guardian.PatNumGuardian= PIn.Long  (table.Rows[i]["PatNumGuardian"].ToString());
				guardian.Relationship  = (GuardianRelationship)PIn.Int(table.Rows[i]["Relationship"].ToString());
				guardian.IsGuardian    = PIn.Bool  (table.Rows[i]["IsGuardian"].ToString());
				retVal.Add(guardian);
			}
			return retVal;
		}

		///<summary>Inserts one Guardian into the database.  Returns the new priKey.</summary>
		public static long Insert(Guardian guardian){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				guardian.GuardianNum=DbHelper.GetNextOracleKey("guardian","GuardianNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(guardian,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							guardian.GuardianNum++;
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
				return Insert(guardian,false);
			}
		}

		///<summary>Inserts one Guardian into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Guardian guardian,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				guardian.GuardianNum=ReplicationServers.GetKey("guardian","GuardianNum");
			}
			string command="INSERT INTO guardian (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="GuardianNum,";
			}
			command+="PatNumChild,PatNumGuardian,Relationship,IsGuardian) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(guardian.GuardianNum)+",";
			}
			command+=
				     POut.Long  (guardian.PatNumChild)+","
				+    POut.Long  (guardian.PatNumGuardian)+","
				+    POut.Int   ((int)guardian.Relationship)+","
				+    POut.Bool  (guardian.IsGuardian)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				guardian.GuardianNum=Db.NonQ(command,true);
			}
			return guardian.GuardianNum;
		}

		///<summary>Updates one Guardian in the database.</summary>
		public static void Update(Guardian guardian){
			string command="UPDATE guardian SET "
				+"PatNumChild   =  "+POut.Long  (guardian.PatNumChild)+", "
				+"PatNumGuardian=  "+POut.Long  (guardian.PatNumGuardian)+", "
				+"Relationship  =  "+POut.Int   ((int)guardian.Relationship)+", "
				+"IsGuardian    =  "+POut.Bool  (guardian.IsGuardian)+" "
				+"WHERE GuardianNum = "+POut.Long(guardian.GuardianNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Guardian in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		public static void Update(Guardian guardian,Guardian oldGuardian){
			string command="";
			if(guardian.PatNumChild != oldGuardian.PatNumChild) {
				if(command!=""){ command+=",";}
				command+="PatNumChild = "+POut.Long(guardian.PatNumChild)+"";
			}
			if(guardian.PatNumGuardian != oldGuardian.PatNumGuardian) {
				if(command!=""){ command+=",";}
				command+="PatNumGuardian = "+POut.Long(guardian.PatNumGuardian)+"";
			}
			if(guardian.Relationship != oldGuardian.Relationship) {
				if(command!=""){ command+=",";}
				command+="Relationship = "+POut.Int   ((int)guardian.Relationship)+"";
			}
			if(guardian.IsGuardian != oldGuardian.IsGuardian) {
				if(command!=""){ command+=",";}
				command+="IsGuardian = "+POut.Bool(guardian.IsGuardian)+"";
			}
			if(command==""){
				return;
			}
			command="UPDATE guardian SET "+command
				+" WHERE GuardianNum = "+POut.Long(guardian.GuardianNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one Guardian from the database.</summary>
		public static void Delete(long guardianNum){
			string command="DELETE FROM guardian "
				+"WHERE GuardianNum = "+POut.Long(guardianNum);
			Db.NonQ(command);
		}

	}
}